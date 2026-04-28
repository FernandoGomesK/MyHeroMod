using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs; // Certifique-se que o ChimeraBuff está aqui

namespace MyHeroMod.content.Quirks.Overhaul.Visuals
{
    public class ChimeraDrawLayer : PlayerDrawLayer
    {
        // Posiciona a camada para desenhar por cima do Peitoral (Torso) em vez dos sapatos!
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Torso);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            // Só fica visível se o jogador não estiver morto e tiver o Buff ativado
            return drawInfo.drawPlayer.active && 
                   !drawInfo.drawPlayer.dead && 
                   drawInfo.drawPlayer.HasBuff(ModContent.BuffType<ChimeraBuff>());
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            
            // VERIFICA SE ESTÁ A ATACAR/USAR ITEM
            bool isUsingItem = player.itemAnimation > 0;

            
            string texturePath = isUsingItem 
                ? "MyHeroMod/content/Quirks/Overhaul/Visuals/Chimera_HandsOn" 
                : "MyHeroMod/content/Quirks/Overhaul/Visuals/Chimera_HandsOff";

            Texture2D texture = ModContent.Request<Texture2D>(texturePath).Value;

            
            Vector2 drawPos = new Vector2(
                (int)(drawInfo.Position.X - Main.screenPosition.X + (player.width / 2f)),
                (int)(drawInfo.Position.Y - Main.screenPosition.Y + player.height - (player.bodyFrame.Height / 2f) + 4f)
            ) + player.bodyPosition;

            // Puxa a iluminação correta do ambiente
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