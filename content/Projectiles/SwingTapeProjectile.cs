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

            // Se o jogador morrer, cancelar o buff ou soltar o clique (se for uma skill segurada), destrói a fita
            if (player.dead || !player.active || player.controlJump)
            {
                Projectile.Kill();
                return;
            }

            // FASE 1: Voando até achar um bloco
            if (!isStuck)
            {
                // Gira o sprite da ponta da fita para a direção que está voando
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

                // Checa colisão com blocos sólidos manualmente
                Vector2 checkPos = Projectile.position + Projectile.velocity;
                if (Collision.SolidCollision(checkPos, Projectile.width, Projectile.height))
                {
                    isStuck = true; // Grudou!
                    stuckPosition = Projectile.Center; // Salva o exato pixel onde grudou
                    Projectile.velocity = Vector2.Zero; // Para de voar
                    
                    // Salva a distância exata entre o jogador e a parede (o tamanho da corda)
                    ropeLength = Vector2.Distance(player.Center, stuckPosition);
                    
                    SoundEngine.PlaySound(SoundID.Tink, Projectile.position); // Som de bater
                }
            }
            // FASE 2: Grudado e Balançando 
            else
            {
                // Trava o projétil na parede para ele não cair
                Projectile.Center = stuckPosition;

                // Calcula a direção e a distância atual do jogador para o gancho
                Vector2 playerToHook = stuckPosition - player.Center;
                float currentDistance = playerToHook.Length();

                // Se o jogador tentar ir mais longe do que a corda permite...
                if (currentDistance > ropeLength)
                {
                    playerToHook.Normalize(); // Pega apenas a direção

                    // 1. Puxa o jogador de volta para a borda do círculo (mantém a corda esticada)
                    player.Center = stuckPosition - (playerToHook * ropeLength);

                    // 2. Física de Pêndulo: Cancela a velocidade que está "rasgando" a corda, 
                    // mas mantém a velocidade que está indo para os lados!
                    Vector2 pullVelocity = Vector2.Dot(player.velocity, playerToHook) * playerToHook;
                    if (Vector2.Dot(player.velocity, playerToHook) < 0)
                    {
                        player.velocity -= pullVelocity;
                    }
                }

                // Permite que o jogador use A e D para pegar embalo enquanto balança!
                if (player.controlLeft) player.velocity.X -= 0.5f;
                if (player.controlRight) player.velocity.X += 0.5f;

                // Avisa ao Terraria que o jogador está pendurado (evita dano de queda e reseta o pulo)
                player.fallStart = (int)(player.position.Y / 16f);
                
                
                // player.fullRotation = playerToHook.ToRotation() - MathHelper.PiOver2;
                // player.fullRotationOrigin = player.Hitbox.Size() / 2f;
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

            // Loop para desenhar os elos
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
}}