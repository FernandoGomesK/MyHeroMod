using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;
using MyHeroMod.content.System.BaseProjectiles;
using MyHeroMod.content.System.BaseIAFProjectiles.SimpleProjectiles.FireBall;


namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueFireball
{
    public class BlueFireBallProj : BaseFireBallProj
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";

        protected override int MainDustType
        {
            get
            {
                Player player = Main.player[Projectile.owner];
                var transPlayer = player.GetModPlayer<TransformationPlayer>();

                if (transPlayer.CurrentStage >= QuirkStage.Adequation)
                {
                    return DustID.DungeonWater; 
                }
                
                
                return DustID.Torch; 
            }
        }

              protected override int KillDustType => MainDustType; 
        
          protected override int HitDebuff => BuffID.Frostburn; 
    }
}