using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using MyHeroMod.content.Buffs;



namespace MyHeroMod.content.Quirks.OFA8th.Projectiles.CaliforniaSmash
{
    public class PrimeCaliforniaSmashController : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Projectile.width = 10; 
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 120; 
            Projectile.alpha = 255; // Invisível
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

        var ofa8Player = player.GetModPlayer<OneForAll8thPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();


        float damageMultiplier = 1.0f;
        int MaxDamage = 50;
         

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                MaxDamage = 50;
                break;
            
                case QuirkStage.Adequation:
                MaxDamage = 50;
                break;
          
                case QuirkStage.Intermediate:
                MaxDamage = 110;
                break;
            
                case QuirkStage.Advanced:
                MaxDamage = 250;
                break;
          
                case QuirkStage.Final:
                MaxDamage = 700;
                break;
        
                default:
                MaxDamage =50;
                break;
                    
            }

            if (player.HasBuff(ModContent.BuffType<StockPileBuff>()) || ofa8Player.form == 1) {
                damageMultiplier = 1.5f; 
            }
            else if (player.HasBuff(ModContent.BuffType<StockPileBuff>() ) || ofa8Player.form == 2)  {
                damageMultiplier = 2.5f;
            }

            var finalDamage = (int)(damageMultiplier * MaxDamage);

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = player.Center;
            player.heldProj = Projectile.whoAmI;
            
            // --- FASE 1: SUBIDA (O Pulo) ---
            // Dura 15 frames (0.25 segundos)
            if (Projectile.ai[0] < 15)
            {
                Projectile.ai[0]++;

                // Aqui é onde ele "PULA"
                player.velocity.Y = -15f;  // Joga o player para CIMA (Aumentei para 15f para subir mais)
                
                // Animação de Giro
                player.fullRotation += 0.4f * player.direction;
                player.fullRotationOrigin = player.Size / 2;
                
                // Partículas saindo do player enquanto sobe
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0, 0, 100, default, 1f);
                }
            }
            // --- FASE 2: CÁLCULO DA MIRA (Frame 15) ---
            else if (Projectile.ai[0] == 15)
            {
                Vector2 Velocity = Main.MouseWorld - player.Center;
                Velocity.Normalize();
                Velocity *= 15f;

                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<PrimeCaliforniaSmashProj>(),
                finalDamage, 
                2f, 
                player.whoAmI); // Aplica no player

                player.fullRotation = 0f;

                 Projectile.Kill();
            }
           
           
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            
            // Para o player e reseta a rotação ao bater
            player.velocity = Vector2.Zero;
            player.fullRotation = 0f; 

            SoundEngine.PlaySound(SoundID.Item62, Projectile.position); 

            // Efeito Visual da Explosão
            for (int i = 0; i < 50; i++)
            {
                int fire = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default, 4f);
                Main.dust[fire].velocity *= 6f;
                Main.dust[fire].noGravity = true;

                int smoke = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 100, default, 3f);
                Main.dust[smoke].velocity *= 4f;
            }
            
            // Dica: Adicione dano em área aqui criando outro projétil de explosão se quiser
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill(); // Garante que exploda ao tocar inimigos
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true; // Garante que exploda ao tocar chão/parede
        }
    }
}