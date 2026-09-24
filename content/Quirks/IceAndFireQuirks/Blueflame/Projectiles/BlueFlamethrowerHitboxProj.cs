using Terraria.ID;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles
{
    public class BlueFlamethrowerHitboxProj : BaseFlameHitbox
    {
        protected override int FlameDustType => DustID.BlueTorch;
        protected override int SparkDustType => DustID.FireworkFountain_Blue;

        protected override int FlameDebuff => BuffID.OnFire3;
    }
}