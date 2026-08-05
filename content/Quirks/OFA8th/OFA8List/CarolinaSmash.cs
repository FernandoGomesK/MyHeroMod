using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.OFA9th;
using Terraria.DataStructures;
using MyHeroMod.content.Quirks.OFA8th.Projectiles.CarolinaSmash;
using MyHeroMod.content.Quirks.OFA8th;


public class CarolinaSmashSkill : QuirkBaseSkill
{
    public override string Name => "Carolina Smash";
    public override string Description => "Propel air forward with a flick of your fingers";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "OneForAll8th";

    public override int BaseCooldown => 200;

    public override QuirkType RequiredQuirk => QuirkType.OneForAll8th;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    


    public override void OnUse(Player player)
    {

         var ofa8Player = player.GetModPlayer<OneForAll8thPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();


        float damageMultiplier = 1.0f;
        int MaxDamage = 50;
         

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                MaxDamage = 50;
                break;
            
                case QuirkStage.Adequation:
                MaxDamage = 50;
                break;
          
                case QuirkStage.Intermediate:
                MaxDamage = 110;
                break;
            
                case QuirkStage.Advanced:
                MaxDamage = 250;
                break;
          
                case QuirkStage.Final:
                MaxDamage = 700;
                break;
        
                default:
                MaxDamage =50;
                break;
                    
            }

            if (player.HasBuff(ModContent.BuffType<StockPileBuff>()) || ofa8Player.form == 1) {
                damageMultiplier = 1.5f; 
            }
            else if (player.HasBuff(ModContent.BuffType<StockPileBuff>() ) || ofa8Player.form == 2)  {
                damageMultiplier = 2.5f;
            }

            var finalDamage = (int)(damageMultiplier * MaxDamage);

        if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeCarolinaSmashController>()] > 0)
                return;

                if (transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                CombatText.NewText(player.getRect(), Color.Yellow, "Carolina Smash!");
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.White, "Dash Slash!");
            }

            
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<PrimeCarolinaSmashController>(),
                finalDamage, 
                10f, 
                player.whoAmI
                
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.8f }, player.position);
            
    }
}
