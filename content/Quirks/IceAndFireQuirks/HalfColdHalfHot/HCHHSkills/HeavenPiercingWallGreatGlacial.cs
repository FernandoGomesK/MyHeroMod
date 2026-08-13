using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.GreatGlacialAegir;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.HeavenPiercingWall;
using MyHeroMod.content.System.Interfaces;



public class HeavenPiercingGreatGlacial: QuirkBaseSkill
{
    
    public override string Name => "Heaven Piercing Wall/Great Glacial Aegir";

   
    public override string Description => "Create a row of Huge ice spikes or dash and freeze everything in your path";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "HCHH";

    public override int BaseCooldown => 1800;

    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    


    public override void OnUse(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        float multiplier = 1.0f;
        if (hchhPlayer.isSurgeArmGauntletsOn) multiplier += 0.5f;

        if (hchhPlayer.IsPhosphorActive)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<GreatGlacialAegirController>()] > 0)
                return;

            int iceDamage = 1000;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Final: iceDamage = 1500; break;
            }
            int finalDamage = (int)(iceDamage * multiplier);

            
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<GreatGlacialAegirController>(),
                finalDamage,
                10f, 
                player.whoAmI);

                foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.ReduceHeat(100);
                }
            }
        }
        else
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<IceWaveController>()] > 0)
                return;

        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/TodorokiIce"), player.position);

        int iceDamage = 80;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Initial: iceDamage = 60; break;
                case QuirkStage.Adequation: iceDamage = 90; break;
                case QuirkStage.Intermediate: iceDamage = 120; break;
                case QuirkStage.Advanced: iceDamage = 160; break;
                case QuirkStage.Final: iceDamage = 220; break;
            }
            int finalDamage = (int)(iceDamage * multiplier);


        
        float direction = Main.MouseWorld.X > player.Center.X ? 1f : -1f;
    
        
        Vector2 velocity = new Vector2(10f * direction, 0f);

      
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center + new Vector2(20f * direction, 0),
            velocity,
            ModContent.ProjectileType<IceWaveController>(),
            finalDamage, 
            5f,
            player.whoAmI

            
        );
        

        foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.ReduceHeat(50);
                }
            }
        }
        }
    }
