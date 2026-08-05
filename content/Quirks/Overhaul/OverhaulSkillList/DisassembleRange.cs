using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Overhaul.Projectiles.DisassembleRange;
using MyHeroMod.content.Quirks.Overhaul;

public class DisassembleRangeSkill : QuirkBaseSkill
{
    public override string Name => "Overhaul Hand";
    public override string Description => "Disassemble objects at a distance";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override string Category => "Overhaul";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.Overhaul;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var overhaulPlayer = player.GetModPlayer<OverhaulPlayer>();

        
        int usedProj = overhaulPlayer.isChimeraActive 
            ? ModContent.ProjectileType<RangedDisassembleChimeraProj>() 
            : ModContent.ProjectileType<RangedDisassembleProj>();

        
        Vector2 velocity = Main.MouseWorld - player.Center;
        velocity.Normalize();
        velocity *= 15f;

    
        int baseDamage = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 15,
            QuirkStage.Adequation => 35,
            QuirkStage.Intermediate => 60,
            QuirkStage.Advanced => 120,
            QuirkStage.Final => 250,
            _ => 15
        };

        // 4. DISPARO
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center,
            velocity,
            usedProj,
            baseDamage, 
            2f, 
            player.whoAmI
        );
    }
}