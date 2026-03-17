
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
using MyHeroMod.content.Quirks.HellFlames.Projectiles.ProminenceBurn;


public class ProminenceBurnSkill: QuirkSkill
{
    
    public override string Name => "Prominence Burn";

   
    public override string Description => "Shoot a Huge Ice Spike at your Cursor or Lines of fire";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

public override void OnUse(Player player)
    {
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();

            if (player.ownedProjectileCounts[ModContent.ProjectileType<ProminenceBurnController>()] > 0)
                return;

            Main.NewText("PROMINENCE BURN!!!", Color.OrangeRed);
            
            
            SoundEngine.PlaySound(SoundID.Item117, player.position); 

            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            // Lança o Controlador
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                direction,
                ModContent.ProjectileType<ProminenceBurnController>(),
                0, 
                0f, 
                player.whoAmI
            );
            hellPlayer.CurrentHeat += 60;
        }}