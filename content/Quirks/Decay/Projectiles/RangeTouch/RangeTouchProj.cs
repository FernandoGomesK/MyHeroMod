using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;

namespace MyHeroMod.content.Quirks.Decay.Projectiles.RangeTouch
{
    public class RangeTouchProj : ModProjectile
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

        public override void OnKill(int timeLeft)
        {
            
        }   
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (transPlayer.HasActiveQuirk(QuirkType.Decay))
            {
                if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Wraith);
            }
            }
            
            

        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            
            target.AddBuff(ModContent.BuffType<DecayBuff>(), 300);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            target.AddBuff(ModContent.BuffType<DecayBuff>(), 300);
        }

        // public override void OnTileCollide(Vector2 oldVelocity)
        // {
        //     Projectile.Kill();
        // 

        
    }

    
}