using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.AllForOne;
using KhacesCore.Content.System;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.OFA8th;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Buffs.Gearshift;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.DetroitSmash;

namespace MyHeroMod.content.Quirks.OFA9th.Skills 
{
public class DetroitSmashSkill : QuirkBaseSkill
{
    public override string Name => "Detroit Smash";
    public override string Description => "Propel air forward with a massive punch";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/DetroitSmashIcon";

    public override int BaseCooldown => 900;
    public override string Category => "OneForAll9th";

    public override QuirkType RequiredQuirk => QuirkType.Quirkless;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;


        public override bool CheckUnlock(TransformationPlayer player)
        {
            if (player.HasActiveQuirk(QuirkType.OneForAll9th) || player.HasActiveQuirk(QuirkType.OneForAll8th))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public override void OnUse(Player player)
        {
            int type = ModContent.ProjectileType<ChargeDetroitProj>();
            if (player.ownedProjectileCounts[type] > 0)
                return;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero,
                type,
                0,
                0f,
                player.whoAmI
            );

            int onomatopoeiaType = player.HasBuff(ModContent.BuffType<GearshiftBuff>())
            ? ModContent.ProjectileType<GearDekuDetroitOnomatopoeia>()
            : ModContent.ProjectileType<DekuDetroitOnomatopoeia>();

            int textIdx = Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center + new Vector2(0, -160f),
                Vector2.Zero,
                onomatopoeiaType,
                0,
                0f,
                player.whoAmI
            );

            if (textIdx >= 0 && textIdx < Main.maxProjectiles)
            {
                Main.projectile[textIdx].scale = 2.5f;
            }
        }
    }
}