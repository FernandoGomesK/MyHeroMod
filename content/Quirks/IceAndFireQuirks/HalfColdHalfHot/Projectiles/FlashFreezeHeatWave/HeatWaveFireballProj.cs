using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.System.BaseIAFProjectiles.SimpleProjectiles.FireBall;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.FlashFreezeHeatWave
{
    public class HeatwaveFireBallProj : BaseFireBallProj
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";
        protected override float ExpansionRate => 4f; 

        public override void SetDefaults()
        {
           
            base.SetDefaults();
            
            Projectile.width = 120; 
            Projectile.height = 120;
            
            
            Projectile.localNPCHitCooldown = 20; 
        }
    }
}