using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using Terraria.ID;
using Terraria.Audio;
using KhacesCore.Content.System;

namespace MyHeroMod.content.Items.Armor.Deku.GammaArmor
{
    public class ToggleGammaDekuCowlSkill : QuirkBaseSkill
    {
        public override string Name => "Toggle Gamma Cowl";
        public override string Description => "Puts the Gamma Cowl up or down.";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/DekuArmor/SuitUpIcon";

        public override string Category => "Gamma Costume";
        public override int BaseCooldown => 60;
        public override QuirkType RequiredQuirk => QuirkType.Quirkless;
        public override QuirkStage RequiredStage => QuirkStage.Initial;

        public override bool IsItemSkill => true;
        public override int RequiredItemId => ModContent.ItemType<GammaHelmet>();
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            var gammaManager = player.GetModPlayer<GammaDekuManager>();

            gammaManager.isCowlUp = !gammaManager.isCowlUp;

            SoundEngine.PlaySound(SoundID.Item37, player.position);

            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
            }
        }
    }
}