
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.HellFlames.Projectiles.ProminenceBurn;
using MyHeroMod.content.Quirks.Blueflames;
using MyHeroMod.content.Quirks.AllForOne;


public class ProminenceBurnSkill: QuirkSkill
{
    
    public override string Name => "Prominence Burn";

   
    public override string Description => "Shoot a Huge Ice Spike at your Cursor or Lines of fire";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 320;

    public override QuirkType RequiredQuirk => QuirkType.HellFlames;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override bool CheckUnlock(TransformationPlayer player)
    {
        if (player.HasActiveQuirk(QuirkType.HellFlames))
        {
            return player.CurrentStage >= QuirkStage.Intermediate; 
        }
            
        else if(player.HasActiveQuirk(QuirkType.BlueFlames))
        {
            return player.CurrentStage >= QuirkStage.Advanced; 
        }
        return false;
    }

public override void OnUse(Player player)
    {
        var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();

            if (player.ownedProjectileCounts[ModContent.ProjectileType<ProminenceBurnController>()] > 0)
                return;

            Main.NewText("PROMINENCE BURN!!!", Color.OrangeRed);
            
            
            SoundEngine.PlaySound(SoundID.Item117, player.position); 

            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            
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