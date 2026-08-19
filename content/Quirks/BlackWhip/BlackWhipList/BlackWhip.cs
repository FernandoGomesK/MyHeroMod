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
using MyHeroMod.content.Quirks.BlackWhip.Projectiles.BlackWhip;


public class BlackWhipHookSkill : QuirkBaseSkill
    {
         public override string Name => "Black Whip Hook";
    public override string Description => "Shoot a hook made from blackwhip at you cursor and pull yourself towards it";
    public override string IconPath
        {
            get
            {   
                Player player = Main.LocalPlayer;
                var transPlayer = player.GetModPlayer<TransformationPlayer>();

                if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
                {
                    return "MyHeroMod/Assets/SkillIcons/Blackwhip/OFABlackwhipIcon"; 
                    
                }
                else
                {
                    return "MyHeroMod/Assets/SkillIcons/Blackwhip/BlackwhipIcon"; 
                }
            }
        } 
    public override string Category => "BlackWhip";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.BlackWhip;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override QuirkStage RequiredOfaStage => QuirkStage.Intermediate;
    
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {

        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var whipLimit = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 1,
            QuirkStage.Adequation => 2,
            QuirkStage.Intermediate => 4,
            QuirkStage.Advanced => 7,
            QuirkStage.Final => 10,
            _ => 1
        };

        



        if (player.ownedProjectileCounts[ModContent.ProjectileType<BlackWhipProjectile>()] >= whipLimit) 
            {
            return; 
            }

            int finalDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 15,
                QuirkStage.Adequation => 30,
                QuirkStage.Intermediate => 60,
                QuirkStage.Advanced => 100,
                QuirkStage.Final => 150,
                _ => 15
            };

                Vector2 textPosition = player.Center + new Vector2(0, -30f);
                Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                textPosition,
                Vector2.Zero, 
                ModContent.ProjectileType<BlackwhipOnomatopoeia>(), 
                0,  
                0f, 
                player.whoAmI);



            
            Vector2 velocity = Main.MouseWorld - player.Center;
            velocity.Normalize();
            velocity *= 18f;

            

            
            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                velocity, 
                ModContent.ProjectileType<BlackWhipProjectile>(), 
                finalDamage,  
                0f, 
                player.whoAmI);

    }
    }