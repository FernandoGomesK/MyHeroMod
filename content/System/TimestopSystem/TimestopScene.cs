using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.System
{
    public class TimeStopScene : ModSceneEffect
    {
        private static float _intensity = 0f;
        private const float FadeSpeed = 0.04f; // velocidade do fade (0.0 a 1.0)

        public override bool IsSceneEffectActive(Player player)
        {
            // Mantém o efeito ativo enquanto ainda há intensidade para fazer fade out
            return _intensity > 0f || TimeStopSystem.IsTimeStopped;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

        public override void SpecialVisuals(Player player, bool isActive)
        {
            // Fade in ou fade out suave
            if (TimeStopSystem.IsTimeStopped)
                _intensity = MathHelper.Min(_intensity + FadeSpeed, 0.5f);
            else
                _intensity = MathHelper.Max(_intensity - FadeSpeed, 0f);

            if (_intensity > 0f)
            {
                var filter = Filters.Scene["MyHeroMod:TimeStop"];

                if (!filter.IsActive())
                    Filters.Scene.Activate("MyHeroMod:TimeStop");

                // Aplica a opacidade do filtro
                filter.GetShader().UseOpacity(_intensity);
            }
            else
            {
                if (Filters.Scene["MyHeroMod:TimeStop"].IsActive())
                    Filters.Scene.Deactivate("MyHeroMod:TimeStop");
            }
        }
    }
}