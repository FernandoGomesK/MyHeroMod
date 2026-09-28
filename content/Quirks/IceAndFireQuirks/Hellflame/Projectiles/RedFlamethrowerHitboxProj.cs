using Terraria.ID;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HellFlames.Projectiles
{
    public class RedFlamethrowerHitboxProj : BaseFlameHitbox
    {
        protected override int FlameDustType => DustID.Torch;
        protected override int SparkDustType => DustID.FireworkFountain_Red;

        protected override int FlameDebuff => BuffID.OnFire;
    }
}