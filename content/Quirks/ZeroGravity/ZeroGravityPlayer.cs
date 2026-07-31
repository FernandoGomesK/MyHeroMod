using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.ZeroGravity
{
    public partial class ZeroGravityPlayer : ModPlayer, IQuirkResetter
    {
        public bool isZeroGravityActive = false;
        
        
        public int Nausea = 0;
        public int NauseaMax = 300; 


        public void FullReset()
        {
            isZeroGravityActive = false;
            Nausea = 0;
        }

        public override void ResetEffects()
        {
            isZeroGravityActive = false;
        }

        public override void PostUpdateEquips()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            NauseaMax = transPlayer.CurrentStage switch 
                {
                    QuirkStage.Initial => 300, QuirkStage.Adequation => 500,
                    QuirkStage.Intermediate => 700, QuirkStage.Advanced => 900,
                    QuirkStage.Final => 1200, _ => 20
                };
            
        }

        public override void PostUpdateMiscEffects()
        {
        
            int floatingNpcCount = 0;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.GetGlobalNPC<Debuffs.ZeroGravityGlobalNPC>().hasZeroGravity)
                {
                    floatingNpcCount++;
                }
            }

        
            if (isZeroGravityActive || floatingNpcCount > 0)
            {
                
                int nauseaRate = 0;
                if (isZeroGravityActive) nauseaRate += 1; 
                nauseaRate += floatingNpcCount;           

                Nausea += nauseaRate;

                
                if (isZeroGravityActive && !Player.mount.Active && Player.velocity.Y != 0)
                {
                    if (Player.controlJump) 
                    {
                        Player.velocity.Y = -0.5f; 
                        Player.fallStart = (int)(Player.position.Y / 16f); 
                    }
                    else if (Player.velocity.Y > 0)
                    {
                        Player.velocity.Y *= 0.25f; 
                        Player.fallStart = (int)(Player.position.Y / 16f);
                    }

                    if (Main.rand.NextBool(4))
                        Dust.NewDust(Player.position, Player.width, Player.height, DustID.PinkFairy);
                }

            
                if (Nausea >= NauseaMax)
                {
                    
                    Player.ClearBuff(ModContent.BuffType<Buffs.ZeroGravityBuff>());
                    isZeroGravityActive = false;
                    
                
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.HasBuff(ModContent.BuffType<ZeroGravityBuff>()))
                        {
                            int buffIndex = npc.FindBuffIndex(ModContent.BuffType<ZeroGravityBuff>());
                            if (buffIndex != -1) npc.DelBuff(buffIndex);
                            npc.GetGlobalNPC<ZeroGravityGlobalNPC>().hasZeroGravity = false;
                        }
                    }

                
                    Player.AddBuff(BuffID.Confused, 240); 
                    Player.AddBuff(BuffID.Slow, 240);
                    
                
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath13, Player.position);
                }
            }
            else
            {
            
                if (Nausea > 0)
                {
                    Nausea -= 2; 
                    if (Nausea < 0) Nausea = 0;
                }
            }
        }
    }
}