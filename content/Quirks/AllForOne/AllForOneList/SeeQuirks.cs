using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;

using MyHeroMod.content.Projectiles;
using MyHeroMod;


namespace MyHeroMod.content.Quirks.AllForOne.AllForOneList // Mude para a pasta onde você salvar este arquivo
{
public class SeeQuirksSkill : QuirkSkill
{
    public override string Name => "See";
    public override string Description => "See all quirks in you mighty possession.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.AllForOne;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
        {
            // Se alguma interface já estiver aberta, a habilidade fecha ela
            if (UISystem.IsUiOpen())
            {
                UISystem.HideUI();
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
            // Se estiver fechado, ela abre o menu do AFO
            else
            {
                UISystem.ShowSeeQuirksUI();
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
}}}