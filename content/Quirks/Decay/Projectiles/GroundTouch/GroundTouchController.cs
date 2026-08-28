using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.Decay.Projectiles.GroundTouch
{
    
    public class GroundTouchController : ModProjectile
    {
       public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = false;
            Projectile.hide = true;
            Projectile.timeLeft = 120; 
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            
            if (Projectile.timeLeft % 3 == 0)
            {
                
                Vector2 groundPos = Projectile.Center;
                
                
                bool foundGround = false;
                for (int y = 0; y < 20; y++)
                {
                    int tileX = (int)(groundPos.X / 16f);
                    int tileY = (int)(groundPos.Y / 16f);
                    
                 
                    Tile tile = Main.tile[tileX, tileY];

                    
                    bool isGround = tile.HasTile && (Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType]);

                    if (isGround) 
                    {
                        groundPos.Y = tileY * 16f;
                        foundGround = true;
                        if (ModContent.GetInstance<MyHeroConfig>().DecayDestroys)
                        {
                            WorldGen.KillTile(tileX, tileY, fail: false, effectOnly: false, noItem: false);

                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tileX, tileY);
                            }
                        }

                        break;
                    }
                    
                    groundPos.Y += 16f;
                }

               
                if (foundGround)
                {
                  
                    groundPos.Y -= 20f; 

                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        groundPos,
                        Vector2.Zero,
                        ModContent.ProjectileType<GroundTouchProj>(),
                        Projectile.damage,
                        Projectile.knockBack,
                        Projectile.owner
                    );
                }
            }
            
            
            Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
        }

       
        
       
        public override bool OnTileCollide(Vector2 oldVelocity) { return false; }
    }

    
}