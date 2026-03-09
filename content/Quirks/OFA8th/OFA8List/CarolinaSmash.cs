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


public class CarolinaSmashSkill : QuirkSkill
{
    public override string Name => "Carolina Smash";
    public override string Description => "Propel air forward with a flick of your fingers";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 200;

    public override QuirkType RequiredQuirk => QuirkType.OneForAll8th;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {

        var ofaPlayer = player.GetModPlayer<TransformationPlayer>();

        if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeCarolinaSmashController>()] > 0)
                return;

                if (ofaPlayer.CurrentStage >= QuirkStage.Adequation)
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
                80, 
                10f, 
                player.whoAmI
                
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.8f }, player.position);
            
    }
}
