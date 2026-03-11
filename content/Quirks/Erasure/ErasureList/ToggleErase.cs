using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Erasure.Projectiles;
using MyHeroMod.content.Quirks.FaJin;
using MyHeroMod.content.Buffs; // Garanta que a pasta do Controller esteja correta

namespace MyHeroMod.content.Quirks.Erasure.ErasureList
{
    public class ToggleEraseSkill : QuirkSkill
    {
        public override string Name => "Erasure";
        public override string Description => "Look at enemies to erase their quirks. Blinking cancels the effect.";
        public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense"; // Mude depois para um ícone de Olho Vermelho

        public override int BaseCooldown => 30;

        public override QuirkType RequiredQuirk => QuirkType.Erasure;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;
        public override bool IsBaseQuirk => false;

        public override void OnUse(Player player)
        {
            var erasurePlayer = player.GetModPlayer<ErasurePlayer>();

            if (player.HasBuff(ModContent.BuffType<ErasingBuff>()))
            {
                player.ClearBuff(ModContent.BuffType<ErasingBuff>());
                SoundEngine.PlaySound(SoundID.MenuClose, player.position);
                CombatText.NewText(player.getRect(), Color.DarkGray, "Erasure: OFF");
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item121, player.position); 
                CombatText.NewText(player.getRect(), Color.Red, "Erasure: ON!");
            }

        
                
                
                
                
                

                
        }}
}