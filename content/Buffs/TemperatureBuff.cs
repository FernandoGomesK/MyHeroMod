using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.Blueflames;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content.Buffs
{
    public class TemperatureBuff : ModBuff
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
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            buffName = "Temperature";
            
            int currentTemp = 0;
            int maxTemp = 100;

            
            if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                var hchh = player.GetModPlayer<HalfColdHalfHotPlayer>();
                currentTemp = hchh.Temperature;
                maxTemp = hchh.MaxTemperature;
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames))
            {
                var blue = player.GetModPlayer<BlueFlamesPlayer>();
                currentTemp = blue.Temperature;
                maxTemp = blue.MaxTemperature;
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                var hell = player.GetModPlayer<HellFlamesPlayer>();
                currentTemp = hell.Temperature;
                maxTemp = hell.MaxTemperature;
            }

            tip = $"Temperature: {currentTemp} / {maxTemp}\nActive skills affect your body temperature.";
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

        
            if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                var hchh = player.GetModPlayer<HalfColdHalfHotPlayer>();
                text = $"{hchh.Temperature} / {hchh.MaxTemperature}";
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.BlueFlames))
            {
                var blue = player.GetModPlayer<BlueFlamesPlayer>();
                text = $"{blue.Temperature} / {blue.MaxTemperature}";
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                var hell = player.GetModPlayer<HellFlamesPlayer>();
                text = $"{hell.Temperature} / {hell.MaxTemperature}";
            }

            
            if (text != "")
            {
                Vector2 drawPos = drawParams.Position + new Vector2(16, 34);
                Utils.DrawBorderString(spriteBatch, text, drawPos, Color.White, 0.8f, 0.5f, 0f);
            }
        }
    }
}