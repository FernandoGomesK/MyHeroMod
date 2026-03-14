using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System; 

namespace MyHeroMod.content.Items
{
    public class DebugStageChanger : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Red;
        }

        
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();

            
            if (player.altFunctionUse == 2)
            {
                modPlayer.ManualStageOverride = false;
                Main.NewText("Debug Mode: OFF | Auto-Progression Enabled", Microsoft.Xna.Framework.Color.Orange);
            }
            
            else
            {
                modPlayer.ManualStageOverride = true;

                
                switch (modPlayer.CurrentStage)
                {
                    case QuirkStage.Initial:
                        modPlayer.CurrentStage = QuirkStage.Adequation;
                        break;
                    case QuirkStage.Adequation:
                        modPlayer.CurrentStage = QuirkStage.Intermediate;
                        break;
                    case QuirkStage.Intermediate:
                        modPlayer.CurrentStage = QuirkStage.Advanced;
                        break;
                    case QuirkStage.Advanced:
                        modPlayer.CurrentStage = QuirkStage.Final;
                        break;
                    case QuirkStage.Final:
                        modPlayer.CurrentStage = QuirkStage.Initial;
                        break;
                }

               
                modPlayer.UpdateUnlockedSkills();

                Main.NewText($"Debug Mode: ON | Stage set to: {modPlayer.CurrentStage}", Microsoft.Xna.Framework.Color.Cyan);
            }

            return true;
        }
    }
}