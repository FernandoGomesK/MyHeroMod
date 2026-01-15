using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace MyHeroMod.content.Quirks.OFA8th.Projectiles.DetroitSmash
{
    public class DetroitPunchProj : ModProjectile
    {
        private int targetID = -1;
        
        public override void SetDefaults()
        {
            
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 10;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
        }


         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            targetID = target.whoAmI;

            Projectile.Kill(); // Garante que exploda ao tocar inimigos
        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.ai[0] == 0){

            Player player = Main.player[Projectile.owner];
            Vector2 spawnVelocity = Projectile.oldVelocity;


            

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                spawnVelocity,
                ModContent.ProjectileType<PrimeDetroitSmashProj>(),
                200, 
                2f, 
                player.whoAmI,
                0,
                targetID);
            }

            
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f);
            }
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.5f);
            }
        }
    }
}