using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;

using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.FlashFreezeHeatWave;
using Microsoft.Xna.Framework;




public class FlashFreezeSkill : QuirkBaseSkill
{
    
    public override string Name => "Flash Freeze Heatwave";

   
    public override string Description => "Cool the air around yourself and quickly heat it up releasing a powerfull Heatwave";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/HCHH/FlashFreezeIcon";
    public override string Category => "HalfColdHalfHot";

    public override int BaseCooldown => 2400;

    public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
   


    public override void OnUse(Player player)
    {
        var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();

        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        
        Projectile.NewProjectile(
            player.GetSource_FromThis(), 
            player.Center, 
            Vector2.Zero, 
            ModContent.ProjectileType<ChargeFlashFreezeHeatWaveProj>(), 
            0, 
            0f, 
            player.whoAmI
        );
        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/TodorokiIce") with { Volume = 0.5f, Pitch = +0.8f }, player.position);

        
    }}
      