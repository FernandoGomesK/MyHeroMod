using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.Blueflames;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.IceShot;
using MyHeroMod.content.System.Interfaces;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles.HellSpider;
using MyHeroMod.content.Quirks.Overhaul.Projectiles.GroundDisassemble;
using MyHeroMod.content.Quirks.Overhaul.Projectiles.RockShoot;



public class RockShootSkill: QuirkBaseSkill
{
    
    public override string Name => "Rock Shoot";
    

   
    public override string Description => "Shoot a rock at your enemies";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Overhaul";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.Overhaul;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    

public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

        Vector2 direction = Main.MouseWorld - player.Center;
        direction.Normalize();

        float multiplier = 1.0f;
        
        
        
                int iceDamage = transPlayer.CurrentStage switch {
                    QuirkStage.Initial => 25, QuirkStage.Adequation => 55, QuirkStage.Intermediate => 90,
                    QuirkStage.Advanced => 180, QuirkStage.Final => 380, _ => 25
                };
                
                Vector2 velocity = direction * 15f;
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, velocity, ModContent.ProjectileType<RockShootProj>(), (int)(iceDamage * multiplier), 2f, player.whoAmI);

                
                return; 
            }
        }
    