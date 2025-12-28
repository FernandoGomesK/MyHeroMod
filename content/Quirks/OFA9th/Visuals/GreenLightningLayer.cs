using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content;

public class GreenLightningLayer : PlayerDrawLayer
{
    
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
        var mp = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
        return mp.SelectedQuirk == QuirkType.OneForAll9th && mp.ActiveForm != QuirkSkills.None;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo) {
        
        if (!ModContent.HasAsset("MyHeroMod/Assets/FullCowling")) {
            return; 
        }

        Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/FullCowling").Value;

        // Ajuste o frameCount para o número real de frames que você desenhou
        int frameCount = 6; 
        int frameSpeed = 6; 
        int currentFrame = (int)(Main.GameUpdateCount / frameSpeed) % frameCount;

        int frameHeight = texture.Height / frameCount;
        Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

        // Centraliza os raios no jogador
        Vector2 position = drawInfo.Center - Main.screenPosition;
        
        // Criando o dado de desenho
        DrawData drawData = new DrawData(
            texture,
            new Vector2((int)position.X, (int)position.Y), 
            sourceRect,
            Color.White, 
            drawInfo.drawPlayer.fullRotation,
            new Vector2(texture.Width / 2f, frameHeight / 2f),
            1f,
            drawInfo.playerEffect,
            0
        );

        // Adiciona à lista de desenhos do frame atual
        drawInfo.DrawDataCache.Add(drawData);
    }
}