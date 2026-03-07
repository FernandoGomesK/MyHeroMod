using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.IceShot;
using MyHeroMod.content.Projectiles.HellSpider;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.JetKindling;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.IceThrower;


public class iceThrowerJetKindling : QuirkSkill
{
    
    public override string Name => "Ice Thrower/Jet Kindling";

   
    public override string Description => "Fire a wave of ice or fire at the direction of your looking.";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

        if (hchhPlayer.IsFlashFireFistActive)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0)
                return;

            // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
            // Note que a velocidade aqui define apenas a direção inicial da mira.
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                direction,
                ModContent.ProjectileType<JetKindlingController>(),
                0, // O controlador não dá dano direto
                0f,
                player.whoAmI
            
            );
            hchhPlayer.temperature += 25;
        }
        else
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<IceThrowerController>()] > 0)
                return;

            
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                direction,
                ModContent.ProjectileType<IceThrowerController>(),
                0, // O controlador não dá dano direto
                0f,
                player.whoAmI
            
            );
            hchhPlayer.temperature -= 25; 
        }
        
        }
        }