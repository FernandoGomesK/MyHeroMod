using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Support.UrarakaSupport.Projectiles
{
    
    public class WristHookProjectile : ModProjectile
    {
        

        public override void SetDefaults()
        {
            
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.penetrate = 1; 
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.damage = 40; 
            Projectile.DamageType = DamageClass.Magic; 
            
        }
        
        
        public override float GrappleRange() => 900f; 

        
        public override void NumGrappleHooks(Player player, ref int numHooks) => numHooks = 10;

        
        public override void GrappleRetreatSpeed(Player player, ref float speed) => speed = 25f;

        public override void GrapplePullSpeed(Player player, ref float speed)
        {
            speed = 18f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frozen, 120);  
        }

        

        public override void AI()
        {
            
            if (Projectile.ai[0] == 0f)
            {
              
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

                
                Point tileCoords = Projectile.Center.ToTileCoordinates();
                Tile tile = Main.tile[tileCoords.X, tileCoords.Y];

            
               
                   
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item52, Projectile.Center);
                
            }

            
        }

        public override bool PreDraw(ref Color lightColor)
        {
            string chainTexturePath = "MyHeroMod/content/Items/Support/UrarakaSupport/Projectiles/WristHookChain";

            if (!ModContent.HasAsset(chainTexturePath)) return true; 

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
        
            return true; 
        }
    }
}