using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Explosion;
using MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot;


using MyHeroMod.content.Quirks.SlideAndGlide.Projectiles.ScrappyThrust;
using MyHeroMod.content.Quirks.SlideAndGlide.Projectiles.ShootyGo;
using MyHeroMod.content.Quirks.OpticBlast.Projectiles;

public class SingleOpticBlastSkill : QuirkBaseSkill
{
     public override string Name => "Optic Blast";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "OpticBlast";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.OpticBlast;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    


                    public override void OnUse(Player player)
            {

                var transPlayer = player.GetModPlayer<TransformationPlayer>();

            


        float damageMultiplier = 1.0f;
        int MaxDamage = 45;
         

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                MaxDamage = 45;
                break;
            
                case QuirkStage.Adequation:
                MaxDamage = 45;
                break;
          
                case QuirkStage.Intermediate:
                MaxDamage = 60;
                break;
            
                case QuirkStage.Advanced:
                MaxDamage = 90;
                break;
          
                case QuirkStage.Final:
                MaxDamage = 180;
                break;
        
                default:
                MaxDamage =45;
                break;
                    
            }


            var finalDamage = (int)(damageMultiplier * MaxDamage);

            var text = "Optic Blast!";
            

            CombatText.NewText(player.getRect(), Color.Blue, text);

            

            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            
                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    player.Center,
                    Velocity,
                    ModContent.ProjectileType<OpticBlastProj>(),
                    finalDamage, 
                    4f,  
                    player.whoAmI
                );

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/SingleOpticBlast"), player.position);
           
            }

            
        }
        