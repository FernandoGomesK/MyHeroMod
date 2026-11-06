using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.GeneralSkills;
using Terraria.Audio;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;

namespace MyHeroMod.content.Quirks.FaJin.Skills
{
    public class ToggleConcentrationSkill : BaseToggleSkill
    {
        public override string Name => "Concentrate";
        public override string Description => "Toggle Explosion energy buildup.";

        public override string IconPath => "MyHeroMod/Assets/SkillIcons/Fajin/Fajin";
        public override string Category => "Blueglame";
        public override int BaseCooldown => 30;

        public override QuirkType RequiredQuirk => QuirkType.Blueflame;
        public override QuirkStage RequiredStage => QuirkStage.Initial;

        public override int BuffType => ModContent.BuffType<ConcentratingBuff>();
        public override string ToggleOnText => "";
        public override string ToggleOffText => "";
        public override SoundStyle? ToggleSound => new SoundStyle("MyHeroMod/Assets/Sounds/FaJinSound");
        public override float ToggleSoundVolume => 0.2f;

        public override int OnomatopoeiaProjType => ModContent.ProjectileType<FajinOnomatopoeia>();
        public override float OnomatipoeiaSpawnOffset => -100f;



        public override void OnToggleOff(Player player)
        {
   
           
            var bluePlayer = player.GetModPlayer<BlueflamePlayer>();
            bluePlayer.isConcentrating = false;

        }
    }
}