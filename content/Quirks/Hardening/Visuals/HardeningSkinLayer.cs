//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.DataStructures;
//using Terraria.ModLoader;
//using MyHeroMod.content.Buffs;

//namespace MyHeroMod.content.Quirks.Hardening.Visuals
//{
//    public class HardeningSkinLayer : PlayerDrawLayer
//    {
//        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
//        {
//            return drawInfo.drawPlayer.HasBuff(ModContent.BuffType<HardenBuff>());
//        }

//        public override Position GetDefaultPosition()
//        {
//            return new AfterParent(PlayerDrawLayers.Head);
//        }

//        protected override void Draw(ref PlayerDrawSet drawInfo)
//        {
//            Player drawPlayer = drawInfo.drawPlayer;

//            Texture2D headTex = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/Hardening/Visuals/HardeningHead_Head").Value;
//            Texture2D bodyTex = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/Hardening/Visuals/HardeningBody_Body").Value;

//            // Calculate base drawing position correctly
//            Vector2 baseDrawPos = new Vector2(
//                (int)(drawInfo.Position.X - Main.screenPosition.X - drawPlayer.bodyFrame.Width / 2f + drawPlayer.width / 2f),
//                (int)(drawInfo.Position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f)
//            );

//            // --- DRAW HEAD ---
//            Vector2 headPos = baseDrawPos + drawPlayer.headPosition + drawInfo.headVect;
//            DrawData headData = new DrawData(headTex, headPos, drawPlayer.bodyFrame, drawInfo.colorArmorHead, drawPlayer.headRotation, drawInfo.headVect, 1f, drawInfo.playerEffect, 0);
//            drawInfo.DrawDataCache.Add(headData);

            
//            //Vector2 bodyPos = baseDrawPos + drawPlayer.bodyPosition + drawInfo.bodyVect;
//            //DrawData bodyData = new DrawData(bodyTex, bodyPos, drawPlayer.bodyFrame, drawInfo.colorArmorBody, drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
//            //drawInfo.DrawDataCache.Add(bodyData);

//            //Rectangle armFrame = drawPlayer.bodyFrame;
//            //armFrame.X += 40;

//            //DrawData armData = new DrawData(bodyTex, bodyPos, armFrame, drawInfo.colorArmorBody, drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
//            //drawInfo.DrawDataCache.Add(armData);
//        }
//    }
//}