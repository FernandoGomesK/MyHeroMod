using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA8th.Projectiles.TexasSmash;
using System;


namespace MyHeroMod.content.Npcs.Bosses.AllForOne
{
    public class AllForOneBoss : ModNPC
    {
        // ── Constantes de frame ──────────────────────────────────────────
        const int FRAME_FLOAT_START  = 2;   
        const int FRAME_FLOAT_END    = 14;  
        const int FRAME_ATTACK_START = 21;  
        const int FRAME_ATTACK_FIRE  = 23; 
        const int FRAME_ATTACK_END   = 24;  

        const int FRAME_SPEED_FLOAT  = 8;   
        const int FRAME_SPEED_ATTACK = 10;   

        

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
            Main.npcFrameCount[Type] = 25;
        }

        public override void SetDefaults()
        {
            NPC.width  = 34;
            NPC.height = 56; 
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
        }

    
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            int start, end, speed;

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
                    {
                        
                        NPC.frame.Y = end * frameHeight;
                    }
                    else
                    {
                        NPC.frame.Y = start * frameHeight; 
                    }
                }
            }
        }

       
        public override void AI()
        {
            
            if (NPC.target < 0 || NPC.target == 255 ||
                Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            
            if (!NPC.HasPlayerTarget)
            {
                NPC.velocity.Y -= 0.1f;
                if (NPC.timeLeft > 60) NPC.timeLeft = 60;
                return;
            }

           
            NPC.direction = NPC.Center.X < player.Center.X ? 1 : -1;

            
            
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

            const int COOLDOWN   = 150; 
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
                                NPC.damage / 2, 
                                0f, Main.myPlayer
                            );
                        }
                    }
                    else 
                    {
                        
                        Vector2 shootDir = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                        int p = Projectile.NewProjectile(
                            NPC.GetSource_FromAI(), NPC.Center, shootDir * 16f,
                            ModContent.ProjectileType<PrimeTexasSmashProj>(),
                            NPC.damage, 
                            0f, Main.myPlayer
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
            }}}}