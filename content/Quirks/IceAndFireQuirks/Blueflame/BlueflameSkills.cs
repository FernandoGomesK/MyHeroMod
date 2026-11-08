using Microsoft.Xna.Framework;
using MyHeroMod.content.Dusts;
using MyHeroMod.content.Projectiles.Blueflame;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame
{
    public partial class BlueflamePlayer : BaseIceAndFirePlayer
    {
        public void explode()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!mainPlayer.HasActiveQuirk(QuirkType.Blueflame)) return;


            int addition = currentExplosionCount switch
            {
                >= 75 => 8,
                >= 50 => 4,
                >= 25 => 2,
                _ => 1 
            };

            if (Main.GameUpdateCount % 60 == 0)
            {
                if (isConcentrating && Temperature >= (MaxTemperature / 2))
                {
                    if (currentExplosionCount < ExplosionLimit)
                    {
                        currentExplosionCount += addition;
                    }

                    if (currentExplosionCount >= ExplosionLimit)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<BlueflameMassiveExplosion>(), 20000, 10f, Player.whoAmI);
                        currentExplosionCount = 0;
                        Temperature = 0;
                    }
                }
                else
                {
                    if (currentExplosionCount > 0)
                    {
                        currentExplosionCount--;
                    }
                }
            }
        }
    }
}