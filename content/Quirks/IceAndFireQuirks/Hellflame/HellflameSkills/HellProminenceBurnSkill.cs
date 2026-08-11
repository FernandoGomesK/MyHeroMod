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
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles;



public class HellProminenceBurnSkill: QuirkBaseSkill
{
    
    public override string Name => "Hell Prominence Burn ";

    public override string GetDisplayName(Player player) => "Prominence Burn ";
        
   
    public override string Description => "Shoot a fireball";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Fire";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;

    public override void OnUse(Player player)
    {
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        int BaseDamage = 0;
        
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                BaseDamage = 20;
                break;
            
                case QuirkStage.Adequation:
                BaseDamage = 40;
                break;
          
                case QuirkStage.Intermediate:
                BaseDamage =  45;
                break;
            
                case QuirkStage.Advanced:
                BaseDamage = 60;
                break;
          
                case QuirkStage.Final:
                BaseDamage = 80;
                break;
        
                default:
                BaseDamage =20;
                break;
                    
            }
        
        float ModifiedDamage = 1;

        if (hellPlayer.IsFlashFireFistActive){
         
        ModifiedDamage += 1.5f;        
        }
        int FinalDamage = (int)(BaseDamage * ModifiedDamage);



        if (transPlayer.HasActiveQuirk(QuirkType.HellFlames)){
            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<ChargeHellProminenceBurnProj>(),
                FinalDamage, 
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