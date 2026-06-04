using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.AllForOne;

public class GreenLightningLayer : PlayerDrawLayer
{
    
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
        var player = drawInfo.drawPlayer.GetModPlayer<OneForAll9thPlayer>();
        var afoPlayer = drawInfo.drawPlayer.GetModPlayer<AllForOnePlayer>();
        var mp = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
        return mp.HasActiveQuirk(QuirkType.OneForAll9th) && player.isFullCowlingBuffActive && !drawInfo.drawPlayer.dead  || (mp.HasActiveQuirk(QuirkType.AllForOne) && afoPlayer.HasInternalQuirk(QuirkType.OneForAll9th) && player.isFullCowlingBuffActive);
    }

    protected override void Draw(ref PlayerDrawSet drawInfo) {
        
        if (!ModContent.HasAsset("MyHeroMod/Assets/Effects/FullCowling")) {
            return; 
        }

        Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/Effects/FullCowling").Value;
       

        
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
            Color.White, 
            drawInfo.drawPlayer.fullRotation,
            new Vector2(texture.Width / 2f, frameHeight / 2f),
            1f,
            drawInfo.playerEffect,
            0
        );

        
        drawInfo.DrawDataCache.Add(drawData);
    }
}