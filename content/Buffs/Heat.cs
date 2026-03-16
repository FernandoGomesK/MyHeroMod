using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.HellFlames;

namespace MyHeroMod.content.Buffs
{
    public class Heat : ModBuff
    {
        
        public override string Texture => "MyHeroMod/Assets/BuffImage/HeatBuff"; 

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true; // Não mostra timer, pois é um estad
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<HellFlamesPlayer>();

            
            buffName = "Heat";
            
            
            tip = $"Heat: {modPlayer.CurrentHeat} / {modPlayer.MaxHeat}\n" +
                  $"Active skills consume heat.";
        }

        public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
{
    var modPlayer = Main.LocalPlayer.GetModPlayer<HellFlamesPlayer>();
    string text = $"{modPlayer.CurrentHeat}/{modPlayer.MaxHeat}";

    
    Vector2 drawPos = drawParams.Position + new Vector2(16, 34);

    
    Utils.DrawBorderString(spriteBatch, text, drawPos, Color.White, 0.8f, 0.5f, 0f);
}
    }
}