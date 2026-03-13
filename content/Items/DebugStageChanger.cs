// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using MyHeroMod.content.System; // Ajuste para seu namespace

// namespace MyHeroMod.content.Items
// {
//     public class DebugStageChanger : ModItem
//     {
//         public override void SetDefaults()
//         {
//             Item.width = 32;
//             Item.height = 32;
//             Item.useTime = 20;
//             Item.useAnimation = 20;
//             Item.useStyle = ItemUseStyleID.HoldUp;
//             Item.rare = ItemRarityID.Red;
//         }

//         public override bool? UseItem(Player player)
//         {
//             var modPlayer = player.GetModPlayer<TransformationPlayer>();

//             // 1. Ativa o modo manual para parar de resetar
//             modPlayer.ManualStageOverride = true;

//             // 2. Avança para o próximo estágio
//             if (modPlayer.CurrentStage == QuirkStage.Adequation)
//                 modPlayer.CurrentStage = QuirkStage.Intermediate;
//             else if (modPlayer.CurrentStage == QuirkStage.Intermediate)
//                 modPlayer.CurrentStage = QuirkStage.Advanced;
//             else if (modPlayer.CurrentStage == QuirkStage.Advanced)
//                 modPlayer.CurrentStage = QuirkStage.Final;
//             else
//                 modPlayer.CurrentStage = QuirkStage.Adequation; // Volta pro começo

//             Main.NewText($"Debug Mode: ON | Stage set to: {modPlayer.CurrentStage}", Microsoft.Xna.Framework.Color.Cyan);

//             return true;
//         }

//         // Clique Direito para voltar ao modo Automático
//         public override bool AltFunctionUse(Player player)
//         {
//             return true;
//         }

//         public override bool CanUseItem(Player player)
//         {
//             if (player.altFunctionUse == 2)
//             {
//                 var modPlayer = player.GetModPlayer<TransformationPlayer>();
//                 modPlayer.ManualStageOverride = false; // Desliga a trava
//                 Main.NewText("Debug Mode: OFF | Auto-Progression Enabled", Microsoft.Xna.Framework.Color.Orange);
//             }
//             return true;
//         }
//     }
// }