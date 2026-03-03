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


public class TexasSmashSkill : QuirkSkill
{
    public override string Name => "Texas Smash";
    public override string Description => "Propel air forward with a flick of your fingers";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 300;

    public override QuirkType RequiredQuirk => QuirkType.OneForAll8th;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        var ofaPlayer = player.GetModPlayer<TransformationPlayer>();


        if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeTexasSmashProj>()] > 0)
                return;

                if (ofaPlayer.CurrentStage >= QuirkStage.Adequation)
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

            // Spawna o projétil que vai controlar o player
            // A velocidade inicial não importa aqui, pois a AI[0] controla a subida
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity, 
                ModContent.ProjectileType<PrimeTexasSmashProj>(),
                10, // Dano alto (Impacto)
                30f, // Knockback alto
                player.whoAmI);

SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1"), player.position);
    }
}
