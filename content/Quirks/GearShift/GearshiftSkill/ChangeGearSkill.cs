using Microsoft.Xna.Framework;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.Gearshift;
using MyHeroMod.content.Quirks.GearShift.Projectiles;
using MyHeroMod.content.System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;



public class ChangeGearSkill : QuirkBaseSkill
{
    public override string Name => "Change Gear";
    public override string Description => "Change the Gear of Whoever you touch";

    public override string IconPath
        {
            get
            {   
                Player player = Main.LocalPlayer;
                var transPlayer = player.GetModPlayer<TransformationPlayer>();

                if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
                {
                    return "MyHeroMod/Assets/SkillIcons/Gearshift/OFAGearshiftIcon"; 
                }
                else
                {
                    return "MyHeroMod/Assets/SkillIcons/Gearshift/GearshiftIcon"; 
                }
            }
        } 
    public override string Category => "Gearshift";
    public override int BaseCooldown => 600;
    public override QuirkType RequiredQuirk => QuirkType.Gearshift;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override QuirkStage RequiredOfaStage => QuirkStage.Final;
    public override bool IsDefaultSkill => false;
    

 

        public override void OnUse(Player player)
    {
        Vector2 Velocity = Main.MouseWorld - player.Center;
        Velocity.Normalize();
        Velocity *= 15f;

        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center,
            Velocity,
            ModContent.ProjectileType<ChangeGearProj>(),
            15,
            2f,
            player.whoAmI);
    }

}


