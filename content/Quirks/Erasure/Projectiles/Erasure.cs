using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.Erasure.Projectiles
{
    public class ErasureController : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj"; 

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.hide = true; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var erasurePlayer = player.GetModPlayer<ErasurePlayer>();

            
            if (player.dead || !player.active || !player.HasBuff(ModContent.BuffType<ErasingBuff>()))
            {
                Projectile.Kill();
                return;
            }

            
            Projectile.Center = player.Center;
            Projectile.timeLeft = 2;

            
            if (Projectile.owner == Main.myPlayer)
            {
                // Descobre a direção do mouse
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.velocity = diff;
                
                
                Projectile.rotation = Projectile.velocity.ToRotation();
                
                
                player.ChangeDir(Main.MouseWorld.X > player.Center.X ? 1 : -1); 
                Projectile.netUpdate = true;
            }

            
            float visionLength = 600f; 
            float visionWidth = 100f; 

            
            Vector2 startPoint = player.Center;
            Vector2 endPoint = player.Center + (Projectile.velocity * visionLength);

            
            // if (Main.rand.NextBool(3))
            // {
            //     Dust.NewDustPerfect(startPoint + (Projectile.velocity * Main.rand.NextFloat(0, visionLength)), DustID.RedTorch, Vector2.Zero).noGravity = true;
            // }

            
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.friendly || npc.townNPC) continue;

                
                float collisionPoint = 0f;
                
            
                if (Collision.CheckAABBvLineCollision(npc.position, npc.Size, startPoint, endPoint, visionWidth, ref collisionPoint))
                {
                    
                    if (Collision.CanHitLine(player.position, player.width, player.height, npc.position, npc.width, npc.height))
                    {
                        
                        var globalNPC = npc.GetGlobalNPC<QuirkGlobalNPC>();
                        if (globalNPC.HasQuirk)
                        {
                            
                            globalNPC.ErasureTimer = 10; 
                            
                            if (Main.rand.NextBool(10))
                            {
                                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.RedMoss); 
                                d.velocity *= 0.5f;
                                d.noGravity = true;
                                
                            }
                        }
                    }
                }
            }
        }
    }
}