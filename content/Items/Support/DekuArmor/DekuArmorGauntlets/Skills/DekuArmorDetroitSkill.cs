using Microsoft.Xna.Framework;
using MyHeroMod.content.items.Support.DekuArmor.Projectiles;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Support.DekuArmor.DekuArmorGauntlets.Skills
{
    public class DekuArmorDetroitSmashSkill : QuirkBaseSkill
    {
        public override string Name => "Armor Detroit Smash";
        public override string Category => "Deku Armor";
        public override string Description => "Propel air forward with a massive punch";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/DetroitSmashIcon";

        public override int BaseCooldown => 120;
        public override QuirkType RequiredQuirk => QuirkType.Quirkless;
        public override bool isItemSkill => true;
        public override int RequiredItemId => ModContent.ItemType<DekuArmorGauntlets>();
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            int onomatopoeiaType = ModContent.ProjectileType<DekuDetroitOnomatopoeia>();
        
            
        
        Vector2 textPosition = player.Center + new Vector2(0, -30f);
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            textPosition,
            Vector2.Zero, 
            onomatopoeiaType, 
            0, 
            0f, 
            player.whoAmI
        );

        
        Projectile.NewProjectile(
            player.GetSource_FromThis(), 
            player.Center, 
            Vector2.Zero, 
            ModContent.ProjectileType<ChargearmorDetroitProj>(), 
            0, 
            0f, 
            player.whoAmI
        );
    }
        }
    }
