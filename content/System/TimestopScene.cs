using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace MyHeroMod.content.System
{
    public class TimeStopScene : ModSceneEffect
    {
        
        public override bool IsSceneEffectActive(Player player)
        {
            return TimeStopSystem.IsTimeStopped;
        }

    
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

        // 3. A mágica visual acontece aqui:
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (isActive)
            {
                
                if (!Filters.Scene["MyHeroMod:TimeStop"].IsActive())
                {
                    Filters.Scene.Activate("MyHeroMod:TimeStop");
                }
            }
            else
            {
            
                if (Filters.Scene["MyHeroMod:TimeStop"].IsActive())
                {
                    Filters.Scene.Deactivate("MyHeroMod:TimeStop");
                }
            }
        }
    }
}