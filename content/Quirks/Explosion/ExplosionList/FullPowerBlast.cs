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

        int BaseDamage = 80; 
           
        float ModifiedDamage = 1;

        if (explodePlayer.IsGrenadierBracersOn && explodePlayer.CurrentSweat > explodePlayer.MaxSweat){
        explodePlayer.CurrentSweat -= 30;    
        ModifiedDamage += 1f;        
        }
        int FinalDamage = (int)(BaseDamage * ModifiedDamage);

        CombatText.NewText(player.getRect(), Color.Orange, "DIE!");
            



         Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Velocity,
                ModContent.ProjectileType<FullPowerProj>(),
                FinalDamage, 
                2f, 
                player.whoAmI
            );

            if (explodePlayer.IsGrenadierBracersOn != true)
            {
                player.statLife -= 5;
            if (player.statLife <= 0)
            {
                var reason = PlayerDeathReason.ByCustomReason(
                Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.BlueFireDeathMessage", player.name));
                player.KillMe(reason, 5, 0);
            }
                
            }

    }
}