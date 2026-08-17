using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using Terraria.Graphics.CameraModifiers;
using MyHeroMod.content.Debuffs;
using System;

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
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            

            
            if (player.dead || !player.active || player.HasBuff(BuffID.Blackout) || opticPlayer.isBlockingEyes() || player.HasBuff(ModContent.BuffType<Heatstroke>()) || !transPlayer.HasActiveQuirk(QuirkType.OpticBlast))
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

            float visionLength = 1000f; 
            float visionWidth = 100f; 

            Vector2 startPoint = player.Center;
            Vector2 endPoint = player.Center + (Projectile.velocity * visionLength);

            if (Main.GameUpdateCount % 4 == 0)
            {
                
                PunchCameraModifier rumble = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 3f, 6f, 5, 1000f, "OpticBlastRumble");
                Main.instance.CameraModifiers.Add(rumble);
            }

           
            
            
            Vector2 perpendicular = new Vector2(-Projectile.velocity.Y, Projectile.velocity.X);
            bool isPink = (transPlayer.CurrentVariant == QuirkVariant.PinkBeam);

            // Determine dust types based on variant
            int coreDustType = isPink ? DustID.PinkTorch : DustID.RedTorch;
            int unstableDustType = isPink ? DustID.PinkFairy : DustID.VampireHeal;

            
            for (int i = 0; i < 15; i++)
            {
                float lengthOffset = Main.rand.NextFloat(0, visionLength);
                float widthOffset = Main.rand.NextFloat(-visionWidth / 2.5f, visionWidth / 2.5f);
                
                Vector2 dustPos = startPoint + (Projectile.velocity * lengthOffset) + (perpendicular * widthOffset);

                Dust beamDust = Dust.NewDustPerfect(dustPos, coreDustType, Vector2.Zero);
                beamDust.noGravity = true;
                beamDust.scale = Main.rand.NextFloat(1.5f, 3f); 
                beamDust.velocity = Projectile.velocity * Main.rand.NextFloat(2f, 6f); 
                
                
                if (i % 3 == 0)
                {
                    if (isPink) Lighting.AddLight(dustPos, 0.8f, 0.2f, 0.6f);
                    else Lighting.AddLight(dustPos, 0.8f, 0.1f, 0.1f);
                }
            }

            
            for (int i = 0; i < 10; i++)
            {
                float lengthOffset = Main.rand.NextFloat(0, visionLength);
                float widthOffset = Main.rand.NextFloat(-visionWidth / 1.5f, visionWidth / 1.5f); // Wider spread
                
                Vector2 dustPos = startPoint + (Projectile.velocity * lengthOffset) + (perpendicular * widthOffset);

                Dust darkDust = Dust.NewDustPerfect(dustPos, unstableDustType, Vector2.Zero);
                darkDust.noGravity = true;
                darkDust.scale = Main.rand.NextFloat(1.2f, 2f); 
                
                
                float pushDirection = Math.Sign(widthOffset); 
                darkDust.velocity = (perpendicular * pushDirection * Main.rand.NextFloat(2f, 7f)) + (Projectile.velocity * Main.rand.NextFloat(1f, 3f)); 
            }

           
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.friendly || npc.townNPC) continue;

                float collisionPoint = 0f;
            
                if (Collision.CheckAABBvLineCollision(npc.position, npc.Size, startPoint, endPoint, visionWidth, ref collisionPoint))
                {
                    if (Collision.CanHitLine(player.position, player.width, player.height, npc.position, npc.width, npc.height))
                    {
                        if (Main.rand.NextBool(10)) 
                        {
                            npc.SimpleStrikeNPC(Projectile.damage, player.direction, false, 0f, DamageClass.Generic, true, player.luck);
                        }
                    }
                }
            }
        }
    }
}