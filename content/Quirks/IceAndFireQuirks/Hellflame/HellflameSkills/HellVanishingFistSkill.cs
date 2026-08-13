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
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles.BlueVanishingFist;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles;
using MyHeroMod.content.Projectiles;




public class HellVanishingFistSkill: QuirkBaseSkill
{
    
    public override string Name => "Hell Vanishing Fist";

    public override string GetDisplayName(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        if (transPlayer.CurrentStage >= QuirkStage.Advanced)
        {
            return "Vanishing Jet Burn";
        }
        else
        {
            return "Vanishing Fist";
        }
    }
    
        
   
    public override string Description => "Shoot a fireball";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Fire";

    public override int BaseCooldown => 720;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        int BaseDamage = 0;
        
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial: BaseDamage = 25; break;
                case QuirkStage.Adequation: BaseDamage = 45; break;
                case QuirkStage.Intermediate: BaseDamage =  55; break;
                case QuirkStage.Advanced: BaseDamage = 75; break;
                case QuirkStage.Final: BaseDamage = 100; break;       
                default: BaseDamage =20; break;
                    
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
                ModContent.ProjectileType<HellVanishingFistProj>(),
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