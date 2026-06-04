using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.System.BasePlayer;

namespace MyHeroMod.content.Buffs
{
    public class DangerSenseBuff : ModBuff
    {
        
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
           
            if (!player.TryGetModPlayer<TransformationPlayer>(out var transformPlayer) ||
                !player.TryGetModPlayer<DangerSensePlayer>(out var dangerPlayer))
            {
                return;
            }

            
            player.dangerSense = true;
        

            float currentChance = 0.05f; 

            
            switch(transformPlayer.CurrentStage) {
                case QuirkStage.Initial: currentChance = 0.05f; break;
                case QuirkStage.Adequation: currentChance = 0.15f; break;
                case QuirkStage.Intermediate: currentChance = 0.25f; break;
                case QuirkStage.Advanced: currentChance = 0.35f; break;
                case QuirkStage.Final: currentChance = 0.50f; break;
            }

            if (dangerPlayer.IsOvertimeActive) currentChance *= 1.5f;
            if (currentChance > 0.9f) currentChance = 0.9f;

           
            dangerPlayer.dodgeChance = currentChance; 
        }
    }
}