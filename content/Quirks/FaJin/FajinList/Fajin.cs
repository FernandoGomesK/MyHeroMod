using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.GeneralSkills;
using Terraria.Audio;
using MyHeroMod.content.Projectiles;

namespace MyHeroMod.content.Quirks.FaJin.Skills
{
    public class FaJinToggleSkill : BaseToggleSkill
    {
        public override string Name => "Fa Jin";
        public override string Description => "Toggle Fa Jin energy buildup.";

        public override string IconPath => "MyHeroMod/Assets/Skills/FaJinIcon";
        public override string Category => "FaJin";
        public override int BaseCooldown => 30;

        public override QuirkType RequiredQuirk => QuirkType.FaJin;
        public override QuirkStage RequiredStage => QuirkStage.Advanced;
        public override bool IsBaseQuirk => true;
        public override int BuffType => ModContent.BuffType<FaJinActiveBuff>();
        public override string ToggleOnText => "";
        public override string ToggleOffText => "";
        public override SoundStyle? ToggleSound => new SoundStyle("MyHeroMod/Assets/Sounds/FaJinSound");
        public override float ToggleSoundVolume => 0.2f; 

        public override int OnomatopoeiaProjType => ModContent.ProjectileType<FajinOnomatopoeia>();
        public override float OnomatipoeiaSpawnOffset => -100f;



        public override void OnToggleOff(Player player)
        {
            var fajinPlayer = player.GetModPlayer<FajinPlayer>();  
            fajinPlayer.FaJinCharges = 0;
        }
    }
}