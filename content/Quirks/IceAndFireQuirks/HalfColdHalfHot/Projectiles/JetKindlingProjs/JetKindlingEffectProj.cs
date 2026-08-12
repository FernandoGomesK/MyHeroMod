using Terraria.ID;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.JetKindlingProjs
{
    
    public class JetKindlingEffectProj : BaseTodorokiEffectProj
    {
        protected override int EffectDustType => DustID.FireworkFountain_Yellow;
        protected override int DebuffType => BuffID.OnFire3;
    }

    
    public class JetPaleEffectProj : BaseTodorokiEffectProj
    {
        protected override int EffectDustType => DustID.FireworkFountain_Blue;
        protected override int DebuffType => BuffID.Frostburn;
    }

    public class JetIceEffectProj : BaseTodorokiEffectProj
    {
        
        protected override int EffectDustType => DustID.Ice;
        protected override int DebuffType => BuffID.Frostburn;
    }
}