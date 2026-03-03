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
using MyHeroMod.content.Quirks.OFA8th.Projectiles.CaliforniaSmash;


public class CaliforniaSmashSkill : QuirkSkill
{
    public override string Name => "California Smash";
    public override string Description => "Propel air forward with a flick of your fingers";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.OneForAll8th;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        var ofaPlayer = player.GetModPlayer<TransformationPlayer>();
        
         if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeCaliforniaSmashController>()] > 0)
                return;

                if (ofaPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                CombatText.NewText(player.getRect(), Color.Yellow, "California Smash!");
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.White, "Roll Punch");
            }

            // Spawna o projétil que vai controlar o player
            // A velocidade inicial não importa aqui, pois a AI[0] controla a subida
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<PrimeCaliforniaSmashController>(),
                80, // Dano alto (Impacto)
                10f, // Knockback alto
                player.whoAmI
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2"), player.position);
            
        }
    }
