using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.AllForOne;
using ReLogic.Content;
using MyHeroMod.content.Quirks.Explosion;

public class GreenLightningLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

    private static Asset<Texture2D> fullCowlingTexture;
    private static Asset<Texture2D> baseLightningTexture;

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
        var player = drawInfo.drawPlayer.GetModPlayer<OneForAll9thPlayer>();
        var afoPlayer = drawInfo.drawPlayer.GetModPlayer<AllForOnePlayer>();
        var mp = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
        
        bool hasOFA = mp.HasActiveQuirk(QuirkType.OneForAll9th) || (mp.HasActiveQuirk(QuirkType.AllForOne) && afoPlayer.HasInternalQuirk(QuirkType.OneForAll9th));
        
        return hasOFA && player.isFullCowlingBuffActive && !drawInfo.drawPlayer.dead;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo) {
        if (fullCowlingTexture == null) {
            fullCowlingTexture = ModContent.Request<Texture2D>("MyHeroMod/Assets/Effects/FullCowling");
        }
        if (baseLightningTexture == null) {
            baseLightningTexture = ModContent.Request<Texture2D>("MyHeroMod/Assets/Effects/BaseLightning");
        }

        var mp = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
        var afoPlayer = drawInfo.drawPlayer.GetModPlayer<AllForOnePlayer>();

        
        bool hasExplosion = mp.HasActiveQuirk(QuirkType.Explosion) || (mp.HasActiveQuirk(QuirkType.AllForOne) && afoPlayer.HasInternalQuirk(QuirkType.Explosion));
        bool hasAFO = mp.HasActiveQuirk(QuirkType.AllForOne);

        Texture2D texture;
        Color drawColor;

        if  (hasAFO)
        {
            texture = baseLightningTexture.Value;
            drawColor = Color.Red;
        }
        else if (hasExplosion) 
        {
            texture = baseLightningTexture.Value;
            drawColor = Color.Orange;
        } 
        
        else 
        {
            texture = fullCowlingTexture.Value;
            drawColor = Color.White;
        }

        int frameCount = 6; 
        int frameSpeed = 6; 
        int currentFrame = (int)(Main.GameUpdateCount / frameSpeed) % frameCount;

        int frameHeight = texture.Height / frameCount;
        Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

        Vector2 position = drawInfo.Center - Main.screenPosition;
        
        DrawData drawData = new DrawData(
            texture,
            new Vector2((int)position.X, (int)position.Y), 
            sourceRect,
            drawColor, 
            drawInfo.drawPlayer.fullRotation,
            new Vector2(texture.Width / 2f, frameHeight / 2f),
            1f,
            drawInfo.playerEffect,
            0
        );

        drawInfo.DrawDataCache.Add(drawData);
    }
}