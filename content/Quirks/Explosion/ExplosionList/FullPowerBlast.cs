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
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.Explosion.Projectiles.FullPower;

public class FullPowerBlastSkill : QuirkBaseSkill
{
    public override string Name => "Full Power Blast";
    public override string Description => "Shoot a concentrated penetrating Projectile";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/Explosion/FullPowerIcon";

    public override string Category => "Explosion";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.Explosion;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    
    public override void OnUse(Player player)
    {
        var explodePlayer = player.GetModPlayer<ExplosionPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

       
        bool canFire = false;
        float damageMultiplier = 1.0f;

        if (explodePlayer.IsGrenadierBracersOn)
        {
        
            if (explodePlayer.CurrentSweat >= 30)
            {
                explodePlayer.CurrentSweat -= 30; 
                damageMultiplier += 1.0f; 
                canFire = true;
            }
        }
        else
        {
         
            int requiredSweat = transPlayer.Nature == NatureType.Resourceful ? 15 : 30;
            int drainAmount = transPlayer.Nature == NatureType.Resourceful ? 10 : 20;

            if (explodePlayer.CurrentSweat >= requiredSweat)
            {
                explodePlayer.CurrentSweat -= drainAmount;
                canFire = true;
                
             
                ApplyRecoil(player, transPlayer); 
            }
        }

        if (!canFire)
        {
            CombatText.NewText(player.getRect(), Color.Orange, "Not enough sweat!");
            return; 
        }

        
        int MaxDamage = transPlayer.CurrentStage switch {
            QuirkStage.Initial => 25,
            QuirkStage.Adequation => 55,
            QuirkStage.Intermediate => 90,
            QuirkStage.Advanced => 160,
            QuirkStage.Final => 320,
            _ => 45
        };

        if (player.HasBuff(ModContent.BuffType<ClusterBuff>()))
        {
            damageMultiplier += 2.5f; 
        }

        var finalDamage = (int)(damageMultiplier * MaxDamage);

    
        CombatText.NewText(player.getRect(), Color.Orange, "DIE!");
            
        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/Explosion2Sound") { Volume = 1.5f, PitchVariance = 0.3f }, player.Center);
        
        Vector2 Velocity = Main.MouseWorld - player.Center;
        Velocity.Normalize();
        Velocity *= 15f;

        Vector2 textPosition = player.Center + new Vector2(0, -30f);
        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            textPosition,
            Vector2.Zero, 
            ModContent.ProjectileType<BoomOnomatopoeia>(),
            0, 
            0f, 
            player.whoAmI
        );

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

  
    private void ApplyRecoil(Player player, TransformationPlayer transPlayer)
    {
        float recoilPercentage = 0.05f; 
        
        if (transPlayer.Nature == NatureType.ResistantBody)
        {
            recoilPercentage *= 0.5f;
        }

        int recoilDamage = (int)(player.statLifeMax2 * recoilPercentage); 
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