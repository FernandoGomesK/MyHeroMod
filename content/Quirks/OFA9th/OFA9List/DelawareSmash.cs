using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using Terraria.DataStructures;
using MyHeroMod.content.Projectiles;
using Terraria.Graphics.CameraModifiers;


namespace MyHeroMod.content.Quirks.OFA9th.Skills 
{
    public class DelawareSmashSkill : QuirkBaseSkill
    {
        public override string Name => "Delaware Smash";
        public override string Description => "Propel air forward with a flick of your fingers";
        public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
        public override int BaseCooldown => 120;
        public override string Category => "OneForAll9th";

        public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;
        

        public override void OnUse(Player player)
        {
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            
            int MaxDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 50,
                QuirkStage.Adequation => 150,
                QuirkStage.Intermediate => 300,
                QuirkStage.Advanced => 550,
                QuirkStage.Final => 1200,
                _ => 150
            };

            bool consumeFinger = false;
            bool hurtPlayer = false;
            float DamageMultiplier = 1f;

        
            if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()))
            {
                float baseMulti = ofaPlayer.percentage switch
                {
                    45 => 0.45f,
                    20 => 0.20f,
                    10 => 0.10f,
                    5 => 0.05f,
                    _ => 1f
                };
                
                
                DamageMultiplier = ofaPlayer.isAirForceOn ? baseMulti + 0.15f : baseMulti;
            }
            else
            {
                hurtPlayer = true;
                consumeFinger = true;
            }

            
            if (consumeFinger && ofaPlayer.currentFingers <= 0)
            {
                CombatText.NewText(player.getRect(), Color.Red, "No fingers left!");
                return; 
            }

            if (consumeFinger) ofaPlayer.currentFingers--;

            int FinalDamage = (int)(MaxDamage * DamageMultiplier);
            Vector2 Velocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero);

            
            if (hurtPlayer)
            {   
                Velocity *= 30f;
            }
            else
            {
                float airForceMod = ofaPlayer.isAirForceOn ? 10f : 0f;
                Velocity *= 15f + airForceMod;
            }

            
            if (player.HasBuff(ModContent.BuffType<FloatBuff>()))
            {
                player.velocity = -Velocity * 2f;

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Cloud, Velocity.X * 2, Velocity.Y * 2, 0, default, 1f);
                }
            }
            
            Vector2 textPosition = player.Center + new Vector2(0, -30f);
            Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>(), 0, 0f, player.whoAmI);

          
            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                Velocity, 
                ModContent.ProjectileType<DelawareSmashProj>(), 
                FinalDamage, 
                2f, 
                player.whoAmI
            );
            
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, player.position);

            
            if (hurtPlayer)
            {
                player.statLife -= (int)(player.statLifeMax2 * 0.05f);
                if (player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", player.name)
                    );
                    player.KillMe(reason, FinalDamage, 0);        
                }
            }


            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}