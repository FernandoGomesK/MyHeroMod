using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;


namespace MyHeroMod.content.Quirks.Decay.Projectiles.GroundTouch
{
    public class GroundTouchController : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = false;
            Projectile.hide = true; // Invisível
            Projectile.timeLeft = 60; // A onda viaja por 1 segundo
            Projectile.tileCollide = true; // Precisa ler o terreno
        }

        public override void AI()
        {
            // O projétil viaja horizontalmente (definido na skill)
            // A cada X frames, ele planta um espinho no chão
            
            // VELOCIDADE DA CRIAÇÃO (A cada 3 frames cria um espinho)
            if (Projectile.timeLeft % 3 == 0)
            {
                // ALGORITMO PARA ACHAR O CHÃO (Raycast Down)
                // Começa na posição atual do controlador
                Vector2 groundPos = Projectile.Center;
                
                // Tenta descer até 20 blocos (320 pixels) procurando chão sólido
                // Isso permite que o gelo desça escadas ou morros
                bool foundGround = false;
                for (int y = 0; y < 20; y++)
                {
                    int tileX = (int)(groundPos.X / 16f);
                    int tileY = (int)(groundPos.Y / 16f);
                    
                    // Pega o tile na memória do jogo
                    Tile tile = Main.tile[tileX, tileY];

                    // LÓGICA NOVA:
                    // 1. tile.HasTile: Tem algum bloco ali?
                    // 2. Main.tileSolid: É um bloco sólido (terra, pedra)?
                    // 3. Main.tileSolidTop: É um topo sólido (Plataforma, Planter Box)?
                    bool isGround = tile.HasTile && (Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType]);

                    if (isGround) 
                    {
                        groundPos.Y = tileY * 16f;
                        foundGround = true;
                        break; // Para no primeiro chão que encontrar (seja plataforma ou terra)
                    }
                    
                    groundPos.Y += 16f;
                }

                // Se achou chão, planta o espinho
                if (foundGround)
                {
                    // Ajuste: Subir um pouco para o espinho não nascer totalmente enterrado
                    groundPos.Y -= 20f; 

                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        groundPos,
                        Vector2.Zero, // Espinho parado
                        ModContent.ProjectileType<GroundTouchProj>(),
                        Projectile.damage,
                        Projectile.knockBack,
                        Projectile.owner
                    );
                }
            }
            
            // Lógica simples para subir morros (se bater na parede, tenta subir)
            // Isso evita que a onda pare no primeiro degrau
            Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
        }

       
        
        // Permite subir blocos sem morrer
        public override bool OnTileCollide(Vector2 oldVelocity) { return false; }
    }

    
}