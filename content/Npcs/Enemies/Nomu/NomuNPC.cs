using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace MyHeroMod.content.Npcs.Enemies.Nomu
{
    public class NomuNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            
            Main.npcFrameCount[Type] = 12; 
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
        }

        public override void PostAI()
        {
            
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

            int frameStanding = 0;
            int frameJumping = 3;

          
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
            
                NPC.frameCounter += Math.Abs(NPC.velocity.X); 

                if (NPC.frameCounter >= 6.0) 
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                    
                }

                if (NPC.frame.Y > 10 * frameHeight || NPC.frame.Y < 1 * frameHeight)
                {
                    NPC.frame.Y = 1 * frameHeight; 
                }
            }
        }
    }
}