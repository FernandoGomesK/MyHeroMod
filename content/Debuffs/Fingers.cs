using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th.Buffs
{
    public class FingersBuff : ModBuff
    {
       
        

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
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            
            buffName = "Remaining Fingers";
            
            
            tip = $"Remaining Fingers: {ofaPlayer.currentFingers} / {ofaPlayer.MaxFingers}\n" +
                  $"Your fingers are Broken.";
        }

        public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
{
    var ofaPlayer = Main.LocalPlayer.GetModPlayer<OneForAll9thPlayer>();
    string text = $"{ofaPlayer.currentFingers}/{ofaPlayer.MaxFingers}";

    
    Vector2 drawPos = drawParams.Position + new Vector2(16, 34);

    Color color = ofaPlayer.currentFingers >= ofaPlayer.MaxFingers ? Color.Red : Color.White;

    
    Utils.DrawBorderString(spriteBatch, text, drawPos, color, 0.8f, 0.5f, 0f);
}
    }
}