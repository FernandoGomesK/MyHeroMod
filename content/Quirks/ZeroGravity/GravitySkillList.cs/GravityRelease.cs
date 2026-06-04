using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.ZeroGravity;
using MyHeroMod.content.Debuffs; // Puxa o seu ModBuff dos inimigos
using MyHeroMod.content.Buffs;
using MyHeroMod.content;   // Puxa o seu ModBuff do player

public class GravityReleaseSkill : QuirkSkill
{
    public override string Name => "Release";
    public override string Description => "Negate the gravitational pull of objects at a distance";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.ZeroGravity;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override void OnUse(Player player)
    {
        var zPlayer = player.GetModPlayer<ZeroGravityPlayer>();

        // 1. Limpa a gravidade do PRÓPRIO jogador
        player.ClearBuff(ModContent.BuffType<ZeroGravityBuff>());
        zPlayer.isZeroGravityActive = false;

        // 2. Percorre todos os inimigos do mapa e os derruba!
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            
            // Verifica se o NPC está vivo e tem o Buff de gravidade nela
            if (npc.active && npc.HasBuff(ModContent.BuffType<ZeroGravityBuff>()))
            {
                int buffIndex = npc.FindBuffIndex(ModContent.BuffType<ZeroGravityBuff>());
                if (buffIndex != -1)
                {
                    npc.DelBuff(buffIndex); // Remove o buff
                }
                
                // Força a variável de segurança a desligar imediatamente
                npc.GetGlobalNPC<ZeroGravityGlobalNPC>().hasZeroGravity = false;
            }
        }

        // 3. Efeito visual e sonoro da liberação ("KAIJO!")
        Terraria.Audio.SoundEngine.PlaySound(SoundID.MaxMana, player.position);
        for (int i = 0; i < 15; i++)
        {
            Dust.NewDust(player.position, player.width, player.height, DustID.PinkFairy, 0f, -2f, 150, default, 1.5f);
        }
    }
}