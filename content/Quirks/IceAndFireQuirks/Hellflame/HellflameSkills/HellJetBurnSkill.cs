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
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles;



public class HellJetBurnSkill: QuirkBaseSkill
{
    
    public override string Name => "Hell Jet Burn";

    public override string GetDisplayName(Player player) => "Flashfire Fist: Jet Burn";
            
        
   
    public override string Description => "Shoot a constant stream of fire";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Fire";

    public override int BaseCooldown => 1200;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        int BaseDamage = 0;
        
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial: BaseDamage = 40; break;
                case QuirkStage.Adequation: BaseDamage = 60; break;
                case QuirkStage.Intermediate: BaseDamage =  85; break;   
                case QuirkStage.Advanced: BaseDamage = 110; break;
                case QuirkStage.Final: BaseDamage = 150; break;
                default: BaseDamage = 40; break;
                    
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

            int finalDamage = (int)(BaseDamage * modifiedDamage);



        if (transPlayer.HasActiveQuirk(QuirkType.HellFlames)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<ChargeHellJetBurnProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
            
        }

            
        }}