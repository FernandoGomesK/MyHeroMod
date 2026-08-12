using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.JetKindlingProjs
{
  
    public class JetKindlingController : BaseTodorokiController
    {
        protected override int OuterDustType => DustID.Torch;
        protected override int CoreDustType => DustID.RedTorch;
        protected override int SparkDustType => DustID.FireworkFountain_Red;
        protected override Vector3 LightColor => new Vector3(1.2f, 0.6f, 0.3f);
        protected override int DebuffType => BuffID.OnFire3;
        protected override int ParticleType => ModContent.ProjectileType<JetKindlingEffectProj>();
    }

    public class JetPaleController : BaseTodorokiController
    {
        protected override int OuterDustType => DustID.BlueTorch;
        protected override int CoreDustType => DustID.Frost; 
        protected override int SparkDustType => DustID.FireworkFountain_Blue;
        protected override Vector3 LightColor => new Vector3(0.3f, 0.6f, 1.2f);
        protected override int DebuffType => BuffID.Frostburn;
        protected override int ParticleType => ModContent.ProjectileType<JetPaleEffectProj>();
    }

    public class JetIceController : BaseTodorokiController
    {
        protected override int OuterDustType => DustID.IceTorch;
        protected override int CoreDustType => DustID.Snow;
        protected override int SparkDustType => DustID.Ice;
        protected override Vector3 LightColor => new Vector3(0.6f, 0.9f, 1.2f);
        protected override int DebuffType => BuffID.Frostburn;
        protected override int ParticleType => ModContent.ProjectileType<JetIceEffectProj>();
    }
}