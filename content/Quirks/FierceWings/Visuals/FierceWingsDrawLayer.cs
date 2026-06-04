using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.FierceWings
{
    public class FierceWingsDrawLayer : PlayerDrawLayer
    {
        
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            var transPlayer = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
            
            return drawInfo.drawPlayer.active && 
                   !drawInfo.drawPlayer.dead && 
                   transPlayer.HasActiveQuirk(QuirkType.FierceWings);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            var wingsPlayer = player.GetModPlayer<FierceWingsPlayer>();

            
            string texturePath = wingsPlayer.featherStage switch
            {
                1 => "MyHeroMod/content/Quirks/FierceWings/Visuals/FierceWings_1", 
                2 => "MyHeroMod/content/Quirks/FierceWings/Visuals/FierceWings_2",
                3 => "MyHeroMod/content/Quirks/FierceWings/Visuals/FierceWings_3",
                _ => "MyHeroMod/content/Quirks/FierceWings/Visuals/FierceWings_4"
            };

            Texture2D texture = ModContent.Request<Texture2D>(texturePath).Value;

        
            Vector2 drawPos = new Vector2(
                (int)(drawInfo.Position.X - Main.screenPosition.X + (player.width / 2f)),
                (int)(drawInfo.Position.Y - Main.screenPosition.Y + (player.height / 2f)) 
            );

            
            drawPos.Y += 4f; 

            Color drawColor = drawInfo.colorArmorBody;

            
            DrawData drawData = new DrawData(
                texture,
                drawPos,
                player.bodyFrame, 
                drawColor,
                player.bodyRotation, 
                new Vector2(player.bodyFrame.Width / 2f, player.bodyFrame.Height / 2f), 
                1f, 
                drawInfo.playerEffect, 
                0
            );
            
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}