using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Debuffs; 

namespace MyHeroMod.content.Quirks.Erasure.Projectiles
{
    public class NPCErasureProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/NPCErasureProj";
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = false;
            Projectile.hostile = true; 
            Projectile.tileCollide = true; 
            Projectile.timeLeft = 600;
            Projectile.alpha = 255; 
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            
            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch);
                d.noGravity = true;
                d.velocity *= 0.1f;
            }
        }

        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            
            target.AddBuff(ModContent.BuffType<QuirkErased>(), 300);
        }
    }
}