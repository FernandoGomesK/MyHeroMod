using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using Terraria.Graphics.CameraModifiers;
using MyHeroMod.content.Debuffs;

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
            

            
            if (player.dead || !player.active || player.HasBuff(BuffID.Darkness) || opticPlayer.isRubyGlassesEquipped || player.HasBuff(ModContent.BuffType<Heatstroke>()) || !transPlayer.HasActiveQuirk(QuirkType.OpticBlast))
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

            // --- THE THICK BEAM DUST LOGIC ---
            
            
            Vector2 perpendicular = new Vector2(-Projectile.velocity.Y, Projectile.velocity.X);

            // 2. Spawn multiple dust particles per frame (15 per frame = 900 per second!)

            for (int i1 = 0; i1 < 5; i1++)
            {
                // Pick a random distance along the 600-pixel length
                float lengthOffset = Main.rand.NextFloat(0, visionLength);
                
                // Pick a random distance along the 100-pixel width (-50 to +50)
                float widthOffset = Main.rand.NextFloat(-visionWidth / 1f, visionWidth / 1f);
                
                // Combine them to get the exact spawn position
                Vector2 dustPos = startPoint + (Projectile.velocity * lengthOffset) + (perpendicular * widthOffset);

                Dust beamDust = Dust.NewDustPerfect(dustPos, DustID.RedTorch, Vector2.Zero);
                beamDust.noGravity = true;
                beamDust.scale = Main.rand.NextFloat(1.5f, 3f); // Make the dust randomly large and chunky
                
                // Optional: Give the dust a tiny bit of forward velocity so the beam looks like it's flowing
                beamDust.velocity = Projectile.velocity * Main.rand.NextFloat(1f, 4f); 
            }


            for (int i2 = 0; i2 < 15; i2++)
            {
                
                float lengthOffset = Main.rand.NextFloat(0, visionLength);
                
                
                float widthOffset = Main.rand.NextFloat(-visionWidth / 2f, visionWidth / 2f);
                
                
                Vector2 dustPos = startPoint + (Projectile.velocity * lengthOffset) + (perpendicular * widthOffset);

                Dust beamDust = Dust.NewDustPerfect(dustPos, DustID.RedTorch, Vector2.Zero);
                beamDust.noGravity = true;
                beamDust.scale = Main.rand.NextFloat(1.5f, 3f); 
                
                
                beamDust.velocity = Projectile.velocity * Main.rand.NextFloat(1f, 4f); 
            }

            // --- COLLISION AND DAMAGE LOGIC ---
            
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
        }}}