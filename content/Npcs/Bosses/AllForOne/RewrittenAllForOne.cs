using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyHeroMod.content.Quirks.OFA8th.Projectiles.TexasSmash;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Npcs.Bosses.AllForOne
{
    [AutoloadBossHead]
    public class AllForOneBoss : ModNPC
    {
        // ── Masked sheet (AllForOneBoss.png): 62x58, 13 frames ─────────────
        const int MASKED_W = 62, MASKED_H = 58;
        const int M_IDLE_START = 0, M_IDLE_END = 7;
        const int M_ATTACK_START = 8, M_ATTACK_END = 12;

        // ── Maskless sheet (AllForOneBoss_Maskless.png): 80x62, 26 frames ──
        const int MASKLESS_W = 80, MASKLESS_H = 62;
        const int L_IDLE_START = 0, L_IDLE_END = 7;
        const int L_THROW_START = 8, L_THROW_END = 13;   // frame 8-13: throw / shoot animation
        const int L_GROW_START = 14, L_GROW_END = 18;     // frames 14-18: arm growing
        const int L_PUNCH_START = 19, L_PUNCH_END = 21;   // frames 19-21: punch windup, launches on 21
        const int L_PUNCH_FREEZE = 22;                     // frame 22: held during the dash
        const int L_FOLLOW_A = 23, L_FOLLOW_B = 24;

        const int PUNCH_WINDUP_TICKS = 12;  // how long frames 19-21 take at the end of the windup
        const int FOLLOW_A_TICKS = 12;

        const int FRAME_SPEED_IDLE = 8;
        const int FRAME_SPEED_ATTACK = 10;
        const int FRAME_SPEED_GROW = 8;
        const int FRAME_SPEED_DASH = 5;

        const int FRAME_SPEED_THROW = 8;
        const int HOVER_TIME = 60;

        // ── Dash tuning ────────────────────────────────────────────────────

        const int DASHES_PER_CHAIN = 3;
        const int DASH_WINDUP = 35;   // slow down + aim: the player's tell / dodge prep
        const int DASH_ACTIVE = 25;   // full-speed dash
        const int DASH_RECOVER = 35;  // glide to a stop; gap before the next dash
        const float DASH_SPEED = 20f;

        // ───────────────────────────────────────────────────────────────────

        public enum BossState
        {
            Phase1_FloatAndAim,
            Phase1_ShootRivetStab,
            Phase2_Transition,
            Phase2_Hover,
            Phase2_DashCombo,
            Phase2_Shoot
        }

        public BossState State
        {
            get => (BossState)NPC.ai[0];
            set => NPC.ai[0] = (float)value;
        }

        public ref float Timer => ref NPC.ai[1];
        public ref float AttackCounter => ref NPC.ai[2];

        int visualFrame; 
        bool Maskless => State >= BossState.Phase2_Transition;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;

            Main.npcFrameCount[Type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 62;
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
        }

        // =========================================================================
        // ANIMATION + DRAWING
        // =========================================================================
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            int start, end, speed;
            bool hold = false; 

            switch (State)
            {
                case BossState.Phase1_ShootRivetStab:
                    start = M_ATTACK_START; end = M_ATTACK_END;
                    speed = FRAME_SPEED_ATTACK; hold = true;
                    break;

                case BossState.Phase2_Transition:
                case BossState.Phase2_Hover:
                    start = L_IDLE_START; end = L_IDLE_END;
                    speed = FRAME_SPEED_IDLE;
                    break;

                case BossState.Phase2_Shoot:
                    start = L_THROW_START; end = L_THROW_END;
                    speed = FRAME_SPEED_THROW; hold = true;
                    break;

                case BossState.Phase2_DashCombo:
                    visualFrame = GetDashFrame();
                    return; 

                default: 
                    start = M_IDLE_START; end = M_IDLE_END;
                    speed = FRAME_SPEED_IDLE;
                    break;
            }

            if (visualFrame < start || visualFrame > end)
            {
                visualFrame = start;
                NPC.frameCounter = 0;
            }

            if (++NPC.frameCounter >= speed)
            {
                NPC.frameCounter = 0;
                visualFrame++;
                if (visualFrame > end)
                    visualFrame = hold ? end : start;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool maskless = Maskless;

            Texture2D tex = maskless
                ? ModContent.Request<Texture2D>(Texture + "_Maskless").Value
                : TextureAssets.Npc[Type].Value;

            int w = maskless ? MASKLESS_W : MASKED_W;
            int h = maskless ? MASKLESS_H : MASKED_H;

            Rectangle source = new Rectangle(0, visualFrame * h, w, h);
            Vector2 origin = new Vector2(w / 2f, h / 2f);
            Vector2 drawPos = NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY);

            SpriteEffects fx = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(tex, drawPos, source, drawColor, NPC.rotation, origin, NPC.scale, fx, 0f);
            return false;
        }

        // =========================================================================
        // MAIN AI HUB
        // =========================================================================
        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
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

            if (State != BossState.Phase2_DashCombo)
                NPC.direction = NPC.Center.X < player.Center.X ? 1 : -1;

            if (NPC.life <= NPC.lifeMax / 2 && State < BossState.Phase2_Transition)
            {
                State = BossState.Phase2_Transition;
                Timer = 0;
                NPC.netUpdate = true;
            }

            switch (State)
            {
                case BossState.Phase1_FloatAndAim:
                    DoPhase1_Float(player);
                    break;
                case BossState.Phase1_ShootRivetStab:
                    DoPhase1_Shoot(player);
                    break;
                case BossState.Phase2_Transition:
                    DoPhase2_Transition(player);
                    break;
                case BossState.Phase2_Hover:
                    DoPhase2_Hover(player);
                    break;
                case BossState.Phase2_DashCombo:
                    DoPhase2_Dash(player);
                    break;
                case BossState.Phase2_Shoot:
                    DoPhase2_Shoot(player);
                    break;
            }
        }

        // =========================================================================
        // ATTACK LOGIC METHODS
        // =========================================================================

        private void DoPhase1_Float(Player player)
        {
            float sideOffset = NPC.direction * -320f;
            float heightOffset = -160f + (float)Math.Sin(Main.GameUpdateCount * 0.04f) * 18f;

            Vector2 targetPos = player.Center + new Vector2(sideOffset, heightOffset);
            Vector2 delta = targetPos - NPC.Center;
            float dist = delta.Length();

            if (dist > 8f)
            {
                Vector2 dir = delta / dist;
                float desiredSpeed = MathHelper.Min(dist * 0.08f, 5f);
                NPC.velocity = (NPC.velocity * 18f + dir * desiredSpeed) / 19f;
            }
            else
            {
                NPC.velocity *= 0.85f;
            }

            Timer++;
            if (Timer >= 160)
            {
                State = BossState.Phase1_ShootRivetStab;
                Timer = 0;
                AttackCounter = 0;
                NPC.netUpdate = true;
            }
        }

        private void DoPhase1_Shoot(Player player)
        {
            NPC.velocity *= 0.9f;
            Timer++;

            if (Timer == 30)
            {
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

            if (Timer >= 60)
            {
                State = BossState.Phase1_FloatAndAim;
                Timer = 0;
                NPC.netUpdate = true;
            }
        }

        private void DoPhase2_Shoot(Player player)
        {
            NPC.velocity *= 0.9f;
            Timer++;

            if (Timer == 30)
            {
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

            if (Timer >= 60)
            {
                State = BossState.Phase2_Hover;
                Timer = 0;
                NPC.netUpdate = true;
            }
        }

        private void DoPhase2_Transition(Player player)
        {
            NPC.velocity *= 0.9f;
            Timer++;

            if (Timer == 1)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                // Maskless sprite is bigger: resize hitbox around the same center
                Vector2 c = NPC.Center;
                NPC.width = 70;
                NPC.height = 58;
                NPC.Center = c;

                for (int i = 0; i < 40; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Bone, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 0, default, 1.5f);
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), 0, default, 2f);
                }
            }

            if (Timer >= 60)
            {
                State = BossState.Phase2_Hover;
                Timer = 0;
                AttackCounter = 0;
                NPC.netUpdate = true;
            }
        }

        private void DoPhase2_Hover(Player player)
        {
            Vector2 targetPos = player.Center + new Vector2(0, -250f);
            Vector2 dir = targetPos - NPC.Center;

            if (dir.Length() > 20f)
            {
                dir.Normalize();
                NPC.velocity = (NPC.velocity * 20f + dir * 8f) / 21f;
            }

            Timer++;

            // Checks at tick 60 and 120 for a random chance to trigger Phase2_Shoot
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Timer == 60 || Timer == 120)
                {
                    if (Main.rand.NextBool()) 
                    {
                        State = BossState.Phase2_Shoot;
                        Timer = 0;
                        NPC.netUpdate = true;
                        return;
                    }
                }
            }

        
            if (Timer >= 180)
            {
                State = BossState.Phase2_DashCombo;
                Timer = 0;
                AttackCounter = 0;
                NPC.netUpdate = true;
            }
        }

        private int GetDashFrame()
        {
            int t = (int)Timer;

            // WINDUP: arm grows (14-18), then the punch winds up (19-21), reaching 21 as the dash launches
            if (t <= DASH_WINDUP)
            {
                int punchStartTick = DASH_WINDUP - PUNCH_WINDUP_TICKS;

                if (t < punchStartTick)
                {
                    int growFrames = L_GROW_END - L_GROW_START + 1;
                    float p = t / (float)punchStartTick;
                    return L_GROW_START + Math.Min((int)(p * growFrames), growFrames - 1);
                }

                int punchFrames = L_PUNCH_END - L_PUNCH_START + 1;
                float q = (t - punchStartTick) / (float)PUNCH_WINDUP_TICKS;
                return L_PUNCH_START + Math.Min((int)(q * punchFrames), punchFrames - 1);
            }

            // DASH: frozen on frame 22 while he's flying
            if (t <= DASH_WINDUP + DASH_ACTIVE)
                return L_PUNCH_FREEZE;

            // RECOVERY: frame 23, then frame 24 until the next windup starts
            int rec = t - (DASH_WINDUP + DASH_ACTIVE);
            return rec < FOLLOW_A_TICKS ? L_FOLLOW_A : L_FOLLOW_B;
        }

        private void DoPhase2_Dash(Player player)
        {
            // 1) WINDUP: brake and keep aiming. The dash direction is locked at the end.
            if (Timer < DASH_WINDUP)
            {
                NPC.velocity *= 0.92f;

                // Keep facing the player while winding up
                NPC.direction = NPC.Center.X < player.Center.X ? 1 : -1;

                if (Timer % 5 == 0)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0f, 0f, 0, default, 1.2f);
            }
            // 2) DASH: fire once, on the exact frame the windup ends
            else if (Timer == DASH_WINDUP)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                Vector2 dashDir = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitY);
                NPC.velocity = dashDir * DASH_SPEED;
                NPC.netUpdate = true;
            }
            // 3) RECOVER: glide to a stop
            else if (Timer > DASH_WINDUP + DASH_ACTIVE)
            {
                NPC.velocity *= 0.94f;
                Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood,
                    NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f, 0, default, 1.5f);
            }

            Timer++;

            if (Timer >= DASH_WINDUP + DASH_ACTIVE + DASH_RECOVER)
            {
                AttackCounter++; // count the dash that just finished
                Timer = 0;
                NPC.netUpdate = true;

                if (AttackCounter >= DASHES_PER_CHAIN)
                {
                    State = BossState.Phase2_Hover;
                    AttackCounter = 0;
                }
            }
        }
    }
}