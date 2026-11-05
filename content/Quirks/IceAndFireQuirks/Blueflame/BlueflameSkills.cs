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

            if (isConcentrating)
            {
                if (Temperature >= (MaxTemperature / 2))
                {
                    if (Main.GameUpdateCount % 60 == 0)
                    {
                        if (currentExplosionCount < ExplosionLimit)
                        {
                            currentExplosionCount++;
                        }
                        if (currentExplosionCount > ExplosionLimit)
                        {
                            currentExplosionCount = ExplosionLimit;
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
                        if (Main.GameUpdateCount % 60 == 0)
                        {
                            if (currentExplosionCount > 0)
                            {
                                currentExplosionCount--;
                            }
                            if (currentExplosionCount < 0)
                            {
                                currentExplosionCount = 0;
                            }
                        }
                    }
                }
            }
        }

      
    }
}