using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;

using MyHeroMod.content.Quirks.Overhaul.Projectiles.GroundDisassemble;



public class GroundDisassembleSkill: QuirkBaseSkill
{
    
    public override string Name => "Ground Disassemble";

   
    public override string Description => "Create a row of Huge Rock Spikes";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
    public override string Category => "Overhaul";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.Overhaul;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {
        
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        float multiplier = 1.0f;
        

        
            if (player.ownedProjectileCounts[ModContent.ProjectileType<RockWaveController>()] > 0)
                return;

        // SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/TodorokiIce"), player.position);

        int iceDamage = 80;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Initial: iceDamage = 80; break;
                case QuirkStage.Adequation: iceDamage = 150; break;
                case QuirkStage.Intermediate: iceDamage = 300; break;
                case QuirkStage.Advanced: iceDamage = 500; break;
                case QuirkStage.Final: iceDamage = 750; break;
            }
            int finalDamage = (int)(iceDamage * multiplier);


        // Define a direção (Esquerda ou Direita baseado no mouse)
        float direction = Main.MouseWorld.X > player.Center.X ? 1f : -1f;
    
        // Velocidade da onda (Rápida)s
        Vector2 velocity = new Vector2(10f * direction, 0f);

        // Spawna o Controlador um pouco na frente do player
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center + new Vector2(20f * direction, 0), // Começa um pouco a frente
            velocity,
            ModContent.ProjectileType<RockWaveController>(),
            finalDamage, // Dano
            5f,
            player.whoAmI

            
        );
        // hchhPlayer.temperature -= 45;

    }}
