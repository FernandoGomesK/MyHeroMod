using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using MyHeroMod.Buffs;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.ZeroGravity.Projectiles.GravityTouch
{
    public class GravityTouchProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj";
        
        public override void SetDefaults()
        {
            Projectile.width = 32; 
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 120; 
            Projectile.alpha = 255; 
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }       

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (transPlayer.HasActiveQuirk(QuirkType.ZeroGravity))
            {
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkFairy);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            target.AddBuff(ModContent.BuffType<ZeroGravityBuff>(), 300);
        }
    }
}
