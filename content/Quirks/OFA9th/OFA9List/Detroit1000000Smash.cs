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

    public override string GetDisplayName(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            if (transPlayer.CurrentStage == QuirkStage.Final)
            {
               return "Rising Smash"; 
            } 
            else
            {
                return "Detroit 1000000 Smash";
            }
            
        }

    public override int BaseCooldown => 20;
    public override string Category => "OneForAll9th";

    public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
    public override QuirkStage RequiredStage => QuirkStage.Adequation;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        
        var transPlayer = player.GetModPlayer<TransformationPlayer>();

        if (transPlayer.CurrentStage == QuirkStage.Final)
            {
                Projectile.NewProjectile(
            player.GetSource_FromThis(), 
            player.Center, 
            Vector2.Zero, 
            ModContent.ProjectileType<ChargeFinalSmashProj>(), 
            0, 
            0f, 
            player.whoAmI
        );
            }
        else
            {
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
}