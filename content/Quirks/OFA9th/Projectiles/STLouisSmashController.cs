using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KhacesCore.Content.System.BaseProjectiles;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{
    
    public class STLouisSmashController : BaseJumpKickProj
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";
        protected override float DashSpeed => 20f;

        public override void SpawnHoverDust(Player player)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0, 0, 100, default, 1f);
            }
        }

        public override void SpawnDashDust(Player player)
        {
            
        }

        public override void SpawnExplosionDust(Vector2 position)
        {
            
            for (int i = 0; i < 50; i++)
            {
                int fire = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.GreenTorch, 0, 0, 100, default, 4f);
                Main.dust[fire].velocity *= 6f;
                Main.dust[fire].noGravity = true;

                int smoke = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 100, default, 3f);
                Main.dust[smoke].velocity *= 4f;
            }
        }
    }
}