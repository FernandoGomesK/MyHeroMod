using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using MyHeroMod.content.Quirks.OFA8th.Projectiles.TexasSmash;
using System;

namespace MyHeroMod.content.Npcs.Bosses.AllForOne
{
    [AutoloadBossHead]
    public class AllForOneBoss : ModNPC
    {
        // ── Constantes de frame ──────────────────────────────────────────
        // Fase 1
        const int FRAME_FLOAT_START  = 2;   
        const int FRAME_FLOAT_END    = 14;  
        const int FRAME_ATTACK_START = 21;  
        const int FRAME_ATTACK_FIRE  = 23; 
        const int FRAME_ATTACK_END   = 24;  

        // Fase 2 (Máscara Quebrada e Investida)
        const int FRAME_BROKEN_FLOAT_START = 25;
        const int FRAME_BROKEN_FLOAT_END   = 34; 
        
        
        const int FRAME_DASH_START         = 35; 
        const int FRAME_DASH_END           = 40;

        const int FRAME_SPEED_FLOAT  = 8;   
        const int FRAME_SPEED_ATTACK = 10;   
        const int FRAME_SPEED_DASH   = 5;

        public bool IsAttacking
        {
            get => NPC.ai[1] == 1f;
            set
            {
                if (value == IsAttacking) return; 
                NPC.ai[1] = value ? 1f : 0f;
                
                NPC.frame.Y = value
                    ? FRAME_ATTACK_START * (NPC.frame.Height == 0 ? 56 : NPC.frame.Height)
                    : FRAME_FLOAT_START  * (NPC.frame.Height == 0 ? 56 : NPC.frame.Height);
                NPC.frameCounter = 0;
            }
        }

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Atualizado para 51 (lembre-se: frame 50 = 51 frames totais do 0 ao 50)
            Main.npcFrameCount[Type] = 41; 
        }

        public override void SetDefaults()
        {
            NPC.width  = 62; 
            NPC.height = 54; 
            NPC.damage = 100;
            NPC.defense = 50;
            NPC.lifeMax = 50000;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(gold: 50);
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.boss = true;

            if (!Main.dedServ) 
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/AllForOneTheme");
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            int start = 0, end = 0, speed = 10;

            // FASE 1 (Magias com a máscara)
            if (NPC.ai[3] == 0f)
            {
                if (IsAttacking)
                {
                    start = FRAME_ATTACK_START;
                    end   = FRAME_ATTACK_END;
                    speed = FRAME_SPEED_ATTACK;
                }
                else
                {
                    start = FRAME_FLOAT_START;
                    end   = FRAME_FLOAT_END;
                    speed = FRAME_SPEED_FLOAT;
                }
            }
            // FASE 2 (Investida Brutal / Máscara Quebrada)
            else 
            {
                if (IsAttacking) // Quando ele dá o Dash (Braço Gigante)
                {
                    start = FRAME_DASH_START;
                    end   = FRAME_DASH_END;
                    speed = FRAME_SPEED_DASH;
                }
                else // Flutuando e esperando o próximo dash (Máscara Quebrada)
                {
                    start = FRAME_BROKEN_FLOAT_START;
                    end   = FRAME_BROKEN_FLOAT_END;
                    speed = FRAME_SPEED_FLOAT;
                }
            }

            // Lógica padrão para tocar a animação corretamente
            if (NPC.frame.Y < start * frameHeight || NPC.frame.Y > end * frameHeight)
            {
                NPC.frame.Y    = start * frameHeight;
                NPC.frameCounter = 0;
                return; 
            }

            NPC.frameCounter++;
            if (NPC.frameCounter >= speed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > end * frameHeight)
                {
                    if (IsAttacking)
                        NPC.frame.Y = end * frameHeight;
                    else
                        NPC.frame.Y = start * frameHeight; 
                }
            }
        }

        public override void AI()
        {
            // 1. VALIDAÇÃO DE ALVO
            if (NPC.target < 0 || NPC.target == 255 ||
                Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            if (!NPC.HasPlayerTarget)
            {
                NPC.velocity.Y -= 0.1f; // Foge para o céu se o jogador morrer
                if (NPC.timeLeft > 60) NPC.timeLeft = 60;
                return;
            }

            NPC.direction = NPC.Center.X < player.Center.X ? 1 : -1;

            // 2. VERIFICAÇÃO DE TRANSIÇÃO PARA A FASE 2
            if (NPC.life <= NPC.lifeMax / 2 && NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f; // Entrou na Fase 2
                NPC.ai[0] = 0f; // Reseta o relógio principal
                NPC.ai[2] = 0f;
                IsAttacking = false;

                // Efeito da "Máscara Quebrando"
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                for (int i = 0; i < 40; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Bone, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 0, default, 1.5f);
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), 0, default, 2f);
                }
                
                NPC.netUpdate = true;
            }

            // 3. COMPORTAMENTOS DAS FASES
            if (NPC.ai[3] == 0f)
            {
                // ==========================================
                // FASE 1: FLUTUAR E ATIRAR
                // ==========================================
                float sideOffset  = NPC.direction * -320f;
                float heightOffset = -160f + (float)Math.Sin(NPC.ai[0] * 0.04f) * 18f;
                
                Vector2 targetPos = player.Center + new Vector2(sideOffset, heightOffset);
                Vector2 delta     = targetPos - NPC.Center;
                float   dist      = delta.Length();

                float maxSpeed = IsAttacking ? 2f : 5f; 
                float lerpBase = IsAttacking ? 30f : 18f;

                if (dist > 8f)
                {
                    Vector2 dir = delta / dist; 
                    float   desiredSpeed = MathHelper.Min(dist * 0.08f, maxSpeed);
                    NPC.velocity = (NPC.velocity * lerpBase + dir * desiredSpeed) / (lerpBase + 1f);
                }
                else
                {
                    NPC.velocity *= 0.85f;
                }

                NPC.ai[0]++; 

                const int ANIM_START = 160;
                const int FIRE_TICK  = 190; 
                const int RESET_TICK = 200; 

                if (NPC.ai[0] == ANIM_START)
                {
                    IsAttacking = true;
                    NPC.ai[2] = 0f; 
                }

                if (NPC.ai[0] >= FIRE_TICK && NPC.ai[2] == 0f)
                {
                    NPC.ai[2] = 1f;

                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                    {
                        if (Main.rand.NextBool()) 
                        {
                            for (int i = -1; i <= 1; i++) 
                            {
                                Vector2 baseDir = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                                Vector2 spreadDir = baseDir.RotatedBy(MathHelper.ToRadians(30 * i));
                                
                                Projectile.NewProjectile(
                                    NPC.GetSource_FromAI(), NPC.Center, spreadDir * 8f, 
                                    ModContent.ProjectileType<Projectiles.RivetStabProj>(),
                                    NPC.damage / 2, 0f, Main.myPlayer
                                );
                            }
                        }
                        else 
                        {
                            Vector2 shootDir = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                            int p = Projectile.NewProjectile(
                                NPC.GetSource_FromAI(), NPC.Center, shootDir * 16f,
                                ModContent.ProjectileType<PrimeTexasSmashProj>(),
                                NPC.damage, 0f, Main.myPlayer
                            );
                            Main.projectile[p].friendly = false;
                            Main.projectile[p].hostile = true;
                        }
                    }
                }

                if (NPC.ai[0] >= RESET_TICK)
                {
                    IsAttacking = false; 
                    NPC.ai[0] = 0f;      
                    NPC.ai[2] = 0f;
                }
            }
            else
            {
                // ==========================================
                // FASE 2: INVESTIDAS AGRESSIVAS (Estilo Olho de Cthulhu)
                // ==========================================
                NPC.ai[0]++; // Relógio principal da Fase 2

                // ── ESTADO 1: FLUTUAR E MIRAR ──
                if (NPC.ai[2] == 0f)
                {
                    IsAttacking = false; // Recolhe o braço (Usa a animação da Máscara Quebrada)
                    
                    Vector2 targetPos = player.Center + new Vector2(0, -250f);
                    Vector2 dir = targetPos - NPC.Center;
                    
                    if (dir.Length() > 20f)
                    {
                        dir.Normalize();
                        NPC.velocity = (NPC.velocity * 20f + dir * 8f) / 21f; 
                    }

                    if (NPC.ai[0] >= 60)
                    {
                        NPC.ai[0] = 0;
                        NPC.ai[2] = 1f; // Inicia o combo
                    }
                }
                // ── ESTADO 2: COMBO DE 3 DASHES ──
                else
                {
                    if (NPC.ai[0] == 1)
                    {
                        IsAttacking = true; // Estica o braço gigante
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        
                        Vector2 dashDir = player.Center - NPC.Center;
                        dashDir.Normalize();
                        
                        NPC.velocity = dashDir * 20f; 
                    }
                    
                    if (NPC.ai[0] > 1 && NPC.ai[0] < 40)
                    {
                        Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f, 0, default, 1.5f);
                        NPC.velocity *= 0.97f; 
                    }

                    if (NPC.ai[0] >= 40)
                    {
                        NPC.ai[0] = 0; 
                        NPC.ai[2]++;   

                        if (NPC.ai[2] > 3f)
                        {
                            NPC.ai[2] = 0f;
                            IsAttacking = false; // Fim do combo, volta a flutuar ofegante
                        }
                    }
                }
            }
        }
    }
}