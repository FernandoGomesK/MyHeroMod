using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;

using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles;



public class IgnitedArrowSkill: QuirkBaseSkill
{
    
    public override string Name => "Ignited Arrow";

    public override string GetDisplayName(Player player) => "Ignited Arrow";
   
    public override string Description => "Shoot a arrow of Fire";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Hellflame/HellArrowIcon";
    public override string Category => "Fire";

    public override int BaseCooldown => 900;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
            int baseDamage = 20;

            switch(transPlayer.CurrentStage)
            {
                case QuirkStage.Initial: baseDamage = 20; break;
                case QuirkStage.Adequation: baseDamage = 40; break;
                case QuirkStage.Intermediate: baseDamage = 45; break;
                case QuirkStage.Advanced: baseDamage = 60; break;
                case QuirkStage.Final: baseDamage = 80; break;
            }

            float modifiedDamage = 1f;

            
            if (hellPlayer.IsFlashFireFistActive)
            {
                modifiedDamage += 1.5f; 
            }
        
            if (hellPlayer.isSurgeArmGauntletsOn)
            {
                modifiedDamage += 0.5f; 
            }

            int finalDamage = (int)(baseDamage * modifiedDamage);



        if (transPlayer.HasActiveQuirk(QuirkType.HellFlames)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<IgnitedArrowProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
            
        }

        foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(15);
                }
            }
            
        }}