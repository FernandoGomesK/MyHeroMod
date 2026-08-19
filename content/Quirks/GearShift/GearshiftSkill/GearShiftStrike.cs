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
    public override string IconPath
        {
            get
            {   
                Player player = Main.LocalPlayer;
                var transPlayer = player.GetModPlayer<TransformationPlayer>();

                if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
                {
                    return "MyHeroMod/Assets/SkillIcons/Gearshift/OFAGearStrikeIcon"; 
                }
                else
                {
                    return "MyHeroMod/Assets/SkillIcons/Gearshift/GearStrikeIcon"; 
                }
            }
        } 
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
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

             int finalDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 100,
                QuirkStage.Adequation => 300,
                QuirkStage.Intermediate => 450,
                QuirkStage.Advanced => 600,
                QuirkStage.Final => 900,
                _ => 15
            };

         Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<GearshiftStrikeProj>(),
                finalDamage, 
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