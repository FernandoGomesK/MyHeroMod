using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; 
using KhacesCore.Content.System.BaseProjectiles;

namespace MyHeroMod.content.Quirks.OpticBlast.Projectiles
{
    public class OpticBlastProj : BaseSimpleProj
    {
        public override void AI()
        {
            base.AI(); 

            Player player = Main.player[Projectile.owner];
            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
            
            
           bool isPink = (transPlayer.CurrentVariant == QuirkVariant.PinkBeam);

           int coreDustType = isPink ? DustID.PinkTorch : DustID.RedTorch;
            int unstableDustType = isPink ? DustID.PinkFairy : DustID.VampireHeal;

            if (isPink)
            {
                Lighting.AddLight(Projectile.Center, 0.8f, 0.2f, 0.6f); 
            }
        
            else
            {
                Lighting.AddLight(Projectile.Center, 0.8f, 0.1f, 0.1f); 
            }
            

            
            if (Main.rand.NextBool(2))
            {
                Dust coreDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, coreDustType, 0f, 0f, 100, default, 1.5f);
                coreDust.noGravity = true; 
                coreDust.velocity = Projectile.velocity * 0.3f; 
            }

            
            if (Main.rand.NextBool(3))
            {
                Dust darkDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, unstableDustType, 0f, 0f, 100, default, 1.3f);
                darkDust.noGravity = true;
                
            
                darkDust.velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(40)) * 0.4f;
            }
        }
    }
}