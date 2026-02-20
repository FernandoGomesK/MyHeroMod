// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Terraria;
// using Terraria.DataStructures;
// using Terraria.ModLoader;
// using MyHeroMod.content.Quirks.DangerSense; // Importante para achar o Player

// namespace MyHeroMod.content.Quirks.OFA9th.Visuals
// {
//     public class DangerSenseEffect : PlayerDrawLayer
//     {
//         // Define que desenha depois da cabeça/capacete
//         public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

//         public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
//         {
//             if (drawInfo.drawPlayer == null || !drawInfo.drawPlayer.active) return false;

//             if (!drawInfo.drawPlayer.TryGetModPlayer<DangerSensePlayer>(out var modPlayer))
//                 return false;

//             return modPlayer.VisualTimer > 0 && !drawInfo.drawPlayer.dead;
//         }

//         protected override void Draw(ref PlayerDrawSet drawInfo)
//         {
//             if (!drawInfo.drawPlayer.TryGetModPlayer<DangerSensePlayer>(out var modPlayer)) return;
    
//             if (!ModContent.HasAsset("MyHeroMod/Assets/Effects/DangerSenseEffect")) return;

//             Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/Effects/DangerSenseEffect").Value;
        

//             // CONFIGURAÇÃO DA ANIMAÇÃO
//             int totalFrames = 8; 
//             int timer = modPlayer.VisualMaxTimer - modPlayer.VisualTimer; 
//             int frameDuration = modPlayer.VisualMaxTimer / totalFrames;
            
//             if (frameDuration < 1) frameDuration = 1;

//             int currentFrame = timer / frameDuration;
//             if (currentFrame >= totalFrames) currentFrame = totalFrames - 1;

//             // Recorte do Sprite Sheet
//             int frameHeight = texture.Height / totalFrames;
//             Rectangle sourceRectangle = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            
//             Vector2 drawPos = drawInfo.Center - Main.screenPosition;
//             drawPos.Y -= 50f; 
//             drawPos.Y += drawInfo.drawPlayer.gfxOffY; 

//             // Cor e Luz
//             Lighting.AddLight(drawInfo.Center, Color.Cyan.ToVector3() * 0.8f);

//             DrawData drawData = new DrawData(
//                 texture,
//                 drawPos,
//                 sourceRectangle,
//                 Color.White, 
//                 0f, // Rotação
//                 new Vector2(texture.Width / 2f, frameHeight / 2f), 
//                 1f, // Escala
//                 drawInfo.playerEffect, 
//                 0
//             );

//             drawInfo.DrawDataCache.Add(drawData);
//         }
//     }
// }