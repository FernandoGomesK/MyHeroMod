using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.System.BaseProjectiles
{
    public abstract class BaseGroundWaveController : ModProjectile
    {
    
        protected abstract int ProjectileToSpawn { get; }
        protected virtual int PlacementCooldown => 3;
        protected virtual float VerticalOffset => 20f; 

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = false;
            Projectile.hide = true; 
            Projectile.timeLeft = 60; 
            Projectile.tileCollide = true; 
        }

        public override void AI()
        {
            if (Projectile.timeLeft % PlacementCooldown == 0)
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
                        break; 
                    }
                    groundPos.Y += 16f;
                }

                if (foundGround)
                {
                    groundPos.Y -= VerticalOffset; 

                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        groundPos,
                        Vector2.Zero, 
                        ProjectileToSpawn,
                        Projectile.damage,
                        Projectile.knockBack,
                        Projectile.owner
                    );
                }
            }
            
            
            Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
        }

        public override bool OnTileCollide(Vector2 oldVelocity) 
        { 
            
            Projectile.velocity.X = 0;
            return false; 
        }
    }
}