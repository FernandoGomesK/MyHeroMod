using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.ZeroGravity;

public class SelfFloatSkill : QuirkSkill
{
    public override string Name => "Self Float";
    public override string Description => "Float around";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 30;
    public override QuirkType RequiredQuirk => QuirkType.ZeroGravity;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        var zPlayer = player.GetModPlayer<ZeroGravityPlayer>();

        // 1. BLOQUEIO DE ENJOO: Não deixa usar se estiver passando mal
        if (player.HasBuff(BuffID.Confused) || zPlayer.Nausea >= zPlayer.NauseaMax)
        {
            Main.NewText("You feel too sick to use your Quirk...", Color.GreenYellow);
            return; // Bloqueia a skill
        }

        // 2. SISTEMA LIGA/DESLIGA (TOGGLE)
        if (player.HasBuff(ModContent.BuffType<ZeroGravityBuff>()))
        {
            // Se JÁ ESTIVER flutuando, a skill funciona como um "Release" pessoal
            player.ClearBuff(ModContent.BuffType<ZeroGravityBuff>());
            zPlayer.isZeroGravityActive = false;
        }
        else
        {
            // Se NÃO ESTIVER flutuando, ativa o Buff.
            // Colocamos um tempo gigante (1 hora = 216000 frames) porque o que vai 
            // desligar a skill naturalmente é a Náusea ou o jogador clicar de novo!
            player.AddBuff(ModContent.BuffType<ZeroGravityBuff>(), 216000); 
            
            // Um efeito sonoro de "Magia/Flutuar" para dar aquele toque de polimento
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item29, player.position);
        }
    }
}
