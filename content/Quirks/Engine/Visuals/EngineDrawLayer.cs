using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.Engine
{
    public class EngineDrawLayer : PlayerDrawLayer
    {
        
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Shoes);

        
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            // Só fica visível se o jogador não estiver morto e tiver o Buff ativado
             var transPlayer = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
            
            
            return drawInfo.drawPlayer.active && 
                   !drawInfo.drawPlayer.dead && 
                   transPlayer.HasActiveQuirk(QuirkType.Engine);
        }

        
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            var transPlayer = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();

            // Carregue a sua textura (Troque o caminho para o local exato da sua imagem!)
            string texturePath = transPlayer.CurrentStage >= QuirkStage.Intermediate 
                ? "MyHeroMod/content/Quirks/Engine/Visuals/Exhausts2" 
                : "MyHeroMod/content/Quirks/Engine/Visuals/Exhausts";

            // Carrega a textura escolhida (agora acessível para o DrawData!)
            Texture2D texture = ModContent.Request<Texture2D>(texturePath).Value;
            

            
            Vector2 drawPos = new Vector2(
                (int)(drawInfo.Position.X - Main.screenPosition.X + (player.width / 2f)),
                (int)(drawInfo.Position.Y - Main.screenPosition.Y + player.height)
            );

            
            drawPos.Y -= 22f; 

            
            Color drawColor = drawInfo.colorArmorBody;

            
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