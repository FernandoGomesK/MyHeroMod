using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using KhacesCore.Content.System;
using KhacesCore.Content.System.Interfaces;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.System.Interfaces;
using Steamworks;
using MyHeroMod.content.Buffs.Gearshift;

namespace MyHeroMod.content.Quirks.Gearshift
{
    /// <summary>
    /// State management for the Gearshift quirk: activation charge-up, buff and recoil handling,
    /// movement and shooting bonuses, and the afterimage request.
    /// </summary>
    public partial class GearshiftPlayer : ModPlayer, IDashModifier, IQuirkResetter
    {
        #region Tuning

        /// <summary>Run speed bonus while Gearshift is active and the player has One For All 9th.</summary>
        public const float RunSpeedBoostOneForAll = 5.0f;

        /// <summary>Run speed bonus while Gearshift is active without One For All 9th.</summary>
        public const float RunSpeedBoostDefault = 2.0f;

        /// <summary>Multiplier applied to the velocity of every projectile shot while Gearshift is active.</summary>
        public const float ProjectileVelocityMultiplier = 2.5f;

        /// <summary>Multiplier applied to the damage of every attack shot while Gearshift is active.</summary>
        public const float ProjectileDamageMultiplier = 1.3f;

        /// <summary>Recoil duration multiplier for players with the Resistant Body nature.</summary>
        public const float ResistantBodyRecoilMultiplier = 0.75f;

        /// <summary>Velocity multiplier applied every tick during the activation charge-up.</summary>
        public const float ActivationVelocityDamping = 0.8f;

        /// <summary>Number of dust particles spawned in the burst when the charge-up finishes.</summary>
        public const int ActivationBurstDustCount = 20;

        /// <summary>Offset from the player's center where the onomatopoeia projectile spawns.</summary>
        private static readonly Vector2 OnomatopoeiaOffset = new(0f, -60f);

        /// <summary>Color shared by every Gearshift dust effect.</summary>
        private static readonly Color DustColor = Color.Cyan;

        /// <summary>Gets the Gearshift buff duration (in ticks) for the given stage.</summary>
        public static int GetBuffDuration(QuirkStage stage) => stage switch
        {
            QuirkStage.Initial => 380,
            QuirkStage.Adequation => 590,
            QuirkStage.Intermediate => 750,
            QuirkStage.Advanced => 1800,
            QuirkStage.Final => 3500,
            _ => 6000
        };

        /// <summary>Gets the base recoil debuff duration (in ticks) for the given stage.</summary>
        public static int GetRecoilDuration(QuirkStage stage) => stage switch
        {
            QuirkStage.Initial => 187,
            QuirkStage.Adequation => 375,
            QuirkStage.Intermediate => 750,
            QuirkStage.Advanced => 1500,
            QuirkStage.Final => 3000,
            _ => 6000
        };

        #endregion

        #region State

        /// <summary>True on every tick in which the Gearshift buff is active. Reset each tick in <see cref="ResetEffects"/>.</summary>
        public bool isGearshiftBuffActive = false;

        public bool isTopGearActive = false;

        /// <summary>Value of <see cref="isGearshiftBuffActive"/> on the previous tick.</summary>
        public bool wasGearshiftBuffActive = false;

        /// <summary>True while the activation charge-up is running.</summary>
        public bool GearActivation = false;

        /// <summary>Ticks elapsed in the current activation charge-up.</summary>
        public int ActivationTimer = 0;

        /// <summary>Ticks the activation charge-up lasts before the buff is applied.</summary>
        public int ActivationMaxTime = 40;

        /// <summary>True if this ModPlayer belongs to the client's own player.</summary>
        private bool IsLocalPlayer => Player.whoAmI == Main.myPlayer;

        #endregion

        #region Reset

        /// <inheritdoc cref="IQuirkResetter.FullReset"/>
        public void FullReset()
        {
            ResetGearshiftState();
        }

        public override void OnRespawn()
        {
            ResetGearshiftState();
        }

        /// <summary>Clears the buff and cancels any activation in progress.</summary>
        private void ResetGearshiftState()
        {
            isGearshiftBuffActive = false;
            GearActivation = false;
            ActivationTimer = 0;
            Player.ClearBuff(ModContent.BuffType<GearshiftBuff>());
            Player.ClearBuff(ModContent.BuffType<TopGearBuff>());
        }

        public override void ResetEffects()
        {
            wasGearshiftBuffActive = isGearshiftBuffActive;
            isGearshiftBuffActive = false;
            isTopGearActive = false;
        }

        #endregion

        #region Movement and combat

        /// <summary>
        /// Applies the run speed bonus. Runs after accessories 
        /// so the bonus is not overwritten.
        /// </summary>
        public override void PostUpdateRunSpeeds()
        {
            if (!isGearshiftBuffActive)
                return;

            bool hasOneForAll = Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.OneForAll9th);
            float speedBoost = hasOneForAll ? RunSpeedBoostOneForAll : RunSpeedBoostDefault;

            Player.maxRunSpeed += speedBoost;
            Player.accRunSpeed += speedBoost;
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (!isGearshiftBuffActive)
                return;

            velocity *= ProjectileVelocityMultiplier;
            damage = (int)(damage * ProjectileDamageMultiplier);
        }

        #endregion

        #region Activation

        public override void PreUpdate()
        {
            if (!GearActivation)
                return;

            ActivationTimer++;
            Player.velocity *= ActivationVelocityDamping;

            if (Main.netMode != NetmodeID.Server && Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, DustColor, 0.5f);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }

            if (ActivationTimer >= ActivationMaxTime)
            {
                GearActivation = false;
                ActivationTimer = 0;
                ActivateGearshift();
            }
        }

        /// <summary>
        /// Finishes the charge-up. The dust burst runs on every client; the buff and the
        /// onomatopoeia projectile are only created by the owner, because the buff is synced by vanilla
        /// and the projectile is replicated by the server (creating them everywhere would duplicate them).
        /// </summary>
        private void ActivateGearshift()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < ActivationBurstDustCount; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, speed.X, speed.Y, 0, DustColor, 2f);
                }
            }

            if (!IsLocalPlayer)
                return;

            var transformPlayer = Player.GetModPlayer<TransformationPlayer>();
            Player.AddBuff(ModContent.BuffType<GearshiftBuff>(), GetBuffDuration(transformPlayer.CurrentStage));

            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center + OnomatopoeiaOffset,
                Vector2.Zero,
                ModContent.ProjectileType<OverdriveOnomatopoeia>(),
                0,
                0f,
                Player.whoAmI
            );
        }

        #endregion

        #region Visuals

        public override void PostUpdateMiscEffects()
        {
            if (isGearshiftBuffActive)
                Player.GetModPlayer<AfterimagePlayer>().Request(AfterimagePalettes.Blue);
        }

        #endregion

        #region Recoil

        /// <summary>
        /// Applies the recoil debuff on the tick the Gearshift buff ends.
        /// Only the owner applies it; other clients receive the debuff through vanilla buff sync.
        /// </summary>
        public override void PostUpdateBuffs()
        {
            if (!IsLocalPlayer)
                return;

            if (wasGearshiftBuffActive && !isGearshiftBuffActive)
            {

                var transformPlayer = Player.GetModPlayer<TransformationPlayer>();

                Player.ClearBuff(ModContent.BuffType<TopGearBuff>());
                if (transformPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
                {

                    Player.ClearBuff(ModContent.BuffType<TopGearBuff>());
                    int recoilDuration = GetRecoilDuration(transformPlayer.CurrentStage);

                    if (transformPlayer.Nature == NatureType.ResistantBody)
                    {
                      
                        recoilDuration = (int)(recoilDuration * ResistantBodyRecoilMultiplier);
                    }

                    if (recoilDuration > 0)
                        Player.AddBuff(ModContent.BuffType<GearshiftRecoil>(), recoilDuration);
                }
            }
        }

        #endregion
    }
}