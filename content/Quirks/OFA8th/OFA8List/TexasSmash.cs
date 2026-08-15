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
using MyHeroMod.content.Quirks.OFA8th.Projectiles.TexasSmash;
using MyHeroMod.content.Quirks.OFA8th;


public class TexasSmashSkill : QuirkBaseSkill
{
    public override string Name => "Texas Smash";
    public override string Description => "Propel air forward with a flick of your fingers";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA8th/DetroitSmashIcon"; 
    public override string Category => "OneForAll8th";

    public override int BaseCooldown => 300;

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


        if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeTexasSmashProj>()] > 0)
                return;

                if (transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                CombatText.NewText(player.getRect(), Color.Yellow, "Texas Smash!");
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.White, "Air Pressure!");
            }

            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 30f;

        
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity, 
                ModContent.ProjectileType<PrimeTexasSmashProj>(),
                finalDamage, 
                45f, 
                player.whoAmI);

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
    }
}
