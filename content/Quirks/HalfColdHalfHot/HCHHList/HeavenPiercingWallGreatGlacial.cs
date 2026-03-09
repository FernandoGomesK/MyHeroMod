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
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HeavenPiercingWall;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.GreatGlacialAegir;


public class HeavenPiercingGreatGlacial: QuirkSkill
{
    
    public override string Name => "Heaven Piercing Wall/Great Glacial Aegir";

   
    public override string Description => "Create a row of Huge ice spikes or dash and freeze everything in your path";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

        if (hchhPlayer.IsPhosphorActive)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<GreatGlacialAegirController>()] > 0)
                return;

            // Spawna o projétil que vai controlar o player
            // A velocidade inicial não importa aqui, pois a AI[0] controla a subida
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<GreatGlacialAegirController>(),
                80, // Dano alto (Impacto)
                10f, // Knockback alto
                player.whoAmI);
        }
        else
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<IceWaveController>()] > 0)
                return;

        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/TodorokiIce"), player.position);

        


        // Define a direção (Esquerda ou Direita baseado no mouse)
        float direction = Main.MouseWorld.X > player.Center.X ? 1f : -1f;
    
        // Velocidade da onda (Rápida)s
        Vector2 velocity = new Vector2(10f * direction, 0f);

        // Spawna o Controlador um pouco na frente do player
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center + new Vector2(20f * direction, 0), // Começa um pouco a frente
            velocity,
            ModContent.ProjectileType<IceWaveController>(),
            50, // Dano
            5f,
            player.whoAmI

            
        );
        hchhPlayer.temperature -= 45;
        }
        }
    }
