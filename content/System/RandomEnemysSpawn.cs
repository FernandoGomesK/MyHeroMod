using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using MyHeroMod.content.Npcs.Enemies.Nomu;

namespace MyHeroMod.content.System
{
    public class NomuSpawnSystem : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
           
            if (spawnInfo.PlayerSafe || !spawnInfo.Player.ZoneOverworldHeight)
                return;

            var transPlayer = spawnInfo.Player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.CurrentStage == QuirkStage.Adequation)
            {
                pool.Add(ModContent.NPCType<NomuNPC>(), 0.05f);
            }
            else if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                pool.Add(ModContent.NPCType<NomuNPC>(), 0.15f);
            }
        }
    }
}