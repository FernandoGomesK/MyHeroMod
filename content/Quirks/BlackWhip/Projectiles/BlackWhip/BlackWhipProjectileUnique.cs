using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhip
{
    
    public class BlackWhipProjectileUnique : ModProjectile
    {
        public override void SetDefaults()
        {
            
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            Projectile.width = 18;
            Projectile.height = 18;
            
        }

       
        public override float GrappleRange() => 600f; 

        
        public override void NumGrappleHooks(Player player, ref int numHooks) => numHooks = 2;

        
        public override void GrappleRetreatSpeed(Player player, ref float speed) => speed = 18f;

        
        public override bool PreDraw(ref Color lightColor)
        {
            
            string chainTexturePath = "MyHeroMod/content/Quirks/BlackWhip/Projectiles/BlackWhip/BlackWhipChain";

            
            if (!ModContent.HasAsset(chainTexturePath)) return false;

            Texture2D texture = ModContent.Request<Texture2D>(chainTexturePath).Value;

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float textureHeight = texture.Height;

            Vector2 vectorToPlayer = mountedCenter - position;
            float rotation = vectorToPlayer.ToRotation() - 1.57f;
            bool chainConnected = true;

            
            while (chainConnected)
            {
                float length = vectorToPlayer.Length();
                
                if (length < textureHeight + 1)
                {
                    chainConnected = false;
                }
                else
                {
                    Vector2 nextLink = vectorToPlayer;
                    nextLink.Normalize();
                    position += nextLink * textureHeight;
                    vectorToPlayer = mountedCenter - position;
                    
                  
                    Color color = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    
                    Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0);
                }
            }
            return false; 
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            Lighting.AddLight(Projectile.Center, 0.2f, 0.8f, 0.6f); 
            
        }
    }
}