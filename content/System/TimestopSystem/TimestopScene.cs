using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.System
{
    public class TimeStopScene : ModSceneEffect
    {
        private static float _intensity = 0f;
        private const float FadeSpeed = 0.04f;

        public override bool IsSceneEffectActive(Player player)
        {
            return _intensity > 0f || TimeStopSystem.IsTimeStopped;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

        public override void SpecialVisuals(Player player, bool isActive)
        {


            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            bool isNeuralUpgrade = transPlayer.CurrentVariant == QuirkVariant.Variant1;

            //if (TimeStopSystem.IsTimeStopped && Main.GameUpdateCount % 60 == 0)
            //    Main.NewText($"Variant: {transPlayer.CurrentVariant} | neural: {isNeuralUpgrade}");

            string activeFilterKey = isNeuralUpgrade ? "MyHeroMod:TimeStopNeural" : "MyHeroMod:TimeStop";
            string inactiveFilterKey = isNeuralUpgrade ? "MyHeroMod:TimeStop" : "MyHeroMod:TimeStopNeural";

            float maxIntensity = isNeuralUpgrade ? 0.6f : 0.5f;

            if (TimeStopSystem.IsTimeStopped)
                _intensity = MathHelper.Min(_intensity + FadeSpeed, maxIntensity);
            else
                _intensity = MathHelper.Max(_intensity - FadeSpeed, 0f);

            if (_intensity > 0f)
            {
                if (Filters.Scene[inactiveFilterKey].IsActive())
                    Filters.Scene.Deactivate(inactiveFilterKey);
                    
                var filter = Filters.Scene[activeFilterKey];

                if (!filter.IsActive())
                    Filters.Scene.Activate(activeFilterKey);

                filter.GetShader().UseOpacity(_intensity);
            }
            else
            {
                if (Filters.Scene[activeFilterKey].IsActive())
                    Filters.Scene.Deactivate(activeFilterKey);

                if (Filters.Scene[inactiveFilterKey].IsActive())
                    Filters.Scene.Deactivate(inactiveFilterKey);
            }
        }
    }
}