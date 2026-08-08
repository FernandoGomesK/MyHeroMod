using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System.BaseProjectiles;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Frost.Projectiles
{
    public class FreezingIceBeamController : BaseLaserProj
    {
        
        protected override float MaxRange => 1200f;
        protected override float BeamWidth => 200f;
        protected override int DustType => DustID.SnowflakeIce;
        protected override float DustScale => 1.5f;
        protected override int HitCooldown => 15;

        protected override bool IsChannelingValid(Player player)
        {
            
            if (Projectile.ai[0] > 0)
            {
                Projectile.ai[0]--; 
                
                if (Projectile.ai[0] <= 0) 
                {
                    return false;
                }

            
                return player.active && !player.dead; 
            }

            return player.active && !player.dead && player.channel;
        }

    
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            target.AddBuff(BuffID.Frostburn, 300);
        }
    }
}