using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles;
using Terraria;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueVanishingFist
{
   public class VanishingFistProj : BaseDashProj
    {
     
        protected override float DashSpeed => 35f; 
        protected override int DashDuration => 15; 
        protected override float HitboxThickness => 90f;

        protected override void SpawnDashVisuals(Player player)
        {
            
            int trailDust = Dust.NewDust(player.position, player.width, player.height, DustID.FireworkFountain_Blue, 0f, 0f, 100, default, 1.5f);
            Main.dust[trailDust].noGravity = true;
            Main.dust[trailDust].velocity *= 0.2f;

            
            if (Projectile.ai[0] == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.DungeonWater, 0, 0, 100, default, 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 3f;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
          
            target.AddBuff(BuffID.Frostburn, 180);
        }
    }
}