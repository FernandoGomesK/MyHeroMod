using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;




namespace MyHeroMod.content
{
    public enum PowerLevel
    {
        None,
        FivePercent,
        EightPercent,
    }
    public class TransformationPlayer : ModPlayer
    {
        public PowerLevel currentPower = PowerLevel.None;
        public bool isTransformed = false;

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (KeybindSystem.TransformKey.JustPressed) {
                isTransformed = !isTransformed;

                string msg = isTransformed ? "One For All: Full Cowling!" : "Form Off.";
                Main.NewText(msg, isTransformed ? Color.LimeGreen : Color.White);

                // Efeito de fumaça verde ao ativar
                for (int i = 0; i < 30; i++) {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.TerraBlade, 0, 0, 100, default, 1.5f);
                }
            }
        }

        public override void PostUpdateEquips() {
            if (isTransformed) {
                Player.GetDamage(DamageClass.Generic) += 0.30f;
                Player.statDefense += 15;
                Player.moveSpeed += 0.20f;
                
                // Luz verde saindo do corpo
                Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.5f);
            }
        }
    }


    public class GreenLightningLayer : PlayerDrawLayer
{
    
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
       
        return drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>().isTransformed;
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
}}