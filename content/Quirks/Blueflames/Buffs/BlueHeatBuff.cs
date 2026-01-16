using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Blueflames.Buffs
{
    public class BlueHeatBuff : ModBuff
    {
        // Use uma textura de ícone apropriada (pode ser um cérebro ou chips)
        public override string Texture => "MyHeroMod/Assets/BlueHeatBuff"; 

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true; // Não mostra timer, pois é um estad
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // O buff não faz nada físico, serve apenas como indicador visual
            // A lógica real está no Player
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<BlueFlamesPlayer>();

            // Atualiza o nome e a descrição dinamicamente
            buffName = "Heat";
            
            // Mostra: "Quirks Ativas: 2 / 3"
            tip = $"Heat: {modPlayer.CurrentHeat} / {modPlayer.MaxHeat}\n" +
                  $"Active skills consume heat.";
        }

        public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
{
    var modPlayer = Main.LocalPlayer.GetModPlayer<BlueFlamesPlayer>();
    string text = $"{modPlayer.CurrentHeat}/{modPlayer.MaxHeat}";

    // Ajuste da Posição:
    // X = 16 (Metade de 32, para centralizar)
    // Y = 34 (Um pouco mais que 32, para ficar EMBAIXO do ícone e não dentro)
    Vector2 drawPos = drawParams.Position + new Vector2(16, 34);

    // Color color = modPlayer.CurrentHeat >= modPlayer.MaxHeat ? Color.Red : Color.White;

    // Desenha centralizado (0.5f no X) e ancorado no topo do texto (0f no Y)
    Utils.DrawBorderString(spriteBatch, text, drawPos, Color.White, 0.8f, 0.5f, 0f);
}
    }
}