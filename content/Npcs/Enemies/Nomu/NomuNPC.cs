using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using MyHeroMod.content.Items.QuirkItems;

namespace MyHeroMod.content.Npcs.Enemies.Nomu
{
    public class NomuNPC : ModNPC
    {
        public bool isBarraging = false;
        
        public int barrageCooldown = 180; 
        
        public int attackPhase = 0; 
        public int phaseTimer = 0; 

       
        public int randomSfxTimer = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 23; 
        }

        public override void SetDefaults()
        {
            NPC.width = 62; 
            NPC.height = 58; 
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.value = Item.buyPrice(silver: 5);
            NPC.knockBackResist = 0.5f;

            NPC.aiStyle = NPCAIStyleID.Fighter; 
            AIType = NPCID.FaceMonster;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            barrageCooldown = Main.rand.Next(180, 360);
            
            
            randomSfxTimer = Main.rand.Next(180, 480);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NomuFragment>(), 10, 1, 1));
        }

        public override bool PreAI()
        {
            
            randomSfxTimer--;
            if (randomSfxTimer <= 0)
            {
               
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/NomuSFX") with { Volume = 0.3f}, NPC.Center);
                
                
                randomSfxTimer = Main.rand.Next(720, 1000);
            }

         
            if (attackPhase == 1) 
            {
                NPC.velocity.X *= 0.60f; 
                
                phaseTimer--;
                if (phaseTimer <= 0)
                {
                    NPC.velocity.X = NPC.direction * 15f; 
                    
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/whoosh"), NPC.Center);
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/WooshSound"), NPC.Center);
                    
                    attackPhase = 2;
                    phaseTimer = 360; 
                    isBarraging = true; 
                    
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/PunchBarrageSFX") with { Volume = 0.6f}, NPC.Center);
                }
                
                return false; 
            }
            else if (attackPhase == 2)
            {
                phaseTimer--;
                if (phaseTimer <= 0)
                {
                    attackPhase = 0;
                    isBarraging = false;
                    barrageCooldown = Main.rand.Next(180, 360); 
                }
                
                return true; 
            }
            else 
            {
                barrageCooldown--;
                
                if (barrageCooldown <= 0 && NPC.velocity.Y == 0)
                {
                    attackPhase = 1;
                    phaseTimer = 60;
                    return false; 
                }
            }

            return true; 
        }

        public override void PostAI()
        {
            if (attackPhase == 1) return; 

            float maxSpeed = 20f; 
            float acceleration = 0.8f; 

            if (NPC.velocity.Y == 0)
            {
                if (NPC.direction == 1 && NPC.velocity.X < maxSpeed)
                {
                    NPC.velocity.X += acceleration;
                }
                else if (NPC.direction == -1 && NPC.velocity.X > -maxSpeed)
                {
                    NPC.velocity.X -= acceleration;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            int frameOffset = isBarraging ? 11 : 0; 
            
            int frameStanding = 0 + frameOffset;
            int frameJumping = 3; 
            
            int walkStart = 1 + frameOffset;
            int walkEnd = 11 + frameOffset; 

            if (NPC.velocity.Y != 0f)
            {
                NPC.frame.Y = frameJumping * frameHeight;
                NPC.frameCounter = 0; 
            }
            else if (Math.Abs(NPC.velocity.X) < 0.1f)
            {
                NPC.frame.Y = frameStanding * frameHeight;
                NPC.frameCounter = 0;
            }
            else
            {
                int currentFrameIndex = NPC.frame.Y / frameHeight;
                if (currentFrameIndex < walkStart || currentFrameIndex > walkEnd)
                {
                    NPC.frame.Y = walkStart * frameHeight;
                }

                NPC.frameCounter += Math.Abs(NPC.velocity.X); 

                if (NPC.frameCounter >= 6.0) 
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                }

                
                int currentFrame = NPC.frame.Y / frameHeight;
                if (currentFrame == 4 + frameOffset || currentFrame == 9 + frameOffset)
                {
                    
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/NomuFootstepSFX") with { Volume = 0.2f}, NPC.Center);
                }

                currentFrameIndex = NPC.frame.Y / frameHeight;
                if (currentFrameIndex > walkEnd || currentFrameIndex < walkStart)
                {
                    NPC.frame.Y = walkStart * frameHeight; 
                }
            }
        }
    }
}