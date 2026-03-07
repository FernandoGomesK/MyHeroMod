using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.HalfColdHalfHot;

namespace MyHeroMod.content.Buffs
{
    public class TemperatureBuff : ModBuff
    {
        
        public override string Texture => "MyHeroMod/Assets/BuffImage/TemperatureBuff"; 

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
            var modPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

            
            buffName = "Heat";
            
            
            tip = $"Heat: {modPlayer.temperature} / {modPlayer.maxTemperature}\n" +
                $"Active skills generate heat.";
        }

        public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
{
    var modPlayer = Main.LocalPlayer.GetModPlayer<HalfColdHalfHotPlayer>();
    string text = $"{modPlayer.temperature}/{modPlayer.maxTemperature}";

    
    Vector2 drawPos = drawParams.Position + new Vector2(16, 34);

    
    Utils.DrawBorderString(spriteBatch, text, drawPos, Color.White, 0.8f, 0.5f, 0f);
}
    }
}