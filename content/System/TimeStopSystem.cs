using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.System
{
    public class TimeStopSystem : ModSystem
    {
        
        public static bool IsTimeStopped = false;

        
        public override void PreUpdateEntities()
        {
            IsTimeStopped = false;
        }
    }
}