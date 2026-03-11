using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.FaJin;

namespace MyHeroMod.content.Quirks.Erasure.Projectiles
{
    public class ErasureController : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj"; // Coloque qualquer textura transparente ou minúscula, ele não vai aparecer

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.hide = true; // Invisível!
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var erasurePlayer = player.GetModPlayer<ErasurePlayer>();

            // erasurePlayer.eyeTimer <= 0

            // 1. Morre se o jogador morrer, se a skill for desligada, ou se o tempo de piscar acabar
            if (player.dead || !player.active || !erasurePlayer.isErasureActive)
            {
                erasurePlayer.isErasureActive = false;
                Projectile.Kill();
                return;
            }

            // 2. Gruda no Jogador
            Projectile.Center = player.Center;
            Projectile.timeLeft = 2; // Mantém vivo para o próximo frame
            
            // Opcional: Efeito visual sutil de olho vermelho flutuando na cara do jogador
            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(player.Top - new Vector2(0, 10), player.width, 10, DustID.RedTorch);
                d.noGravity = true;
                d.velocity *= 0.1f;
            }

            // 3. A Lógica do Cone de Visão
            Vector2 aimDirection = Main.MouseWorld - player.Center;
            aimDirection.Normalize();

            float visionRange = 600f; // 600 pixels de alcance (Quase a tela toda)
            
            // Varre TODOS os NPCs próximos
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.friendly || npc.townNPC) continue;

                float distance = Vector2.Distance(player.Center, npc.Center);
                
                if (distance < visionRange)
                {
                    // Descobre a direção entre o player e o monstro
                    Vector2 directionToNpc = npc.Center - player.Center;
                    directionToNpc.Normalize();

                    // O Dot Product checa o ângulo. 
                    // 1 = Exatamente na frente. 0 = Do lado. -1 = Nas costas.
                    // 0.8f dá um "cone" bem razoável na frente do personagem!
                    float angleDifference = Vector2.Dot(aimDirection, directionToNpc);

                    // Se estiver no cone (maior que 0.8) E tiver linha de visão (sem paredes na frente)
                    if (angleDifference > 0.8f && Collision.CanHitLine(player.position, player.width, player.height, npc.position, npc.width, npc.height))
                    {
                        // APLICA O DEBUFF DE APAGAR QUIRK!
                        var globalNPC = npc.GetGlobalNPC<QuirkGlobalNPC>();
                        if (globalNPC.HasQuirk)
                        {
                            globalNPC.IsQuirkErased = true; // Você precisa criar essa variável lá no seu QuirkGlobalNPC!
                            
                            // Efeito visual no monstro avisando que ele perdeu a Quirk
                            if (Main.rand.NextBool(5))
                            {
                                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Wraith); // Fumacinha preta
                                d.velocity *= 0.5f;
                            }
                        }
                    }
                }
            }
        }
    }
}