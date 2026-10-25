using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.Engine;

namespace MyHeroMod.content.UI.EngineUI
{
    public class EngineGearUIState : UIState
    {
        public EngineGearBar EngineGear;

        public override void OnInitialize()
        {
            EngineGear = new EngineGearBar();
            EngineGear.Width.Set(46f, 0f);
            EngineGear.Height.Set(66f, 0f);
            Append(EngineGear); 
        }
    }

    
    public class EngineGearBar : BaseDraggableBar
    {
        protected override void AutoPosition()
        {
           
            Left.Set(Main.screenWidth - 355f, 0f);
            Top.Set(10f, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var enginePlayer = player.GetModPlayer<EnginePlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.Engine))
                return;

            Texture2D textureToDraw = enginePlayer.currentGear switch
            {
                1 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear1").Value,
                2 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear2").Value,
                3 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear3").Value,
                4 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear4").Value,
                5 => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGear5").Value,
                _ => ModContent.Request<Texture2D>("MyHeroMod/Assets/UI/EngineGears").Value
            };

            CalculatedStyle dimensions = GetDimensions();
            Vector2 drawPos = new Vector2(dimensions.X, dimensions.Y);

            spriteBatch.Draw(textureToDraw, drawPos, Color.White);
        }
    }
}