using MyHeroMod.content.Items.Weapons;
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
    public class GetFeatherSwordSkill : QuirkBaseSkill
    {
        public override string Name => "Feather Sword";
        public override string Description => "Make a sword out of feathers";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/FierceWings/FeatherBladeIcon";

        public override string Category => "Fierce Wings";

        public override int BaseCooldown => 1200;

        public override QuirkType RequiredQuirk => QuirkType.FierceWings;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;



        public override void OnUse(Player player)
        {

            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            var fiercePlayer = player.GetModPlayer<FierceWingsPlayer>();
            if (fiercePlayer.currentFeathers <= 20)
            {
                return;
            }
            else
            {
                fiercePlayer.RemoveFeathers(20);

                int swordItemID = ModContent.ItemType<FeatherBlade>();

                player.QuickSpawnItem(player.GetSource_FromThis(), swordItemID, 1);
            }
        }
     }
}
