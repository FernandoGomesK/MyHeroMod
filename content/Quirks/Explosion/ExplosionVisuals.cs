using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Humanizer;
using MyHeroMod.content.Dusts;

namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer
    {
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            

            if (IsClusterActive)
            {
    
                Lighting.AddLight(Player.Center, Color.Orange.ToVector3() * 0.8f);

    
                Vector2 randomPos = Player.Center + Main.rand.NextVector2Circular(20f, 20f);    
                if (Main.rand.NextBool(25)) 
                { 
                    int dust = Dust.NewDust(
                    randomPos, 
                    0, 0, 
                    ModContent.DustType<ClusterDust>(),
                    0f, 0f, 
                    0, default, 1.5f
                );
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity = Player.velocity;        
                }
                
            }
        }

        public override void PostUpdate()
        {
            if (!Player.GetModPlayer<TransformationPlayer>().HasActiveQuirk(QuirkType.Explosion))
            {
                return; 
            }   
            
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            // ===================Flight Effects========================================================

            if (mainPlayer.HasActiveQuirk(QuirkType.Explosion) && mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
            if (Player.velocity.Y != 0 && !Player.mount.Active)
                {
                    
                    if (Main.rand.NextBool(10)) 
                    {
                        int dustFire = Dust.NewDust(
                            Player.position + new Vector2(-5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Torch, 
                            0, 2f, 100, default, 3.5f 
                        );
                        int dustSmoke = Dust.NewDust(
                            Player.position + new Vector2(-5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Ash, 
                            0, 2f, 100, default, 3.5f 
                        );
                        Main.dust[dustFire].noGravity = true;
                        Main.dust[dustFire].velocity *= 0.5f; 
                        Main.dust[dustSmoke].noGravity = true;
                        Main.dust[dustSmoke].velocity *= 0.5f;
                    }

                
                    if (Main.rand.NextBool(10))
                    {
                        int dustFire2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / 2, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Torch, 
                            0, 2f, 100, default, 3.5f
                        );
                        int dustSmoke2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / -5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Ash, 
                            0, 2f, 100, default, 3.5f 
                        );
                        Main.dust[dustFire2].noGravity = true;
                        Main.dust[dustFire2].velocity *= 0.5f;
                        Main.dust[dustSmoke2].noGravity = true;
                        Main.dust[dustSmoke2].velocity *= 0.5f;
                    }
                
                    if (Main.rand.NextBool(6) && IsClusterActive)
                    {
                         int dustFire2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / 2, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            ModContent.DustType<ClusterDust>(), 
                            0, 2f, 100, default, 2.5f
                        );
                        int dustSmoke2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / -5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            ModContent.DustType<ClusterDust>(), 
                            0, 2f, 100, default, 2.5f 
                        );
                        Main.dust[dustFire2].noGravity = true;
                        Main.dust[dustFire2].velocity *= 0.5f;
                        Main.dust[dustSmoke2].noGravity = true;
                        Main.dust[dustSmoke2].velocity *= 0.5f;
                        
                    }
                }
        }
    }
    }   
}