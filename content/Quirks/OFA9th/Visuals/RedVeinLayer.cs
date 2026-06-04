using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.OFA9th.Projectiles; 
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.OFA9th.Visuals
{
    public class RedVeinLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow != 0f) return false;

            // Verificação super rápida (O(1)): O jogador é dono de um FullCowlingChargeProj neste exato frame?
            // Se sim, ele está a carregar a habilidade e as veias devem aparecer!
            return drawInfo.drawPlayer.ownedProjectileCounts[ModContent.ProjectileType<FullCowlingChargeProj>()] > 0;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (!ModContent.HasAsset("MyHeroMod/Assets/Effects/FullCowlingVeins")) return;

            Player player = drawInfo.drawPlayer;

            int timer = 0;
            int maxTime = 40; // O tempo de ChannelTime que definimos no FullCowlingChargeProj (40 frames)

            // Vamos procurar o projétil para pegar o tempo exato (ai[0]) e animar as veias subindo!
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<FullCowlingChargeProj>())
                {
                    timer = (int)p.ai[0];
                    break;
                }
            }

            Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/Effects/FullCowlingVeins").Value;

            int totalframes = 4;
            int frameDuration = maxTime / totalframes;
            if (frameDuration < 1) frameDuration = 1;

            int currentFrame = (timer / frameDuration);
            if (currentFrame >= totalframes) currentFrame = totalframes - 1;

            int frameHeight = texture.Height / totalframes;
            Rectangle sourceRectangle = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);
            
            Vector2 position = drawInfo.Center - Main.screenPosition;
            position = new Vector2((int)position.X, (int)position.Y + player.gfxOffY);

            DrawData drawData = new DrawData(
                texture,
                position,
                sourceRectangle,
                Color.White * 0.8f,
                player.fullRotation,
                new Vector2(texture.Width / 2f, frameHeight / 2f),
                1f,
                drawInfo.playerEffect,
                0
            );

            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}