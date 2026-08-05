using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.AllForOne;
using KhacesCore.Content.System;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.OFA8th;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.OFA9th.Skills 
{
public class DetroitSmashSkill : QuirkBaseSkill
{
    public override string Name => "Detroit Smash";
    public override string Description => "Propel air forward with a massive punch";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;
    public override string Category => "OneForAll9th";

    public override QuirkType RequiredQuirk => QuirkType.Quirkless;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    


    public override bool CheckUnlock(TransformationPlayer player)
    {
        var afoPlayer = player.Player.GetModPlayer<AllForOnePlayer>();

        if (player.HasActiveQuirk(QuirkType.OneForAll8th) || player.HasActiveQuirk(QuirkType.OneForAll9th))
            return player.CurrentStage >= QuirkStage.Initial;

        if (player.HasActiveQuirk(QuirkType.AllForOne) && (afoPlayer.HasInternalQuirk(QuirkType.OneForAll8th) || afoPlayer.HasInternalQuirk(QuirkType.OneForAll9th)))
        {
            return true;
        }

        return false;
    }

    public override void OnUse(Player player)
    {
        
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        
        
        int onomatopoeiaType = ModContent.ProjectileType<DetroitOnomatopoeia>();

        
        if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
        {
            if (player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            {
                onomatopoeiaType = ModContent.ProjectileType<GearDekuDetroitOnomatopoeia>();
            }
            else
            {
                onomatopoeiaType = ModContent.ProjectileType<DekuDetroitOnomatopoeia>();
            }
            
        }

        
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
            ModContent.ProjectileType<ChargeDetroitProj>(), 
            0, 
            0f, 
            player.whoAmI
        );
    }
}
}