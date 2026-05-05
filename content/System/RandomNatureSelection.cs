// using System;
// using Terraria;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;
// using MyHeroMod;
// using Terraria.ID;

// namespace MyHeroMod.content.System
// {
//     public class RandomNatureSelection
//     {
//         public static void SelectRandomNature()
//         {
//             var transPlayer = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
//             Array quirksArray = Enum.GetValues(typeof(QuirkType));
//             Array naturesArray = Enum.GetValues(typeof(NatureType));

//             // 1. Prevenção de Crash: Verifica se o jogador já tem TODAS as quirks
//             // (Subtraímos 1 por causa do Quirkless)
//             if (transPlayer.ActiveQuirks.Count >= quirksArray.Length - 1)
//             {
//                 Main.NewText("Your body cannot physically hold any more Quirks!", Color.Red);
//                 return;
//             }

//             // 2. Sorteia até calhar uma Quirk que ele NÃO TENHA
//             QuirkType quirkType;
//             do
//             {
//                 int randomIndex = Main.rand.Next(0, quirksArray.Length);
//                 quirkType = (QuirkType)quirksArray.GetValue(randomIndex);
//             }
//             // Continua a sortear se for Quirkless OU se o jogador já tiver essa Quirk
//             while (quirkType == QuirkType.Quirkless || transPlayer.HasActiveQuirk(quirkType));

//             // 3. A Mecânica "Nomu" (Aviso de Perigo)
//             // Lembre-se de usar os nomes exatos das variáveis que estão no TransformationPlayer
//             if (transPlayer.ActiveQuirks.Count >= transPlayer.naturalQuirkLimit)
//             {
//                 // Dá um aviso assustador, mas NÃO bloqueia a injeção!
//                 Main.NewText("Your body feels heavy... taking another Quirk is mutating your cells!", Color.DarkRed);
//             }

//             // 4. Injeta a Quirk Nova!
//             transPlayer.ActiveQuirks.Add(quirkType);
//             transPlayer.UpdateUnlockedSkills();

//             // 5. Sincroniza com os amigos no Multiplayer
//             if (Main.netMode == NetmodeID.MultiplayerClient)
//             {
//                 transPlayer.SendClientChanges(transPlayer);
//             }

//             Main.NewText($"You awakened: {quirkType}!", Color.Yellow);
//         }
//     }
// }