using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Smokescreen;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace MyHeroMod.content.Buffs // Ajuste o namespace se necessário
{
    public class SmokescreenBuff : ModBuff
    {
        
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            player.GetModPlayer<SmokescreenPlayer>().isSmokescreenActive = true;
            

            float currentChance = 0.05f; 

            
            switch(transformPlayer.CurrentStage) {
                case QuirkStage.Initial: currentChance = 0.05f; break;
                case QuirkStage.Adequation: currentChance = 0.15f; break;
                case QuirkStage.Intermediate: currentChance = 0.25f; break;
                case QuirkStage.Advanced: currentChance = 0.35f; break;
                case QuirkStage.Final: currentChance = 0.45f; break;
            }

            player.GetModPlayer<SmokescreenPlayer>().dodgeChance = currentChance;

            Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0f, 0f, 100, Color.MediumPurple, 6.0f);
        }
    }
}