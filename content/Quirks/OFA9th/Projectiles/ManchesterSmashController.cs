using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KhacesCore.Content.System.BaseProjectiles;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
   
    public class ManchesterSmashController : BaseJumpSpinKickProj
    {
        
        protected override float DashSpeed => Projectile.ai[1] == 1f ? 40f : 25f;

        public override void SpawnHoverDust(Player player)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0, 0, 100, default, 1f);
            }
        }

        public override void SpawnDashDust(Player player)
        {
            
            if (Projectile.ai[1] == 1f)
            {
                int d = Dust.NewDust(player.position, player.width, player.height, DustID.RedTorch, 0, 0, 100, Color.Red, 4f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.5f;
            }
        }

        public override void SpawnExplosionDust(Vector2 position)
        {
            
            if (Projectile.ai[1] == 1f)
            {
                int smoke = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.RedTorch, 0, 0, 100, default, 3f);
                Main.dust[smoke].velocity *= 4f;
            }

           
            for (int i = 0; i < 50; i++)
            {
                int fire = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.Electric, 0, 0, 100, Color.Green, 4f);
                Main.dust[fire].velocity *= 6f;
                Main.dust[fire].noGravity = true;

                int smoke = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 100, default, 3f);
                Main.dust[smoke].velocity *= 4f;
            }
        }
    }
}