using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System.BaseProjectiles;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.ProminenceBurn
{
    public class RemadeProminenceBurnController : BaseLaserProj
    {
        
        protected override float MaxRange => 1200f;
        protected override float BeamWidth => 40f; 
        protected override int DustType => DustID.RedTorch;
        protected override float DustScale => 1.5f;
        protected override int HitCooldown => 15;

        protected override bool IsChannelingValid(Player player)
        {
            
            return player.active && !player.dead && player.channel;
        }

    
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            target.AddBuff(BuffID.Frostburn, 300);
        }
    }
}