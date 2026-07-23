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

public class ReciproExtendSkill : QuirkBaseSkill
{
    public override string Name => "Recipro Extend";
    public override string Description => "Jump and do a diving Kick at your Cursor";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash"; 
    public override string Category => "Engine";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.Engine;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var enginePlayer = player.GetModPlayer<EnginePlayer>();

        
        int MaxDamage = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 130,
            QuirkStage.Adequation => 350,
            QuirkStage.Intermediate => 500,
            QuirkStage.Advanced => 950,
            QuirkStage.Final => 2200,
            _ => 130
        };

    

        float DamageMultiplier = 1f;
        string attackName = "Recipro Extend!";
        int extraDamage = enginePlayer.currentGear switch
        {
            1 => 20,
            2 => 50,
            3 => 100,
            4 => 200,
            5 => 400,
            _ => 0
        };

        

        
        /*
        var enginePlayer = player.GetModPlayer<EnginePlayer>();
        if (enginePlayer.isIronSolesOn)
        {
            extraDamage = 50;
        }
        */

        
        if (player.HasBuff(ModContent.BuffType<ReciproBuff>()))
        {
            DamageMultiplier = 1.5f;   
            attackName = "Recipro Extend: BURST!"; 
        }

        int FinalDamage = (int)(MaxDamage * DamageMultiplier) + extraDamage;

        
        CombatText.NewText(player.getRect(), Color.Cyan, attackName);

        
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center,
            Vector2.Zero, 
            ModContent.ProjectileType<ReciproExtendController>(),
            FinalDamage, 
            10f, 
            player.whoAmI
        );

        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);   
    }
}