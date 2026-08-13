using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Projectiles.GreyOnomatopoeias;
using MyHeroMod.content.Projectiles;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.JetKindlingProjs
{

    public class JetKindlingCharge : BaseTodorokiCharge
    {
        protected override int OuterDustType => DustID.Torch;
        protected override int CoreDustType => DustID.RedTorch;
        protected override int SparkDustType => DustID.FireworkFountain_Red;
        protected override int BeamProjectileType => ModContent.ProjectileType<JetKindlingController>();
        protected override int OnomatopoeiaType => ModContent.ProjectileType<KindlingOnomatopoeia>();
        protected override string SoundStylePath => "MyHeroMod/Assets/Sounds/CremationSound"; 
        protected override Vector3 LightColor => new Vector3(1.2f, 0.6f, 0.3f);
        protected override Color ImpactColor => Color.Orange;
         protected override int removeHeatVar => 0;
        protected override int addHeatVar => 0;
    }

    public class JetPaleCharge : BaseTodorokiCharge
    {
        protected override int OuterDustType => DustID.BlueTorch;
        protected override int CoreDustType => DustID.Frost;
        protected override int SparkDustType => DustID.FireworkFountain_Blue;
        protected override int BeamProjectileType => ModContent.ProjectileType<JetPaleController>();
        protected override int OnomatopoeiaType => ModContent.ProjectileType<GreyBurnOnomatopoeia>();
        protected override string SoundStylePath => "MyHeroMod/Assets/Sounds/CremationSound";
        protected override Vector3 LightColor => new Vector3(0.3f, 0.6f, 1.2f);
        protected override Color ImpactColor => Color.DeepSkyBlue;
        protected override int addHeatVar => 35;
        protected override int removeHeatVar => 0;
    }

    public class JetIceCharge : BaseTodorokiCharge
    {
        protected override int OuterDustType => DustID.IceTorch;
        protected override int CoreDustType => DustID.Snow;
        protected override int SparkDustType => DustID.Ice;
        protected override int BeamProjectileType => ModContent.ProjectileType<JetIceController>();
        protected override int OnomatopoeiaType => -1;
        protected override string SoundStylePath => "MyHeroMod/Assets/Sounds/TodorokiIce"; 
        protected override Vector3 LightColor => new Vector3(0.6f, 0.9f, 1.2f);
        protected override Color ImpactColor => Color.Cyan;
        protected override int removeHeatVar => 20;
        protected override int addHeatVar => 0;
    }
}