using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public class OneForAll9thPlayer : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && mainPlayer.isTransformationActive)
            {
                float multiplier = (int)mainPlayer.CurrentStage + 1;
                Player.GetDamage(DamageClass.Generic) += 0.10f * multiplier;
                Player.statDefense += (int)(5 * multiplier);
                Player.moveSpeed += 0.10f * multiplier;

                Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);

                if (mainPlayer.CurrentStage == QuirkStage.Initial && Main.rand.NextBool(600))
                {
                    Player.GetDamage(DamageClass.Generic) += 0.10f;
                    Player.statLife -= 5;
                    CombatText.NewText(Player.getRect(), Color.Red, "-5 HP: Strain!");
                }

            }
             
        }
    }
}

public class GreenLightningLayer : PlayerDrawLayer
{
    
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
        var mp = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
        return mp.SelectedQuirk == QuirkType.OneForAll9th && mp.isTransformationActive;
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