using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueFlamethrower
{
    public class BlueFlamethrowerHitboxProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";
        public override void SetDefaults()
        {
            Projectile.width = 40; 
            Projectile.height = 40;
            
            Projectile.friendly = true; 
            Projectile.hostile = false; 
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 60; 
            
            Projectile.alpha = 255; 
            Projectile.ignoreWater = false; 
            Projectile.tileCollide = true; 
            
            Projectile.usesLocalNPCImmunity = true;
        
            Projectile.localNPCHitCooldown = 20; 
        }

        public override void AI()
        {
         
            for (int i = 0; i < 2; i++) 
            {
                int dustIndex = Dust.NewDust(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height, 
                    DustID.DungeonWater, 
                    Projectile.velocity.X * 0.2f, 
                    Projectile.velocity.Y * 0.2f, 
                    100, 
                    default, 
                    2f 
                );
                
                Main.dust[dustIndex].noGravity = true; 
                Main.dust[dustIndex].velocity *= 1.5f; 
                Main.dust[dustIndex].velocity += Projectile.velocity * 0.5f; 
            }

         
            Projectile.velocity *= 0.95f; 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
          
            target.AddBuff(BuffID.Frostburn, 180); 
        }
    }
}