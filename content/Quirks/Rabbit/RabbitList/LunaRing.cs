using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Engine.Projectiles;
using MyHeroMod.content.Quirks.Engine;
using MyHeroMod.content.Quirks.Rabbit;
using MyHeroMod.content.Quirks.Rabbit.Projectiles;

public class LunaRingSkill : QuirkBaseSkill
{
    public override string Name => "Luna Ring";
    public override string Description => "A Circular Kick";
    public override string IconPath => "MyHeroMod/Assets/Skills/LunaRing"; 
    public override string Category => "Rabbit";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.Rabbit;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var rabbitPlayer = player.GetModPlayer<RabbitPlayer>();

        
        int MaxDamage = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 130,
            QuirkStage.Adequation => 350,
            QuirkStage.Intermediate => 500,
            QuirkStage.Advanced => 950,
            QuirkStage.Final => 2200,
            _ => 130
        };

    

        

        var extraDamage = 0;
        if (rabbitPlayer.isIronSolesOn)
        {
            extraDamage = 50;
        }
        

        
        var DamageMultiplier = 1f;

        var attackName = "Luna Ring!";
        

        
        

        int FinalDamage = (int)(MaxDamage * DamageMultiplier) + extraDamage;

        
        CombatText.NewText(player.getRect(), Color.Cyan, attackName);

        
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center,
            Vector2.Zero, 
            ModContent.ProjectileType<LunaRingController>(),
            FinalDamage, 
            10f, 
            player.whoAmI
        );

        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);   
    }
}