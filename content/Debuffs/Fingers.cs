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

            
            buffName = "Remaining Fingers";
            
            
            tip = $"Mental Capacity: {modPlayer.currentFingers} / {modPlayer.MaxFingers}\n" +
                  $"Your fingers are Broken.";
        }

        public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
{
    var modPlayer = Main.LocalPlayer.GetModPlayer<OneForAll9thPlayer>();
    string text = $"{modPlayer.ParallelProcessing}/{modPlayer.MaxParallelProcessing}";

    
    Vector2 drawPos = drawParams.Position + new Vector2(16, 34);

    Color color = modPlayer.ParallelProcessing >= modPlayer.MaxFingers ? Color.Red : Color.White;

    
    Utils.DrawBorderString(spriteBatch, text, drawPos, color, 0.8f, 0.5f, 0f);
}
    }
}