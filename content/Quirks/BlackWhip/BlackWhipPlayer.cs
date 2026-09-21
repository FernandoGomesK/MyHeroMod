using KhacesCore.Content.System;
using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles;
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhipStun;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Quirks.BlackWhip
{
   
    public partial class BlackWhipPlayer : ModPlayer, IStrainSource
    {
        public bool isOverlayActive = false;
        public bool isAutomaticWhipActive = false;
        public int overlayAutoAttackTimer = 0;

        public CorePlayer Core => Player.GetModPlayer<CorePlayer>();
        bool IStrainSource.IsStrainActive => Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.Explosion);
        bool IStrainSource.CausesStrainDamage => true;



        public int StrainPenaltyPerSecond { get; set; }

        public void AddStrain(int amount)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            Core.currentStrain += amount;

            if (Core.currentStrain <= 0) Core.currentStrain = 0;
            if (Core.currentStrain >= Core.maxStrain)
            {
                Core.currentStrain = Core.maxStrain;
            
                Player.ClearBuff(ModContent.BuffType<OverlayBuff>());
            }
        }

       public override void ResetEffects()
        {
            
            isOverlayActive = Player.HasBuff(ModContent.BuffType<OverlayBuff>());
            isAutomaticWhipActive = Player.HasBuff(ModContent.BuffType<AutomaticWhipBuff>());

           
            if (!isOverlayActive && !isAutomaticWhipActive)
            {
                overlayAutoAttackTimer = 0;
                StrainPenaltyPerSecond = 0; 
            }
            else
            {
                
                StrainPenaltyPerSecond = isOverlayActive ? 15 : 0; 
            }
        }

        
        public override void FrameEffects()
        {
            if (isOverlayActive)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "OverlayHead", EquipType.Head);
                Player.handon = EquipLoader.GetEquipSlot(Mod, "OverlayArms", EquipType.HandsOn);
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "OverlayArms", EquipType.HandsOff);
                Player.front = EquipLoader.GetEquipSlot(Mod, "OverlayBody", EquipType.Front);
            }
        }

       
        public override void PostUpdate()
        {
            if (isOverlayActive || isAutomaticWhipActive)
            {
                overlayAutoAttackTimer++;

               
                if (overlayAutoAttackTimer >= 45)
                {
                    overlayAutoAttackTimer = 0;
                    ExecuteOverlayAutoAttack();
                }
            }
        }

       private void ExecuteOverlayAutoAttack()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            int baseDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 20,
                QuirkStage.Adequation => 50,
                QuirkStage.Intermediate => 90,
                QuirkStage.Advanced => 150,
                QuirkStage.Final => 250,
                _ => 20
            };

            int projectileCount = 1;

            KhacesCore.Content.System.Interfaces.TargetFinder targetFinder = new();
            NPC target = ((KhacesCore.Content.System.Interfaces.IClosestEnemyFinder)targetFinder).FindClosestEnemy(Player, 400f, false);

            Vector2 baseVelocity;
            float hasTargetFlag; 

            if (target != null)
            {
                
                baseVelocity = (target.Center - Player.Center).SafeNormalize(Vector2.UnitY * -1) * 8f;
                hasTargetFlag = 1f;
            }
            else
            {
                float randomUpwardAngle = Main.rand.NextFloat(-MathHelper.PiOver2 - 0.5f, -MathHelper.PiOver2 + 0.5f);
                baseVelocity = randomUpwardAngle.ToRotationVector2() * 8f;
                hasTargetFlag = 0f;
            }

            for (int i = 0; i < projectileCount; i++)
            {
                Vector2 finalVelocity = baseVelocity;
                if (target == null || projectileCount > 1)
                {
                    finalVelocity = baseVelocity.RotatedByRandom(MathHelper.ToRadians(25));
                }


                Projectile.NewProjectile(
                    Player.GetSource_FromThis(), 
                    Player.Center, 
                    finalVelocity, 
                    ModContent.ProjectileType<AutomaticBlackWhipProj>(), 
                    baseDamage, 
                    2f, 
                    Player.whoAmI,
                    hasTargetFlag 
                );
            }

            
            SoundEngine.PlaySound(SoundID.Item17 with { Volume = 0.6f }, Player.position);
        }
    }
}