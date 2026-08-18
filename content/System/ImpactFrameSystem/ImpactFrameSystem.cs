using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using System;

namespace MyHeroMod.content.System
{
    public class ImpactFrameSystem : ModSystem
    {
        public static int ImpactTimer = 4;
        
       
        public static string[] ImpactTextures;
        public static Color ActiveColor = Color.White; 

        public static SpriteEffects ActiveSpriteEffect = SpriteEffects.None;
        
    
        private const int FramesPerStage = 2; 
        public override void Load()
        {
            if (!Main.dedServ) 
            {
               
                foreach (string fileName in Mod.GetFileNames())
                {
                    
                    if (fileName.StartsWith("Assets/Effects/") && (fileName.EndsWith(".rawimg") || fileName.EndsWith(".png")))
                    {
                        
                        string cleanPath = fileName.Replace(".rawimg", "").Replace(".png", "");
                        
                
                        string fullPath = Mod.Name + "/" + cleanPath;
                        
                    
                        ModContent.Request<Texture2D>(fullPath, ReLogic.Content.AssetRequestMode.AsyncLoad);
                    }
                }
            }
        }
       
        public static void Trigger(Color color, bool flipSprite, params string[] textures)
        {
            ImpactTextures = textures;
            ActiveColor = color;

            ActiveSpriteEffect = flipSprite ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            
            
            int totalImages = textures != null ? textures.Length : 0;
            ImpactTimer = FramesPerStage + (totalImages * FramesPerStage);
        }

        public override void PostUpdateEverything()
        {
            if (ImpactTimer > 0)
                ImpactTimer--;
            else
                ImpactTextures = null;
        }

        public override void PostDrawTiles()
        {
            var config = ModContent.GetInstance<MyHeroConfig>();

            if (ImpactTimer <= 0 || !ModContent.GetInstance<MyHeroConfig>().EnableImpactFrames)
                return;

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            int totalImageFrames = ImpactTextures != null ? ImpactTextures.Length * FramesPerStage : 0;

            float intensity = config.ImpactFrameIntensity;

            
            if (ImpactTimer > totalImageFrames)
            {
                Main.spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value,
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
                    Color.White * intensity
                );
            }
           
            else if (ImpactTextures != null && ImpactTextures.Length > 0)
            {
                
                int currentIndex = ImpactTextures.Length - 1 - ((ImpactTimer - 1) / FramesPerStage);
                currentIndex = Math.Clamp(currentIndex, 0, ImpactTextures.Length - 1);

                Texture2D customImpact = ModContent.Request<Texture2D>(ImpactTextures[currentIndex]).Value;
                
                Main.spriteBatch.Draw(
                    customImpact,
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
                    null, 
                    ActiveColor * intensity,
                    0f, 
                    Vector2.Zero, 
                    ActiveSpriteEffect,
                    0f 
                );
            }

            Main.spriteBatch.End();
        }
    }
}