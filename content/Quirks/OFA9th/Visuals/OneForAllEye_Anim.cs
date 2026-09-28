using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MyHeroMod.content.Items.Armor;
using MyHeroMod.content.Items.Armor.Endeavor.FirstCostume;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.OFA9th;

namespace MyHeroMod.content.System
{
    public class OneForAllEyeAnim : PlayerDrawLayer
    {
        
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) 
        {
            var player = drawInfo.drawPlayer.GetModPlayer<OneForAll9thPlayer>();
            var afoPlayer = drawInfo.drawPlayer.GetModPlayer<AllForOnePlayer>();
            var mp = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
            
            bool hasOFA = mp.HasActiveQuirk(QuirkType.OneForAll9th) || (mp.HasActiveQuirk(QuirkType.AllForOne) && afoPlayer.HasInternalQuirk(QuirkType.OneForAll9th));
            
           
            return hasOFA && player.isFullCowlingBuffActive && player.percentage >= 45 && !drawInfo.drawPlayer.dead;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            var mp = player.GetModPlayer<TransformationPlayer>();
            bool hasExplosion = mp.ActiveQuirks.Contains(QuirkType.Explosion);
            bool hasAFO = mp.ActiveQuirks.Contains(QuirkType.AllForOne);

            Texture2D texture;
            Color drawColor = Color.White;

            if (ofaPlayer.percentage >= 65)
            {
                if (hasAFO)
                {
                    texture = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/OFA9th/Visuals/ColorlessOneForAllEye100Anim").Value;
                    drawColor = Color.Red;
                }
                else if (hasExplosion)
                {
                    texture = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/OFA9th/Visuals/ColorlessOneForAllEye100Anim").Value;
                    drawColor = Color.Orange;                    
                }
                else
                {
                    texture = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/OFA9th/Visuals/OneForAllEye100Anim").Value;
                }
            }
            else 
            {
                if (hasAFO)
                {
                    texture = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/OFA9th/Visuals/ColorlessOneForAllEyeAnim").Value;
                    drawColor = Color.Red;
                }
                else if (hasExplosion)
                {
                    texture = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/OFA9th/Visuals/ColorlessOneForAllEyeAnim").Value;
                    drawColor = Color.Orange;
                }
                else
                {
                  texture = ModContent.Request<Texture2D>("MyHeroMod/content/Quirks/OFA9th/Visuals/OneForAllEyeAnim").Value;  
                }
                
            }

            
            int frameCount = 4; 
            int frameSpeed = 5; 
            int currentFrame = (int)((Main.GameUpdateCount / frameSpeed) % frameCount);
            int frameHeight = texture.Height / frameCount; 

          
            Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

            
            Vector2 drawPos = (drawInfo.Position - Main.screenPosition) + new Vector2(player.width / 2, player.height - player.bodyFrame.Height + 4) + drawInfo.headVect;
            drawPos.Y += player.gfxOffY;
            Vector2 ajuste = new Vector2(-20f, 0f);
            drawPos += ajuste;

           
            DrawData drawData = new DrawData(
                texture,
                drawPos.Floor(), 
                sourceRect,
                drawColor, 
                player.headRotation,
                drawInfo.headVect, 
                1f,
                drawInfo.playerEffect,
                0
            );

            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}