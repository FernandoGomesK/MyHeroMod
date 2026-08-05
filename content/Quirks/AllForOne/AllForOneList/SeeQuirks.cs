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


namespace MyHeroMod.content.Quirks.AllForOne.AllForOneList 
{
public class SeeQuirksSkill : QuirkBaseSkill
{
    public override string Name => "See";
    public override string Description => "See all quirks in you mighty possession.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override string Category => "AllForOne";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.AllForOne;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    
    public override void OnUse(Player player)
        {
            
            if (UISystem.IsUiOpen())
            {
                UISystem.HideUI();
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
            
            else
            {
                UISystem.ShowSeeQuirksUI();
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
}}}