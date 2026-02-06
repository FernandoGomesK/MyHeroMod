using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;

namespace MyHeroMod.content.Buffs
{
    public class DangerSenseBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/DangerSenseBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var dangerPlayer = player.GetModPlayer<DangerSensePlayer>();
            
            player.detectCreature = true;
            player.dangerSense = true;
            
            float currentChance = 0.05f;    

            switch(transformPlayer.CurrentStage){
                case QuirkStage.Initial:
                
                currentChance = 0.05f; // 5% de chance de esquiva
                break;
            
                case QuirkStage.Adequation:
                currentChance = 0.10f; // 10% de chance de esquiva
                break;
          
                case QuirkStage.Intermediate:
                currentChance = 0.15f; // 15% de chance de esquiva
                break;
            
                case QuirkStage.Advanced:
                currentChance = 0.25f; // 25% de chance de esquiva
                break;
          
                case QuirkStage.Final:
                currentChance = 0.50f; // 20% de chance de esquiva
                break;
        
                default:
                currentChance = 0.05f; // Chance de esquiva padrão
                break;
                    
            }

            if (dangerPlayer.IsOvertimeActive)
            {
                currentChance *= 1.5f;
            }
            if (currentChance > 0.9f) currentChance = 0.9f;

            transformPlayer.DodgeChance += currentChance;

        }
    }
}

            
