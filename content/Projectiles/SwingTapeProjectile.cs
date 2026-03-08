using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using System;
using Terraria.Audio;

namespace MyHeroMod.content.Projectiles
{
    public class SwingTapeProjectile : ModProjectile
    {

        public override string Texture => "MyHeroMod/Assets/Projectiles/SwingTapeProjectile";
        private bool isStuck = false;
        private float ropeLength = 0f;
        private Vector2 stuckPosition;

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 3600; 
           
            Projectile.aiStyle = 0; 
        }

        public override void AI()
{
    Player player = Main.player[Projectile.owner];

    if (player.dead || !player.active || player.controlJump)
    {
        // Impulso ao soltar: mantém a velocidade tangencial atual (já está correta)
        // Mas remove o "freio" artificial que o Terraria aplica ao matar projéteis
        
        Projectile.Kill();
        return;
    }

    if (!isStuck)
{
    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

    Vector2 nextPos = Projectile.position + Projectile.velocity;

    bool hitSolid = Collision.SolidCollision(nextPos, Projectile.width, Projectile.height);

    // Checa plataformas manualmente via tile
    bool hitPlatform = false;
    int tileX = (int)((nextPos.X + Projectile.width / 2) / 16);
    int tileY = (int)((nextPos.Y + Projectile.height / 2) / 16);
    
    if (WorldGen.InWorld(tileX, tileY))
    {
        Tile tile = Main.tile[tileX, tileY];
        hitPlatform = tile.HasTile && 
                      TileID.Sets.Platforms[tile.TileType] && 
                      tile.IsActuated == false;
    }

    if (hitSolid || hitPlatform)
    {
        isStuck = true;
        stuckPosition = Projectile.Center;
        Projectile.velocity = Vector2.Zero;
        ropeLength = Vector2.Distance(player.Center, stuckPosition);
        SoundEngine.PlaySound(SoundID.Tink, Projectile.position);
    }
}
    else
    {
        Projectile.Center = stuckPosition;

        Vector2 playerToHook = stuckPosition - player.Center;
        float currentDistance = playerToHook.Length();

        float gravity = player.gravity > 0 ? player.gravity : 0.4f;
        player.velocity.Y += gravity;

        if (currentDistance > ropeLength)
        {
            Vector2 ropeDir = playerToHook;
            ropeDir.Normalize();

            player.Center = stuckPosition - (ropeDir * ropeLength);

            float radialComponent = Vector2.Dot(player.velocity, ropeDir);
            if (radialComponent < 0)
                player.velocity -= radialComponent * ropeDir;

            player.velocity *= 0.995f;
        }

        if (player.controlLeft) player.velocity.X -= 0.5f;
        if (player.controlRight) player.velocity.X += 0.5f;

        player.velocity = Vector2.Clamp(player.velocity, new Vector2(-18f, -18f), new Vector2(18f, 18f));
        player.fallStart = (int)(player.position.Y / 16f);
    }
}
    public override bool PreDraw(ref Color lightColor)
        {
            
            string chainTexturePath = "MyHeroMod/Assets/Projectiles/SwingTapeChain";

            
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
            return true; 
        }
}}