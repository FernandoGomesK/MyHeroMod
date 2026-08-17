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
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueFireball;



public class BlueFireballSkill: QuirkBaseSkill
{
    
    public override string Name => "Blue Fireball";

    public override string GetDisplayName(Player player)
        {
            
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
   
            if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                return "Blue Fireball";
            }
            return "Fireball"; 
        }
   
    public override string Description => "Shoot a fireball";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Blueflame/BlueFireballIcon";
    public override string Category => "Fire";

    public override int BaseCooldown => 900;

    public override QuirkType RequiredQuirk => QuirkType.Blueflame;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var bluePlayer = player.GetModPlayer<BlueflamePlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        int BaseDamage = 0;
        
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                BaseDamage = 30;
                break;
            
                case QuirkStage.Adequation:
                BaseDamage = 60;
                break;
          
                case QuirkStage.Intermediate:
                BaseDamage =  75;
                break;
            
                case QuirkStage.Advanced:
                BaseDamage = 90;
                break;
          
                case QuirkStage.Final:
                BaseDamage = 150;
                break;
        
                default:
                BaseDamage =20;
                break;
                    
            }
        
            float modifiedDamage = 1f;

            
            if (bluePlayer.IsFlashFireFistActive)
            {
                modifiedDamage += 2.0f; 
            }
        
            if (bluePlayer.isSurgeArmGauntletsOn)
            {
                modifiedDamage += 1.5f; 
            }

            int finalDamage = (int)(BaseDamage * modifiedDamage);



        if (transPlayer.HasActiveQuirk(QuirkType.Blueflame)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<BlueFireBallProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/CremationSound") { Volume = 0.5f, PitchVariance = 1.0f }, player.Center);
        }

        foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(20);
                }
            }
            
        }}