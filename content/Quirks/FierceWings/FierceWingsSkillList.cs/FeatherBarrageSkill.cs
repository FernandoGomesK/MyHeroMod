using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Explosion.Projectiles.ApShot;
using MyHeroMod.content.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.FierceWings.FierceWingsSkillList.cs
{
    public class FeatherBarrageSkill: QuirkBaseSkill
    {
        public override string Name => "Piercing Feather Barrage";
        public override string Description => "Shoot a barrage of high speed feathers";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/FierceWings/FeatherBarrageIcon";

        public override string Category => "Fierce Wings";

        public override int BaseCooldown => 300;

        public override QuirkType RequiredQuirk => QuirkType.FierceWings;
        public override QuirkStage RequiredStage => QuirkStage.Adequation;
        public override bool IsDefaultSkill => false;



        public override void OnUse(Player player)
        {
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();


            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                direction,
                ModContent.ProjectileType<FeatherBarrageProj>(),
                0,
                0f,
                player.whoAmI

             );
        }
        }
}
