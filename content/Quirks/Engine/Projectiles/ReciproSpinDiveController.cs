using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KhacesCore.Content.System.BaseProjectiles;

namespace MyHeroMod.content.Quirks.Engine.Projectiles
{
   
    public class ReciproSpinDiveController : BaseJumpSpinKickProj
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
            
            player.GetModPlayer<EnginePlayer>().SpawnEngineDust(1.5f);
        }

        public override void SpawnExplosionDust(Vector2 position)
        {
            
            for (int i = 0; i < 50; i++)
            {
                
                int fire = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.BlueTorch, 0, 0, 100, default, 4f);
                Main.dust[fire].velocity *= 6f;
                Main.dust[fire].noGravity = true;

                int smoke = Dust.NewDust(position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 100, default, 3f);
                Main.dust[smoke].velocity *= 4f;
            }
        }
    }
}