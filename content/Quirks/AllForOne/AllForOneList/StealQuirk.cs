using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.ID;


using MyHeroMod.content.Projectiles;



public class StealQuirkSkill : QuirkBaseSkill
{
    public override string Name => "Steal";
    public override string Description => "Steal the power of another quirk.";
    public override string IconPath => "Quirks/GearShift/Gearshift";
    public override string Category => "AllForOne";
    public override int BaseCooldown => 60;
    public override QuirkType RequiredQuirk => QuirkType.AllForOne;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;

    public override void OnUse(Player player)
    {
        Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<HandProj>(),
                15, 
                2f, 
                player.whoAmI);

    }
}