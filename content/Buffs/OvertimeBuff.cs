// using Terraria.ModLoader;
// using MyHeroMod.content.Quirks.DangerSense;
// using Terraria;

// namespace MyHeroMod.content.Buffs // Ajuste o namespace se necessário
// {
//     public class OvertimeBuff : ModBuff
//     {
//         public override string Texture => "MyHeroMod/Assets/BuffImage/OvertimeBuff";
//         public override void SetStaticDefaults()
//         {
//             Main.buffNoSave[Type] = true; 
//             Main.buffNoTimeDisplay[Type] = false; // FALSE = Mostra o timer descendo!
//             Main.debuff[Type] = false; 
//         }

//         public override void Update(Player player, ref int buffIndex)
//         {
//             // Pega o DangerSensePlayer
//             var dangerPlayer = player.GetModPlayer<DangerSensePlayer>();
            
//             // Liga a variável
//             // Como usamos ResetEffects no player, se o buff acabar, 
//             // ninguém vai setar isso como true, e ela desliga sozinha.
//             dangerPlayer.IsOvertimeActive = true;
//         }
//     }
// }