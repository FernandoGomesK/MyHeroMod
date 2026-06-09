using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.DarkShadow;
using MyHeroMod.content.Quirks.DarkShadow.Projectiles;

namespace MyHeroMod.content.Quirks.DarkShadow.Skills
{
    public class PiercingTwilightClawsSkill : QuirkSkill
    {
        public override string Name => "Piercing Twilight Claws";
        public override string Description => "Lança as garras do Dark Shadow à distância.";
        public override string IconPath => "MyHeroMod/Assets/Skills/ClawAttack";
        
        public override int BaseCooldown => 15; 

        public override QuirkType RequiredQuirk => QuirkType.DarkShadow;
        public override QuirkStage RequiredStage => QuirkStage.Initial;

        public override void OnUse(Player player)
        {
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            // Descobre qual mão está livre
            int projectileToSpawn = -1;
            int handIndex = -1;
            
            if (!darkPlayer.isFrontHandAttacking) 
            {
                // Mão da frente livre! Usa o sprite/projétil da frente.
                projectileToSpawn = ModContent.ProjectileType<DarkShadowLongFrontHandProj>();
                handIndex = 0;
            }
            else if (!darkPlayer.isBackHandAttacking) 
            {
                // Mão da frente está ocupada. A de trás está livre? Usa o de trás!
                projectileToSpawn = ModContent.ProjectileType<DarkShadowLongBackHandProj>();
                handIndex = 1;
            }

            // Se as duas mãos estiverem a voar, não faz nada
            if (projectileToSpawn == -1)
            {
                return; 
            }

            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 18f; 

            // O jogo dispara automaticamente o sprite correto e com a camada correta!
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                velocity,
                projectileToSpawn, // Invoca o projétil escolhido pelo IF
                45, 
                4f, 
                player.whoAmI,
                handIndex, 
                0          
            );
        }
    }
}