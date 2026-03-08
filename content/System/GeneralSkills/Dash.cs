using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.System.BasePlayer;

namespace MyHeroMod.content.System
{
    public class DashSkill : QuirkSkill
    {
        public override string Name => "Dash";
        public override string Description => "A quick burst of speed in any direction.";
        public override string IconPath => "MyHeroMod/Assets/Skills/Dash";
        public override int BaseCooldown => 120;
       
        public override QuirkType RequiredQuirk => QuirkType.Quirkless;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        
        public override bool IsDefaultSkill => true;
        public override bool IsBaseQuirk => false;
        

        public override bool CheckUnlock(TransformationPlayer player)
        {
            return true; 
        }   
    

        public override void OnUse(Player player)
        {
            float speed = 14f;
            bool isEnhanced = false;
            bool hideNormalDash = false;
            var explosionColor = Color.Yellow;

            // Main.NewText("SKILL EXECUTADA!");
            

            
    
        foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroDashModifier dashModifier) 
                {
                    dashModifier.ModifyDash(ref speed, ref isEnhanced, ref hideNormalDash, ref explosionColor);
                }
            }

        if (hideNormalDash)
            {
                TeleportDash(player, explosionColor);
                Main.NewText("Teleporte!");
            }

            else
            {
                executeDash(player, speed, isEnhanced);
        player.SetImmuneTimeForAllTypes(10);
            }

        
        }


        
        private void executeDash(Player player, float speed, bool isEnhanced)
        {
            Vector2 dashDirection = Main.MouseWorld - player.Center;
            if (dashDirection != Vector2.Zero)
            {
                dashDirection.Normalize();
                player.velocity = dashDirection * speed;
            }
            // 3.Efeitos Visuais (VFX)
            ApplyFajinVfx(player, isEnhanced);
            
            player.SetImmuneTimeForAllTypes(10);
        }
        public void TeleportDash(Player player, Color explosionColor) => ApplyDashMovement(player, explosionColor);

        private void ApplyDashMovement(Player player, Color explosionColor)
        {
            Vector2 targetPos = Main.MouseWorld;
                Vector2 dir = targetPos - player.Center;
                float distance = dir.Length();
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.15f }, player.position);
                float maxDist = 600f;
                if (distance > maxDist)
                {
                    dir.Normalize();
                    dir *= maxDist;
                    distance = maxDist;
                }
    
                Vector2 safePos = player.Center;
                float stepSize = 16f; 
                bool hitWall = false;

                for (float i = 0; i < distance; i += stepSize)
                {
                    Vector2 checkPos = player.Center + Vector2.Normalize(dir) * i;
                    
                    
                    if (Collision.SolidCollision(checkPos - new Vector2(player.width/2, player.height/2), player.width, player.height))
                    {
                        hitWall = true;
                        break; 
                    }
                    safePos = checkPos; 
                }

                Vector2 startPos = player.Center;
                int dustCount = (int)(Vector2.Distance(startPos, safePos) / 5); 
                for (int i = 0; i < dustCount; i++)
                {
                    Vector2 dustPos = Vector2.Lerp(startPos, safePos, (float)i / dustCount);
                    int d = Dust.NewDust(dustPos, 0, 0, DustID.Electric, 0, 0, 100, explosionColor, 1.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 0.5f;
                }

                
                player.Center = safePos;
                // player.velocity = Vector2.Zero; 
                if (hitWall) 
                {
                    player.velocity = -Vector2.Normalize(dir) * 2f; 
                }

                dashvfx(player);
        }

        private void dashvfx(Player player)
        {
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.15f }, player.position);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 dustPosition = player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    Dust.NewDust(dustPosition, 0, 0, DustID.Smoke, player.velocity.X * -0.5f, player.velocity.Y * -0.5f);
                }
                for (int i = 0; i < 15; i++)
                {
                    Vector2 dustPosition = player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    Dust.NewDust(dustPosition, 0, 0, DustID.BlueTorch, player.velocity.X * -1f, player.velocity.Y * -1f, 0, default, 6f);
                }
        }

        private void ApplyFajinVfx(Player player, bool enhanced)
        {
            int dustCount = enhanced ? 20 : 10;
            int type = enhanced ? DustID.RedTorch : DustID.Cloud;
            float scale = enhanced ? 2f : 1.5f;

            for (int i = 0; i < dustCount; i++)
            {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, type, 0f, 0f, 100, default, scale);
                dust.velocity *= 0.5f;
                if (enhanced) dust.noGravity = true;
            }
        }}}
    


//    