using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;

using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.ZeroGravity.Projectiles.GravityBubble
{
    public class GravityBubbleProj : ModProjectile
    {
       
        
        public override void SetDefaults()
        {
            Projectile.width = 16; 
            Projectile.height = 16;
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

           
            Vector2 targetCenter = Vector2.Zero;
            bool hasTarget = false;
            float closestDistance = 400f;

          
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && !npc.friendly && npc.CanBeChasedBy())
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetCenter = npc.Center;
                        hasTarget = true;
                    }
                }
            }

            if (hasTarget)
            {
                Vector2 targetDirection = (targetCenter - Projectile.Center).SafeNormalize(Vector2.Zero);
                float speed = Projectile.velocity.Length();
                if (speed < 8f) speed = 8f;

                
                Projectile.velocity = Vector2.Normalize(Vector2.Lerp(Projectile.velocity, targetDirection * speed, 0.15f)) * speed;
            }

            
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
           target.AddBuff(ModContent.BuffType<ZeroGravityEnemyBuff>(), 300);
        }
    }
}
