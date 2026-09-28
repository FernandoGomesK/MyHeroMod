using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.SlideAndGlide
{
    public class SlideAndGlideDrawLayer : PlayerDrawLayer
    {
        
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Shoes);

        
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            // Só fica visível se o jogador não estiver morto e tiver o Buff ativado
            return drawInfo.drawPlayer.active && 
                   !drawInfo.drawPlayer.dead && 
                   drawInfo.drawPlayer.HasBuff(ModContent.BuffType<SlideAndGlideBuff>());
        }

        
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            var slidePlayer = player.GetModPlayer<SlideAndGlidePlayer>();
            
            bool isWallSliding = player.sliding;
            
            
            bool isShooting = slidePlayer.greenLegsTimer > 0;

            
            string texturePath = (isWallSliding || isShooting) 
                ? "MyHeroMod/content/Quirks/SlideAndGlide/Visuals/GreenLegs" 
                : "MyHeroMod/content/Quirks/SlideAndGlide/Visuals/BlueLegs";

            
            Texture2D texture = ModContent.Request<Texture2D>(texturePath).Value;

            Vector2 drawPos = new Vector2(
                (int)(drawInfo.Position.X - Main.screenPosition.X + (player.width / 2f)),
                (int)(drawInfo.Position.Y - Main.screenPosition.Y + player.height)
            );

            
            drawPos.Y -= 22f; 

            
            Color drawColor = Color.White;

            
            DrawData drawData = new DrawData(
                texture,
                drawPos,
                player.legFrame, 
                drawColor,
                player.legRotation, 
                new Vector2(player.legFrame.Width / 2f, player.legFrame.Height / 2f), // Ponto de origem muda para o centro do frame
                1f, 
                drawInfo.playerEffect, 
                0
            );
            
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}