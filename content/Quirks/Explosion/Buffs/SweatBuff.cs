// using Microsoft.Xna.Framework.Graphics;
// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.DataStructures;
// using Terraria.ModLoader;

// namespace MyHeroMod.content.Quirks.Explosion.Buffs;

// public class SweatBuff : ModBuff
// {
//     public override string Texture => "MyHeroMod/Assets/BuffImage/SweatBuff";

//     public override void SetStaticDefaults()
//     {
//         Main.buffNoSave[Type] = true;
//         Main.buffNoTimeDisplay[Type] = true;
//         Main.debuff[Type] = false;
//     }

//     public override void Update(Player player, ref int buffIndex)
//     {
        
//     }

//     public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
//     {
//         Player player = Main.LocalPlayer;
//         var modPlayer = player.GetModPlayer<ExplosionPlayer>();

//         buffName = "Sweat";

//         tip = $"Heat: {modPlayer.CurrentSweat} / {modPlayer.MaxSweat}\n" +
//                   $"Active skills generate sweat."; 
//     }

//     public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
//     {
//         var modPlayer = Main.LocalPlayer.GetModPlayer<ExplosionPlayer>();
//         string text = $"{modPlayer.CurrentSweat}/{modPlayer.MaxSweat}";

//         Vector2 drawPos = drawParams.Position + new Vector2(16, 34);

//     // Color color = modPlayer.CurrentHeat >= modPlayer.MaxHeat ? Color.Red : Color.White;

//     // Desenha centralizado (0.5f no X) e ancorado no topo do texto (0f no Y)
//         Utils.DrawBorderString(spriteBatch, text, drawPos, Color.White, 0.8f, 0.5f, 0f);
//     }

// }