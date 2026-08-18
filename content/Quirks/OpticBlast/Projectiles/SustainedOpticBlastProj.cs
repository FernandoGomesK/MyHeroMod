using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.CameraModifiers; 
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OpticBlast;
using KhacesCore.Content.System;

namespace MyHeroMod.content.Quirks.OpticBlast.Projectiles
{ 
    public class SustainedOpticBlastProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj"; // Invisible texture

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

            bool isPink = (transPlayer.CurrentVariant == QuirkVariant.Variant1);

            int coreDustType = isPink ? DustID.PinkTorch : DustID.RedTorch;
            int unstableDustType = isPink ? DustID.PinkFairy : DustID.VampireHeal;

            // 1. KILL CONDITIONS (Dead, inactive, or out of energy)
            if (player.dead || !player.active || opticPlayer.CurrentOpticBlast <= 0)
            {
                Projectile.Kill();
                return;
            }

            // 2. HOLD CONDITION (Check if they are still holding a skill key)
            bool isHolding = CoreKeybinds.SkillSlot1.Current || CoreKeybinds.SkillSlot2.Current || CoreKeybinds.SkillSlot3.Current || CoreKeybinds.SkillSlot4.Current;
            if (!isHolding)
            {
                Projectile.Kill();
                return;
            }

            // 3. UPDATE POSITION AND ROTATION
            Projectile.Center = player.Center;
            Projectile.timeLeft = 2; // Keeps it alive as long as AI is running

            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.velocity = diff;
                
                Projectile.rotation = Projectile.velocity.ToRotation();
                player.ChangeDir(Main.MouseWorld.X > player.Center.X ? 1 : -1); 
                Projectile.netUpdate = true;
            }

            // 4. DRAIN THE OPTIC BLAST GAUGE
            // Drains 1 point of energy every 3 frames (so 100 energy lasts 5 seconds)
            if (Main.GameUpdateCount % 3 == 0)
            {
                opticPlayer.CurrentOpticBlast--;
                opticPlayer.regenTimer = -60; // Pauses regeneration for 1 second after shooting!
            }

            // 5. CAMERA RUMBLE
            if (Main.GameUpdateCount % 4 == 0)
            {
                PunchCameraModifier rumble = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 3f, 6f, 5, 1000f, "OpticBlastRumble");
                Main.instance.CameraModifiers.Add(rumble);
            }

            // 6. THICK BEAM VISUALS
            float visionLength = 600f; 
            float visionWidth = 100f; 

            Vector2 startPoint = player.Center;
            Vector2 endPoint = player.Center + (Projectile.velocity * visionLength);
            Vector2 perpendicular = new Vector2(-Projectile.velocity.Y, Projectile.velocity.X);

            for (int i = 0; i < 15; i++)
            {
                float lengthOffset = Main.rand.NextFloat(0, visionLength);
                float widthOffset = Main.rand.NextFloat(-visionWidth / 2f, visionWidth / 2f);
                
                Vector2 dustPos = startPoint + (Projectile.velocity * lengthOffset) + (perpendicular * widthOffset);

                Dust beamDust = Dust.NewDustPerfect(dustPos, coreDustType, Vector2.Zero);
                beamDust.noGravity = true;
                beamDust.scale = Main.rand.NextFloat(1.5f, 3f); 
                beamDust.velocity = Projectile.velocity * Main.rand.NextFloat(1f, 4f); 
            }

            // 7. HITSCAN DAMAGE
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.friendly || npc.townNPC) continue;

                float collisionPoint = 0f;
            
                if (Collision.CheckAABBvLineCollision(npc.position, npc.Size, startPoint, endPoint, visionWidth, ref collisionPoint))
                {
                    if (Collision.CanHitLine(player.position, player.width, player.height, npc.position, npc.width, npc.height))
                    {
                        if (Main.rand.NextBool(3)) // Hits every ~3 frames
                        {
                            
                            float multiplier = GetDamageMultiplier(opticPlayer.CurrentPercentage);
                            int finalDamage = (int)(Projectile.damage * multiplier);

                            npc.SimpleStrikeNPC(finalDamage, player.direction, false, 0f, DamageClass.Generic, true, player.luck);
                        }
                    }
                }
            }
        }

        
        private float GetDamageMultiplier(OpticBlastPlayer.Percentage percentage)
        {
            switch (percentage)
            {
                case OpticBlastPlayer.Percentage.Zero: return 0f;
                case OpticBlastPlayer.Percentage.TwentyFive: return 0.25f;
                case OpticBlastPlayer.Percentage.Fifty: return 0.50f;
                case OpticBlastPlayer.Percentage.SeventyFive: return 0.75f;
                case OpticBlastPlayer.Percentage.Full: return 1.0f;
                default: return 0f;
            }
        }
    }
}