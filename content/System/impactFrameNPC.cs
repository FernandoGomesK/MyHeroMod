using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.System
{
    public class ImpactFrameNPC : GlobalNPC
    {
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (ImpactFrameSystem.ImpactTimer > 0)
            {
             
                SpriteEffects effects = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                
            
                spriteBatch.Draw(
                    TextureAssets.Npc[npc.type].Value, 
                    npc.Center - screenPos, 
                    npc.frame, 
                    Color.Black, 
                    npc.rotation, 
                    npc.frame.Size() / 2, 
                    npc.scale, 
                    effects, 
                    0
                );
                
                return false; 
            }
            
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
}