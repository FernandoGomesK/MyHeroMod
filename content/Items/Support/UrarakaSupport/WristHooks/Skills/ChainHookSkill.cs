using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.BlackWhip;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Items.Support.UrarakaSupport.Projectiles;

namespace MyHeroMod.content.Items.Support.UrarakaSupport.WristHooks.Skills
{
public class ChainHookSkill : QuirkBaseSkill
    {
        public override string Name => "Wrist Hook";
    public override string Description => "Shoot a hook from your wrist at you cursor and pull yourself towards it";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/ZeroGravity/WristHookIcon"; 
                    
                
    public override string Category => "Zero Gravity";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Quirkless;
    public override QuirkStage RequiredStage => QuirkStage.Initial;

    public override bool isItemSkill => true;
    public override int RequiredItemId => ModContent.ItemType<WristHooks>();

    
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {

        
        var whipLimit = 2;
        
        if (player.ownedProjectileCounts[ModContent.ProjectileType<WristHookProjectile>()] >= whipLimit) 
            {
            return; 
            }

            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 50f;
         
            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                velocity, 
                ModContent.ProjectileType<WristHookProjectile>(), 
                10,  
                0f, 
                player.whoAmI);

    }
}
}