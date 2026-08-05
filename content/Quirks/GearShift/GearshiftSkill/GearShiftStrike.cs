using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.GearShift.Projectiles;
using Terraria.Audio;

public class GearShiftStrikeSkill : QuirkBaseSkill
{
    public override string Name => "Gearshift Strike";
    
        

    
    public override string Description => "Dash Forward reaching for your foes";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Gearshift";

    public override int BaseCooldown => 300;
    public override QuirkType RequiredQuirk => QuirkType.Gearshift;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override QuirkStage RequiredOfaStage => QuirkStage.Final;
    public override bool IsDefaultSkill => false;
    


    public override void OnUse(Player player)
    {
        if (player.HasBuff<GearshiftBuff>())
        {
         Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<GearshiftStrikeProj>(),
                200, 
                10f, 
                player.whoAmI
                
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, player.position);   
        }
        else
        {
            return;
        }
        
    }
}