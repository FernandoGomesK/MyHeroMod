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
public class Detroit1000000SmashSkill : QuirkBaseSkill
{
    public override string Name => "Detroit 1000000 Smash";
    public override string Description => "Propel air forward with a massive punch";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;
    public override string Category => "OneForAll9th";

    public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    

    public override bool CheckUnlock(TransformationPlayer player)
    {
        // var afoPlayer = player.Player.GetModPlayer<AllForOnePlayer>();

        // if (player.HasActiveQuirk(QuirkType.AllForOne) && (afoPlayer.HasInternalQuirk(QuirkType.OneForAll8th) || afoPlayer.HasInternalQuirk(QuirkType.OneForAll9th)))
        // {
        //     return true;
        // }
        if (player.HasActiveQuirk(QuirkType.OneForAll9th))
            {
                return true;
            }

        return false;
    }

    public override void OnUse(Player player)
    {
        
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

       
        
        Projectile.NewProjectile(
            player.GetSource_FromThis(), 
            player.Center, 
            Vector2.Zero, 
            ModContent.ProjectileType<Charge1000000DetroitProj>(), 
            0, 
            0f, 
            player.whoAmI
        );
    }
}
}