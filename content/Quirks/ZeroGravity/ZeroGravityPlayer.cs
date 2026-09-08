using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.ZeroGravity.Projectiles.GravityBubble;

namespace MyHeroMod.content.Quirks.ZeroGravity
{
    public partial class ZeroGravityPlayer : ModPlayer, IQuirkResetter
    {
        public bool isZeroGravityActive = false;
        
        
        public int Nausea = 0;
        public int NauseaMax = 300; 
        public int overlayAutoAttackTimer = 0;
        public bool isAwaken = false;


        public void FullReset()
        {
            isZeroGravityActive = false;
            Nausea = 0;
            isAwaken = false;
        }

        public override void ResetEffects()
        {
            isZeroGravityActive = false;
            isAwaken = false;
            if (Player.HasBuff(ModContent.BuffType<GravityAwakenBuff>()))
            {
                isAwaken = true;
            }
            
        }

        public override void PostUpdateEquips()
        {
            
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var additionalNauseaBonus = 0;

            var levelNauseaMax = transPlayer.CurrentStage switch 
                {
                    QuirkStage.Initial => 300, QuirkStage.Adequation => 500,
                    QuirkStage.Intermediate => 700, QuirkStage.Advanced => 900,
                    QuirkStage.Final => 1200, _ => 20
                };

            if (transPlayer.Nature == NatureType.HigherBrainPower)
            {
               additionalNauseaBonus = 300; 
            }
            

            NauseaMax = levelNauseaMax + additionalNauseaBonus;
        }

        public override void PostUpdateMiscEffects()
        {

            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            float ascendingSpeed = 1.5f;
            if (transPlayer.Nature == NatureType.Aerodynamic)
            {
                ascendingSpeed = 3.5f;
            }
        
            int floatingNpcCount = 0;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.GetGlobalNPC<Debuffs.ZeroGravityGlobalNPC>().hasZeroGravity)
                {
                    floatingNpcCount++;
                }
            }

        
            if (isZeroGravityActive || floatingNpcCount > 0)
            {

                if (!isAwaken)
                {
                  int nauseaRate = 0;
                if (isZeroGravityActive) nauseaRate += 1; 
                nauseaRate += floatingNpcCount;           

                Nausea += nauseaRate;  
                }
                
                

                
                if (isZeroGravityActive && !Player.mount.Active && Player.velocity.Y != 0)
                {
                    if (Player.controlJump) 
                {
                    Player.velocity.Y = -ascendingSpeed; 
                    Player.fallStart = (int)(Player.position.Y / 16f); 
                }
                else if(Player.controlDown)
                {
                    Player.velocity.Y = +3.5f; 
                }
                
                else if (Player.velocity.Y > 0)
                {
                    Player.velocity.Y *= 0.25f; 
                }

                    if (Main.rand.NextBool(4))
                        Dust.NewDust(Player.position, Player.width, Player.height, DustID.PinkFairy);
                }

            
                if (Nausea >= NauseaMax)
                {
                    
                    Player.ClearBuff(ModContent.BuffType<Buffs.ZeroGravityBuff>());
                    isZeroGravityActive = false;
                    
                
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.HasBuff(ModContent.BuffType<ZeroGravityBuff>()))
                        {
                            int buffIndex = npc.FindBuffIndex(ModContent.BuffType<ZeroGravityBuff>());
                            if (buffIndex != -1) npc.DelBuff(buffIndex);
                            npc.GetGlobalNPC<ZeroGravityGlobalNPC>().hasZeroGravity = false;
                        }
                    }

                
                    Player.AddBuff(BuffID.Confused, 240); 
                    Player.AddBuff(BuffID.Slow, 240);
                    
                
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath13, Player.position);
                }
            }
            else
            {
            
                if (Nausea > 0)
                {
                    Nausea -= 2; 
                    if (Nausea < 0) Nausea = 0;
                }
            }
        }

        public override void PostUpdate()
        {
            if (isAwaken)
            {
                overlayAutoAttackTimer++;

               
                if (overlayAutoAttackTimer >= 45)
                {
                    overlayAutoAttackTimer = 0;
                    ExecuteAwakenAttack();
                }
            }
        }

        private void ExecuteAwakenAttack()
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
            
            IClosestEnemyFinder finder = new TargetFinder();
            NPC target = finder.FindClosestEnemy(Player, 400f, false);

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
                    ModContent.ProjectileType<GravityBubbleProj>(), 
                    baseDamage, 
                    2f, 
                    Player.whoAmI,
                    hasTargetFlag 
                );
            }

            
           
        }
    }
}