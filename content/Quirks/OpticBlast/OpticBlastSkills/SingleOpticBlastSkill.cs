using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Debuffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OpticBlast.Projectiles;
using MyHeroMod.content.Quirks.OpticBlast;
using System.Diagnostics.Metrics;

namespace MyHeroMod.content.Quirks.OpticBlast.Skills 
{
    public class SingleOpticBlastSkill : QuirkBaseSkill
    {
        public override string Name => "Optic Blast";
        public override string Description => "Shoot a concentrated penetrating Projectile";
        public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";
        public override string Category => "OpticBlast";

        public override int BaseCooldown => 20;
        

        public override QuirkType RequiredQuirk => QuirkType.OpticBlast;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;

       
        
            

        

        public override void OnUse(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();

           int meterReduction = opticPlayer.CurrentPercentage switch
            {
                OpticBlastPlayer.Percentage.TwentyFive => 20,
                OpticBlastPlayer.Percentage.Fifty => 30,
                OpticBlastPlayer.Percentage.SeventyFive => 40,
                OpticBlastPlayer.Percentage.Full => 50,
                _ => 0
            };

            
            if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.Zero || 
                player.HasBuff(BuffID.Darkness) || 
                player.HasBuff(ModContent.BuffType<Heatstroke>()) ||
                transPlayer.currentStrain >= transPlayer.maxStrain ||
                opticPlayer.CurrentOpticBlast < meterReduction) 
            {
                return; 
            }

            float damageMultiplier = opticPlayer.CurrentPercentage switch
            {
                OpticBlastPlayer.Percentage.TwentyFive => 0.5f,
                OpticBlastPlayer.Percentage.Fifty => 1.0f,
                OpticBlastPlayer.Percentage.SeventyFive => 1.5f,
                OpticBlastPlayer.Percentage.Full => 2.5f,
                _ => 1.0f
            };

            
            opticPlayer.CurrentOpticBlast -= meterReduction;
            if (opticPlayer.CurrentOpticBlast < 0)
                opticPlayer.CurrentOpticBlast = 0;

        
            
            int maxDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 45,
                QuirkStage.Adequation => 45,
                QuirkStage.Intermediate => 60,
                QuirkStage.Advanced => 90,
                QuirkStage.Final => 180,
                _ => 45
            };

            int finalDamage = (int)(damageMultiplier * maxDamage);

            CombatText.NewText(player.getRect(), Color.Blue, "Optic Blast!");

            Vector2 velocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 35f; 

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                velocity,
                ModContent.ProjectileType<OpticBlastProj>(),
                finalDamage, 
                4f,  
                player.whoAmI
            );

            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/SingleOpticBlast"), player.position);
        }
    }
}