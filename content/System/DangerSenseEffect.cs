// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Terraria;
// using Terraria.DataStructures;
// using Terraria.ModLoader;
// using MyHeroMod.content.Quirks.DangerSense;

// namespace MyHeroMod.content.Quirks.OFA9th.Visuals
// {
//     public class DangerSenseEffect : PlayerDrawLayer
//     {
//         public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

//         public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
//         {
//             if (drawInfo.drawPlayer == null || !drawInfo.drawPlayer.active || drawInfo.drawPlayer.dead) 
//                 return false;

//             // Buscamos sempre o DangerSensePlayer. 
//             // Mesmo se o jogador usar OneForAll9th, o DangerSensePlayer estará lá processando o visual.
//             if (drawInfo.drawPlayer.TryGetModPlayer<DangerSensePlayer>(out var modPlayer))
//             {
//                 return modPlayer.VisualTimer > 0;
//             }

//             return false;
//         }

//         protected override void Draw(ref PlayerDrawSet drawInfo)
//         {
//             if (!drawInfo.drawPlayer.TryGetModPlayer<DangerSensePlayer>(out var modPlayer)) return;
    
//             // Verificação de segurança para o Asset
//             if (!ModContent.HasAsset("MyHeroMod/Assets/Effects/DangerSenseEffect")) return;

//             Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/Effects/DangerSenseEffect").Value;

            
//             int totalFrames = 8; 
            
//             int timerProgress = modPlayer.VisualMaxTimer - modPlayer.VisualTimer; 
//             int frameDuration = modPlayer.VisualMaxTimer / totalFrames;
            
//             if (frameDuration < 1) frameDuration = 1;

//             int currentFrame = timerProgress / frameDuration;
//             if (currentFrame >= totalFrames) currentFrame = totalFrames - 1;

           
//             int frameHeight = texture.Height / totalFrames;
//             Rectangle sourceRectangle = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            
//             Vector2 drawPos = drawInfo.Center - Main.screenPosition;
//             drawPos.Y -= 50f; 
//             drawPos.Y += drawInfo.drawPlayer.gfxOffY; 

            
//             Lighting.AddLight(drawInfo.Center, Color.Yellow.ToVector3() * 0.8f);

//             DrawData drawData = new DrawData(
//                 texture,
//                 drawPos,
//                 sourceRectangle,
//                 Color.White, 
//                 0f,
//                 new Vector2(texture.Width / 2f, frameHeight / 2f), 
//                 1f, 
//                 drawInfo.playerEffect, 
//                 0
//             );

//             drawInfo.DrawDataCache.Add(drawData);
//         }
//     }
// }