using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Explosion;
using Terraria.DataStructures;

using MyHeroMod.content.Quirks.Explosion.Projectiles.FullPower;


public class FullPowerBlastSkill : QuirkSkill
{
    public override string Name => "Full Power Blast";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


            public override void OnUse(Player player)
    {

        var explodePlayer = player.GetModPlayer<ExplosionPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

         float damageMultiplier = 1.0f;
        int MaxDamage = 45;
         

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                MaxDamage = 25;
                break;
            
                case QuirkStage.Adequation:
                MaxDamage = 55;
                break;
          
                case QuirkStage.Intermediate:
                MaxDamage = 90;
                break;
            
                case QuirkStage.Advanced:
                MaxDamage = 160;
                break;
          
                case QuirkStage.Final:
                MaxDamage = 320;
                break;
        
                default:
                MaxDamage =45;
                break;
                    
            }

        

        
        if (player.HasBuff(ModContent.BuffType<ClusterBuff>()))
        {
            damageMultiplier += 2.5f; 
        }

        
        if (explodePlayer.IsGrenadierBracersOn && explodePlayer.CurrentSweat >= 30)
        {
            explodePlayer.CurrentSweat -= 30; 
            damageMultiplier += 1.0f; 
        }
        else
        {
           
            ApplyRecoil(player); 
        }
        var finalDamage = (int)(damageMultiplier* MaxDamage);

         
           
        
        

        CombatText.NewText(player.getRect(), Color.Orange, "DIE!");
            



         Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<FullPowerProj>(),
                finalDamage, 
                2f, 
                player.whoAmI
            );
             }

             private void ApplyRecoil(Player player)
    {
        int recoilDamage = (int)(player.statLifeMax2 * 0.05f); 
        player.statLife -= recoilDamage;
        
        CombatText.NewText(player.getRect(), Color.Red, "-" + recoilDamage); 

        if (player.statLife <= 0)
        {
            var reason = PlayerDeathReason.ByCustomReason(
                Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", player.name));
            player.KillMe(reason, recoilDamage, 0);
        }
    }
}

            

            // if (explodePlayer.IsGrenadierBracersOn != true)
            // {
            //     player.statLife -= 5;
            // if (player.statLife <= 0)
            // {
            //     var reason = PlayerDeathReason.ByCustomReason(
            //     Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.BlueFireDeathMessage", player.name));
            //     player.KillMe(reason, 5, 0);
            // }
                
            // }

   