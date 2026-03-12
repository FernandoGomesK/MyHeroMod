using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HeavenPiercingWall;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HCHellSpider;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.JetKindling;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.FlashFreezeHeatWave;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.GreatGlacialAegir;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.IceShot;


using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.IceThrower;
using MyHeroMod.content.System;



namespace MyHeroMod.content.Quirks.HalfColdHalfHot
{
    public partial class HalfColdHalfHotPlayer : ModPlayer, IQuirkResetter
    {
//                 

//         

//             switch (skill)
//             {
                    
//                     case QuirkSkills.HeavenPiercingWall:
//                     if (IsPhosphorActive)
//                     {
//                         DoGreatGlacialAegir(mainPlayer);
//                     }
//                     else{
//                         DoHeavenPiercingWall(mainPlayer);
//                     }
//                     break;


//                     case QuirkSkills.JetKindling:
//                     if (IsFlashFireFistActive)
//                     {
//                     DoJetKindling(mainPlayer); 
//                     }
//                     else{
//                         DoIceWave(mainPlayer);
//                     }
//                     break;

                    

//                     case QuirkSkills.HCHellSpider:
//                     if (IsFlashFireFistActive)
//                     {
//                     DoHellSpider(mainPlayer); 
//                     }
//                     else{
//                         DoIceSpike(mainPlayer);
//                     }
//                     break;

                

//                     case QuirkSkills.HCFireFist:

//                     ActivateFlashFireFist(mainPlayer);
//                     break;

//                     case QuirkSkills.HCPhosphor:

//                     ActivatePhosphor(mainPlayer);
//                     break;

//                     case QuirkSkills.FlashFreezeHeatWave:
//                     // AQUI ESTÁ A MUDANÇA:
//                     // Em vez de chamar a lógica inteira, nós apenas ATIVAMOS o modo automático.
//                     if (!IsFlashFreezeActive)
//                     {
//                         IsFlashFreezeActive = true;
//                         FlashFreezeTimer = 0;
//                         SetCooldown(skill, 20); // Exemplo de Cooldown
//                     }
//                     break;
                
                

//             }
//         }
//         private void SetCooldown(QuirkSkills skill, int timeInTicks)

//         {
//             if (SkillCooldowns.ContainsKey(skill))
//             {
//                 SkillCooldowns[skill] = timeInTicks;
//             }
//             else
//             {
//                 SkillCooldowns.Add(skill, timeInTicks);
//             }
//         }

//         private void DoIceSpike(TransformationPlayer mainPlayer)
//         {

//             int IceDamage = 40;
//             float multiplier = 1f;
            

             
//             if (IsSurgeArmGauntletsOn)
//             {
//                 multiplier += 1f;
//             }
//             int FinalDamage = (int)(IceDamage * multiplier); // IceDamage + multiplier;


//             Vector2 Velocity = Main.MouseWorld - Player.Center;
//             Velocity.Normalize();
//             Velocity *= 15f;

//             Projectile.NewProjectile(
//                 Player.GetSource_FromThis(),
//                 Player.Center,
//                 Velocity,
//                 ModContent.ProjectileType<IceShotProj>(),
//                 FinalDamage, 
//                 2f, 
//                 Player.whoAmI);

//                 temperature -= 25;
//         }


//         
            
        
//         

        private void UpdateFlashFreeze()
        {
            
            FlashFreezeTimer++;

            // Configuração da Posição (Costas do Player)
            float offsetCostas = 20f; 
            Vector2 spawnPos = Player.Center - new Vector2(offsetCostas * Player.direction, 0f);
            spawnPos.Y += Main.rand.NextFloat(-10f, 10f);

            // FASE 1: GELO (0 a 2 segundos)
            if (FlashFreezeTimer < 120)
            {
                // gelo no corpo todo
                
                Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.IceTorch, 0, 0, 100, default, 4.5f);
                d.noGravity = true;
                d.velocity *= 8f;   
                

                // Mais gelo nas costas
                int iceDust = Dust.NewDust(spawnPos, 4, 4, DustID.IceTorch, 0, 0, 100, default, 4.5f);
                Main.dust[iceDust].noGravity = true;
                Main.dust[iceDust].velocity = new Vector2(-5f * Player.direction, 0f);
                Player.velocity *= 0.1f; 
            }
            // FASE 2: FOGO E DISPARO (Após 2 segundos)
            else
            {
                Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Torch, 0, 0, 100, default, 3.5f);
                d.noGravity = true;
                d.velocity *= 6f;    


                int fireDust = Dust.NewDust(spawnPos, 4, 4, DustID.Torch, 0, 0, 100, default, 4.5f);
                Main.dust[fireDust].noGravity = true;
                Main.dust[fireDust].velocity = new Vector2(-6f * Player.direction, 0f);

                // O DISPARO (Acontece uma única vez no frame 120)
                if (FlashFreezeTimer == 120)
                {
                    

                    var transPlayer = Player.GetModPlayer<TransformationPlayer>();
                    
                        float multiplier = 1.0f;
                        if (IsSurgeArmGauntletsOn) multiplier += 0.5f;


                    if (Player.whoAmI == Main.myPlayer)
                    {
                        
                        
                        int iceDamage = 120;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Initial: iceDamage = 120; break;
                case QuirkStage.Adequation: iceDamage = 120; break;
                case QuirkStage.Intermediate: iceDamage = 250; break;
                case QuirkStage.Advanced: iceDamage = 550; break;
                case QuirkStage.Final: iceDamage = 1100; break;
            }
            int finalDamage = (int)(iceDamage * multiplier);
                        
                        int projType = ModContent.ProjectileType<FlashFreezeProj>(); 

                        Vector2 Velocity = Main.MouseWorld - Player.Center;
                        Velocity.Normalize();
                        Velocity *= 15f;
                        // int projType = ModContent.ProjectileType<FlashFreezeProj>(); 

                        Projectile.NewProjectile(
                            Player.GetSource_FromThis(),
                            Player.Center,
                            Velocity,
                            projType,   
                            finalDamage, 
                            5f, 
                            Player.whoAmI
                        );
                    }
                }

                // DESLIGA SOZINHO (Dá 10 frames de fogo extra e desliga)
                if (FlashFreezeTimer >= 130)
                {
                    IsFlashFreezeActive = false;
                    FlashFreezeTimer = 0;
                }
            }
        }
            
//         
        
        
//         

//         }
//         
            
//            
//         
//   
}}