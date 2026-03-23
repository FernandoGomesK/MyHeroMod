using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System; // Para acessar o QuirkType
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.OFA8th.Projectiles.TexasSmash;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot;
using MyHeroMod.content.Quirks.Blueflames.Projectiles.BlueFlameThrower;
using MyHeroMod.content.Quirks.Erasure.Projectiles;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.IceShot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.JetBurn;

namespace MyHeroMod.content.System
{
    public class QuirkGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int ErasureTimer = 0;

        
        public bool HasQuirk = false;
        public QuirkType AssignedQuirk = QuirkType.Quirkless;

                private static Asset<Texture2D> fullCowlingTexture;


        public override void Load()
        {
            if (!Main.dedServ) // Evita rodar no servidor dedicado, onde não há gráficos
            {
                fullCowlingTexture = ModContent.Request<Texture2D>("MyHeroMod/Assets/Effects/FullCowling");
            }
        }
        public override void Unload()
        {
            fullCowlingTexture = null;
        }

        
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            
            if (npc.friendly || npc.townNPC || npc.lifeMax < 10)
                return;

            
            // Ex: NextBool(5) = 1 em 5 = 20%
            if (Main.rand.NextBool(5)) 
            {
                HasQuirk = true;

                
                
                int random = Main.rand.Next(4); 
                switch (random)
                {
                    case 0: AssignedQuirk = QuirkType.HellFlames; break;
                    case 1: AssignedQuirk = QuirkType.HalfColdHalfHot; break;
                    // case 2: AssignedQuirk = QuirkType.BlueFlames; break;
                    case 3: AssignedQuirk = QuirkType.OneForAll9th; break;
                    case 4: AssignedQuirk = QuirkType.OneForAll8th; break;
                    case 5: AssignedQuirk = QuirkType.Overclock; break;
                    case 6: AssignedQuirk = QuirkType.Float; break;
                    case 7: AssignedQuirk = QuirkType.DangerSense; break;
                    case 8: AssignedQuirk = QuirkType.BlackWhip; break;
                    case 9: AssignedQuirk = QuirkType.Gearshift; break;
                    case 10: AssignedQuirk = QuirkType.FaJin; break;
                    case 11: AssignedQuirk = QuirkType.SmokeScreen; break;
                    case 12: AssignedQuirk = QuirkType.Explosion; break;
                    case 13: AssignedQuirk = QuirkType.SuperRegeneration; break;
                    case 14: AssignedQuirk = QuirkType.Erasure; break;

                }
                
                
                npc.lifeMax = (int)(npc.lifeMax * 4f); 
                npc.life = npc.lifeMax;
                npc.damage = (int)(npc.damage * 3f);
            }
        }

        public override void ResetEffects(NPC npc)
{
    
}

        
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            
            if (HasQuirk && ErasureTimer == 0 && AssignedQuirk == QuirkType.OneForAll9th)
            {
                if (fullCowlingTexture == null || !fullCowlingTexture.IsLoaded) return;

                Texture2D texture = fullCowlingTexture.Value;

                int frameCount = 6; 
                int frameSpeed = 6; 
                int currentFrame = (int)(Main.GameUpdateCount / frameSpeed) % frameCount;

                int frameHeight = texture.Height / frameCount;
                Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

                
                Vector2 drawPos = npc.Center - screenPos;
                
                
                SpriteEffects effects = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            
                spriteBatch.Draw(
                    texture,
                    drawPos,
                    sourceRect,
                    Color.White, 
                    npc.rotation,
                    new Vector2(texture.Width / 2f, frameHeight / 2f),
                    1.2f, 
                    effects,
                    0f
                );
            }
        }

        // 2. DESENHA AS PARTÍCULAS (POEYRA)
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            
            if (HasQuirk && ErasureTimer == 0)
            {
                if (Main.GameUpdateCount % 3 == 0)
                {
                    int dustType = DustID.MagicMirror; 

                    if (AssignedQuirk == QuirkType.HellFlames) dustType = DustID.Torch; 
                    if (AssignedQuirk == QuirkType.BlueFlames) dustType = DustID.BlueTorch; 
                    if (AssignedQuirk == QuirkType.HalfColdHalfHot) dustType = DustID.IceTorch; 
                    
                    if (AssignedQuirk == QuirkType.OneForAll8th) dustType = DustID.YellowTorch; 
                    if (AssignedQuirk == QuirkType.Overclock) dustType = DustID.YellowTorch;
                    if (AssignedQuirk == QuirkType.Float) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.DangerSense) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.BlackWhip) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.Gearshift) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.FaJin) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.SmokeScreen) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.Explosion) dustType = DustID.OrangeTorch;
                    if (AssignedQuirk == QuirkType.Erasure) dustType = DustID.Wraith;

                    Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, dustType);
                    d.velocity *= 0.5f;
                    d.noGravity = true; 
                    d.scale = 1.2f; 
                }
            }
        }
        public override void OnKill(NPC npc)
        {
            if (HasQuirk && Main.rand.NextBool(2))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Items.QuirkGene>());
            }
        }

        public int quirkTimer = 0; 

        
        public override void AI(NPC npc)
        {
            if (ErasureTimer > 0) 
            {
                ErasureTimer--;
            }
            
            if (!HasQuirk || ErasureTimer > 0 || npc.friendly || npc.townNPC) return;

            quirkTimer++;

        
            npc.TargetClosest(true);
            if (npc.HasValidTarget)
            {
                Player target = Main.player[npc.target];
                Vector2 directionToPlayer = (target.Center - npc.Center).SafeNormalize(Vector2.Zero);

                // 3. A Lógica de cada Quirk
                switch (AssignedQuirk)
                {
                    case QuirkType.HalfColdHalfHot:
                    
                        if (quirkTimer % 180 == 0)
                        {
                            bool isIce = Main.rand.NextBool();
                            
                            int damage = npc.damage / 2;
                            Vector2 baseVelocity = directionToPlayer * 10f; 

                            if (isIce)
                            {
                                
                                int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, baseVelocity, ModContent.ProjectileType<IceShotProj>(), damage, 0f, Main.myPlayer);
                                Main.projectile[p].friendly = false;
                                Main.projectile[p].hostile = true;
                            }
                            else
                            {
                            
                                for (int i = 0; i < 20; i++)
                                {
                                    
                                    Vector2 spreadVel = baseVelocity.RotatedByRandom(MathHelper.ToRadians(25)) * Main.rand.NextFloat(1f, 2.5f);
                                    
                            
                                    int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, spreadVel, ModContent.ProjectileType<JetKindlingProj>(), damage, 0f, Main.myPlayer);
                                    Main.projectile[p].friendly = false;
                                    Main.projectile[p].hostile = true;
                                }
                            }
                        }
                        break;

                    case QuirkType.OneForAll9th:
                    if (quirkTimer % 180 == 0)

                        {
                            int projType = ModContent.ProjectileType<DelawareSmashProj>();
                            
                            Vector2 velocity = directionToPlayer * 10f; 
                            int damage = npc.damage / 2; 

                            
                            int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, velocity, projType, damage, 0f, Main.myPlayer);
                            
                            
                            Main.projectile[p].friendly = false;
                            Main.projectile[p].hostile = true;
                        }

                        break;
                    case QuirkType.OneForAll8th:
                    if (quirkTimer % 250 == 0)

                        {
                            int projType = ModContent.ProjectileType<PrimeTexasSmashProj>();
                            
                            Vector2 velocity = directionToPlayer * 10f; 
                            int damage = npc.damage / 2; 

                            
                            int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, velocity, projType, damage, 0f, Main.myPlayer);
                            
                            
                            Main.projectile[p].friendly = false;
                            Main.projectile[p].hostile = true;
                        }

                        break;

                    case QuirkType.Overclock:
                    case QuirkType.Gearshift:
                       
                        if (npc.Distance(target.Center) < 2000f)
                        {
                            Vector2 extraSpeed = npc.velocity * 2.5f;

                        Vector2 safeMovement = Collision.TileCollision(npc.position, extraSpeed, npc.width, npc.height);

                        
                        npc.position += safeMovement;
                        }
                        break;
                    
                    case QuirkType.Explosion:
                        
                        if (quirkTimer % 150 == 0)
                        {
                            int projType = ModContent.ProjectileType<ApShotProj>();
                            int damage = npc.damage / 2; 

                            Vector2 velocity = directionToPlayer * 12f;
                            
                            int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, velocity, projType, damage, 0f, Main.myPlayer);
                            Main.projectile[p].friendly = false;
                            Main.projectile[p].hostile = true;
                        }
                        break;
                    case QuirkType.Erasure:
                        
                        if (quirkTimer % 350 == 0)
                        {
                            Vector2 velocity = directionToPlayer * 15f; // Rápido
                            int p = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, velocity, ModContent.ProjectileType<NPCErasureProj>(), 1, 0f, Main.myPlayer);
                            
                        }
                        break;
                }
            }
        }
    }
}
        
        // public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        // {
        //     if (HasQuirk)
        //     {
        //         
        //         if (AssignedQuirk == QuirkType.HellFlames)
        //         {
        //             target.AddBuff(BuffID.OnFire, 180); 
        //         }
        //         else if (AssignedQuirk == QuirkType.HalfColdHalfHot)
        //         {
        //             target.AddBuff(BuffID.Frostburn, 180); 
        //         }
        //     }
        // }
    
