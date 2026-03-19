using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.Blueflames;

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
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
            buffName = "Heat";
            
            
            if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot)) 
            {
                var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
                tip = $"Heat: {hchhPlayer.temperature} / {hchhPlayer.maxTemperature}\nActive skills generate heat.";
            }
            
            else if (transPlayer.HasActiveQuirk(QuirkType.HellFlames)) 
            {
                var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
                tip = $"Heat: {hellPlayer.CurrentHeat} / {hellPlayer.MaxHeat}\nActive skills generate heat.";
            }
            
            else if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames)) 
            {
                var bluePlayer = player.GetModPlayer<BlueFlamesPlayer>();
                tip = $"Heat: {bluePlayer.CurrentHeat} / {bluePlayer.MaxHeat}\nActive skills generate heat.";
            }
        }

        
        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            string texturePath = "MyHeroMod/Assets/BuffImage/TemperatureBuff"; 

            
            if (!transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                
                if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
                {
                    texturePath = "MyHeroMod/Assets/BuffImage/HeatBuff"; 
                }
                
                else if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames))
                {
                    texturePath = "MyHeroMod/Assets/BuffImage/BlueHeatBuff"; 
                }
            }

            if (ModContent.HasAsset(texturePath))
            {
                Texture2D customTexture = ModContent.Request<Texture2D>(texturePath).Value;
                drawParams.Texture = customTexture;
                drawParams.SourceRectangle = customTexture.Frame(); 
            }

            return true;
        }

        
        public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
            string text = "";

            // PRIORITY 1
            if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
                text = $"{hchhPlayer.temperature}/{hchhPlayer.maxTemperature}";
            }
            // PRIORITY 2
            else if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
                text = $"{hellPlayer.CurrentHeat}/{hellPlayer.MaxHeat}";
            }
            // PRIORITY 3
            else if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames))
            {
                var bluePlayer = player.GetModPlayer<BlueFlamesPlayer>();
                text = $"{bluePlayer.CurrentHeat}/{bluePlayer.MaxHeat}";
            }

            Vector2 drawPos = drawParams.Position + new Vector2(16, 34);
            Utils.DrawBorderString(spriteBatch, text, drawPos, Color.White, 0.8f, 0.5f, 0f);
        }
    }
}