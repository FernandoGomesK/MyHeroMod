using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Quirks.FaJin; // Importante para acessar o FajinPlayer

namespace MyHeroMod.content.System
{
    public class DashSkill : QuirkSkill
    {
        public override string Name => "Dash";
        public override int BaseCooldown => 60;

        public override void OnUse(Player player)
        {
            var fajinPlayer = player.GetModPlayer<FajinPlayer>();
            float speed = 14f;
            bool isEnhanced = false;

            
            if (fajinPlayer.FaJinStored)
            {
                speed = 25f;
                isEnhanced = true;
                fajinPlayer.FaJinCharges = 0; 
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1"), player.position);
            }
            else 
            {
                fajinPlayer.ChargeFajin(); 
                SoundEngine.PlaySound(SoundID.Item14, player.position);
            }

            Vector2 dashDirection = Main.MouseWorld - player.Center;
            if (dashDirection != Vector2.Zero)
            {
                dashDirection.Normalize();
                player.velocity = dashDirection * speed;
            }


            // 3.Efeitos Visuais (VFX)
            ApplyDashVfx(player, isEnhanced);
            
            player.SetImmuneTimeForAllTypes(10); // Pequena invulnerabilidade
        }

        private void ApplyDashVfx(Player player, bool enhanced)
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
        }
    }
}