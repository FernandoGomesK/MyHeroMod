using Terraria;
using Terraria.ModLoader;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Decay.Projectiles.GroundTouch;

public class GroundTouchSkill : QuirkSkill
{
    public override string Name => "Decay Ground";
    
        

    
    public override string Description => "Touch the ground with decay energy";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";

    public override int BaseCooldown => 200;
    public override QuirkType RequiredQuirk => QuirkType.Decay;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        float direction = Main.MouseWorld.X > player.Center.X ? 1f : -1f;
    
        // Velocidade da onda (Rápida)s
        Vector2 velocity = new Vector2(10f * direction, 0f);

        // Spawna o Controlador um pouco na frente do player
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center + new Vector2(20f * direction, 0), // Começa um pouco a frente
            velocity,
            ModContent.ProjectileType<GroundTouchProj>(),
            10, // Dano
            5f,
            player.whoAmI

            
        );
    }
}