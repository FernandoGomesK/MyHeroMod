using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.OpticBlast
{
    public partial class OpticBlastPlayer : ModPlayer, IQuirkResetter
    {
        
        
        
        public int Percentage = 0;
        public int OpticReserve = 100;
        

        public void FullReset()
        {
            isZeroGravityActive = false;
            Percentage = 0;
            OpticReserve = 100;
        }

        public override void ResetEffects()
        {
            
        }

        public override void PostUpdateMiscEffects()
        {
            // 1. Conta quantos inimigos estão a flutuar neste momento
            int floatingNpcCount = 0;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.GetGlobalNPC<Debuffs.ZeroGravityGlobalNPC>().hasZeroGravity)
                {
                    floatingNpcCount++;
                }
            }

            // Se o player estiver a flutuar OU tiver inimigos a flutuar
            if (isZeroGravityActive || floatingNpcCount > 0)
            {
                // 2. Aumenta a náusea baseada no peso (quantidade de alvos)
                int nauseaRate = 0;
                if (isZeroGravityActive) nauseaRate += 1; // 1 de peso por si mesmo
                nauseaRate += floatingNpcCount;           // 1 de peso extra por cada inimigo!

                Nausea += nauseaRate;

                // 3. Lógica Física do Jogador (apenas se ele ativou em si mesmo)
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

                // 4. LIMITE ATINGIDO (VÔMITO / PERDA DE CONTROLE)
                if (Nausea >= NauseaMax)
                {
                    // Auto-Release no Player
                    Player.ClearBuff(ModContent.BuffType<Buffs.ZeroGravityBuff>());
                    isZeroGravityActive = false;
                    
                    // Auto-Release nos Inimigos (A Quirk desativa à força)
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

                    // Punição Severa de Enjoo (Confuso e Lento)
                    Player.AddBuff(BuffID.Confused, 240); // 4 segundos
                    Player.AddBuff(BuffID.Slow, 240);
                    
                    // Efeito sonoro desagradável opcional (vômito/engasgo do Terraria)
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath13, Player.position);
                }
            }
            else
            {
                // 5. Recuperação de Fôlego
                if (Nausea > 0)
                {
                    Nausea -= 2; // Recupera o enjoo gradualmente
                    if (Nausea < 0) Nausea = 0;
                }
            }
        }
    }
}