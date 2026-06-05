using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.OpticBlast.Projectiles
{
    public class ContinuousOpticBlastController : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj"; 

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.hide = true; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();

            
            if (player.dead || !player.active || opticPlayer.isRubyGlassesEquipped)
            {
                Projectile.Kill();
                return;
            }

            
            Projectile.Center = player.Center;
            Projectile.timeLeft = 2;

            
            if (Projectile.owner == Main.myPlayer)
            {
                
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.velocity = diff;
                
                
                Projectile.rotation = Projectile.velocity.ToRotation();
                
                
                player.ChangeDir(Main.MouseWorld.X > player.Center.X ? 1 : -1); 
                Projectile.netUpdate = true;
            }

            
            float visionLength = 600f; 
            float visionWidth = 100f; 

            
            Vector2 startPoint = player.Center;
            Vector2 endPoint = player.Center + (Projectile.velocity * visionLength);

            
            if (Main.rand.NextBool(3))
            {
                Dust.NewDustPerfect(startPoint + (Projectile.velocity * Main.rand.NextFloat(0, visionLength)), DustID.RedTorch, Vector2.Zero).noGravity = true;
            }

            
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.friendly || npc.townNPC) continue;

                
                float collisionPoint = 0f;
                
            
                if (Collision.CheckAABBvLineCollision(npc.position, npc.Size, startPoint, endPoint, visionWidth, ref collisionPoint))
                {
                    
                    if (Collision.CanHitLine(player.position, player.width, player.height, npc.position, npc.width, npc.height))
{
                    
                    npc.SimpleStrikeNPC(Projectile.damage, player.direction, false, 0f, DamageClass.Generic, true, player.luck);
                    
}
                }
            }
        }
    }
}