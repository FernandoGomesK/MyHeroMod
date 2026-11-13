using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs.Gearshift;
using MyHeroMod.content.Buffs.Overclock;


namespace MyHeroMod.content.System
{
    public class TimeStopSystem : ModSystem
    {
        public static bool IsTimeStopped = false;

        
        public override void PreUpdateEntities()
        {
            IsTimeStopped = false;


            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player p = Main.player[i];
                
               
                if (p.active && !p.dead && p.HasBuff(ModContent.BuffType<OverclockBuff>()))
                {
                    IsTimeStopped = true;
                    break; 
                }

                if (p.active && !p.dead && p.HasBuff(ModContent.BuffType<GearshiftBuff>()) && p.HasBuff(ModContent.BuffType<TopGearBuff>()))
                {
                    IsTimeStopped = true;
                    break;
                }
            }
        }
    }
}