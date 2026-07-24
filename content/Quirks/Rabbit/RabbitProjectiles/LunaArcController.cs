using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KhacesCore.Content.System.BaseProjectiles;


namespace MyHeroMod.content.Quirks.Rabbit.Projectiles
{

    public class LunaArcController : BaseJumpSpinKickProj
    {
        
         
        protected override float DashSpeed => 25f;       
        protected override float JumpPower => -15f;     
        protected override int HoverFrames => 15;        
        protected override int DustType => DustID.YellowStarDust; 
        
        
          

        public override void SpawnHoverDust(Player player)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.YellowStarDust, 0, 0, 100, default, 1f);
            }
        }

        

        public override void SpawnExplosionDust(Vector2 position)
        {
            
            for (int i = 0; i < 50; i++)
            {
                
                int fire = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.YellowTorch, 0, 0, 100, default, 4f);
                Main.dust[fire].velocity *= 6f;
                Main.dust[fire].noGravity = true;

                int smoke = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.YellowStarDust, 0, 0, 100, default, 3f);
                Main.dust[smoke].velocity *= 4f;
            }
        }
    }
}