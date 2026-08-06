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

            
            bool hasFullCowling = drawInfo.drawPlayer.ownedProjectileCounts[ModContent.ProjectileType<FullCowlingChargeProj>()] > 0;
            bool hasDetroit1M = drawInfo.drawPlayer.ownedProjectileCounts[ModContent.ProjectileType<Charge1000000DetroitProj>()] > 0;
            
            return hasFullCowling || hasDetroit1M;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (!ModContent.HasAsset("MyHeroMod/Assets/Effects/FullCowlingVeins")) return;

            Player player = drawInfo.drawPlayer;

            int timer = 0;
            int maxTime = 40; 

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI)
                {
                    if (p.type == ModContent.ProjectileType<FullCowlingChargeProj>())
                    {
                        timer = (int)p.ai[0];
                        maxTime = 40; 
                        break;
                    }
                    else if (p.type == ModContent.ProjectileType<Charge1000000DetroitProj>())
                    {
                        timer = (int)p.ai[0];
                        maxTime = 120; 
                        break;
                    }
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