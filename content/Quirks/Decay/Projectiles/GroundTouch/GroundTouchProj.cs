using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Debuffs;

namespace MyHeroMod.content.Quirks.Decay.Projectiles.GroundTouch
{

public class GroundTouchProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 60; // Espinho alto
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1; // Atravessa inimigos infinitos
            Projectile.timeLeft = 60;  // Dura 1 segundo
            Projectile.tileCollide = false; // Não colide, pois nasce DENTRO do chão
            Projectile.ignoreWater = true;
            Projectile.alpha = 255; // Começa invisível (opcional se tiver sprite)
        }

        public override void AI()
        {
            if (Projectile.ai[0] < 10)
            {
                Projectile.position.Y -= 4f; // Sobe 4 pixels por frame
                
                if (Projectile.alpha < 0) Projectile.alpha = 0;
                Projectile.ai[0]++;
            }

            for (int i = 0; i < 2; i++) 
            {
                // Pick a random X position within the width
                float randomX = Projectile.position.X + Main.rand.NextFloat(Projectile.width);
                
                // Force Y position to be at the bottom of the projectile
                float bottomY = Projectile.position.Y + Projectile.height;

                Vector2 dustPos = new Vector2(randomX, bottomY);

                // Wraith Dust
                int d1 = Dust.NewDust(dustPos, 1, 1, DustID.Wraith, 0f, 0f, 100, default, 2.5f);
                Main.dust[d1].noGravity = true;
                Main.dust[d1].velocity.Y = -Main.rand.NextFloat(2f, 5f); // Shoot UP
                Main.dust[d1].velocity.X *= 0.2f; // Don't move sideways much

                // Purple Dust
                if (Main.rand.NextBool(2)) // 50% chance for purple
                {
                    int d2 = Dust.NewDust(dustPos, 1, 1, DustID.PurpleTorch, 0f, 0f, 100, default, 2.0f);
                    Main.dust[d2].noGravity = true;
                    Main.dust[d2].velocity.Y = -Main.rand.NextFloat(3f, 6f); 
                    Main.dust[d2].velocity.X *= 0.2f;
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
    }
}


   