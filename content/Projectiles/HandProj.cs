using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace MyHeroMod.content.Projectiles
{
    public class HandProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj";
        public override void SetDefaults()
        {
            Projectile.width = 32; 
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 120; 
            Projectile.alpha = 255; 
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }       

        public override void OnKill(int timeLeft)
        {
            
        }   
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            
            

        }

        
    }

    
}