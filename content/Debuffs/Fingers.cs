using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th.Buffs
{
    public class FingersBuff : ModBuff
    {
       
        public override string Texture => "MyHeroMod/Assets/BuffImage/ParallelProcessingBuff"; 

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            // Atualiza o nome e a descrição dinamicamente
            buffName = "Remaining Fingers";
            
            // Mostra: "Quirks Ativas: 2 / 3"
            tip = $"Mental Capacity: {modPlayer.Fingers} / {modPlayer.Fingers}\n" +
                  $"Active skills consume mental capacity.";
        }

        public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
{
    var modPlayer = Main.LocalPlayer.GetModPlayer<OneForAll9thPlayer>();
    string text = $"{modPlayer.ParallelProcessing}/{modPlayer.MaxParallelProcessing}";

    
    Vector2 drawPos = drawParams.Position + new Vector2(16, 34);

    Color color = modPlayer.ParallelProcessing >= modPlayer.MaxParallelProcessing ? Color.Red : Color.White;

    // Desenha centralizado (0.5f no X) e ancorado no topo do texto (0f no Y)
    Utils.DrawBorderString(spriteBatch, text, drawPos, color, 0.8f, 0.5f, 0f);
}
    }
}