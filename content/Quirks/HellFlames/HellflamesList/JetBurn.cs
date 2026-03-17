using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.IceShot;
using MyHeroMod.content.Projectiles.HellSpider;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HCHellSpider;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.HellFlames.Projectiles.IgnitedArrow;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.JetKindling;


public class JetBurnSkill: QuirkSkill
{
    
    public override string Name => "Flash Fire Fist: Jet Burn";

   
    public override string Description => "Shoot a Huge Ice Spike at your Cursor or Lines of fire";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();

if (player.ownedProjectileCounts[ModContent.ProjectileType<JetKindlingController>()] > 0) return;

            int fireDamage = 20;
            float multiplier = 1.0f;
            switch(transPlayer.CurrentStage) {
                case QuirkStage.Initial: fireDamage = 12; break;
                case QuirkStage.Adequation: fireDamage = 22; break;
                case QuirkStage.Intermediate: fireDamage = 55; break;
                case QuirkStage.Advanced: fireDamage = 130; break;
                case QuirkStage.Final: fireDamage = 350; break;
            }
            int finalDamage = (int)(fireDamage * multiplier);
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            Projectile.NewProjectile(
                player.GetSource_FromThis(), player.Center, direction,
                ModContent.ProjectileType<JetKindlingController>(), finalDamage, 0f, player.whoAmI
            );
            
            hellPlayer.CurrentHeat += 25; 
        }}