using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.GeneralSkills;
using Terraria.Audio;
using MyHeroMod.content.Projectiles;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.OpticBlast.Skills 
{
    public class CloseEyesSkill : BaseToggleSkill
    {
        public override string Name => "Close Eyes";
        public override string Description => "Close your eyes to stop uncontrolled optic blasts, at the cost of your vision.";
        public override string IconPath => "MyHeroMod/Assets/Skills/FaJinIcon";
        public override string Category => "OpticBlast";
        public override int BaseCooldown => 30;

        public override QuirkType RequiredQuirk => QuirkType.OpticBlast;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override QuirkStage RequiredOfaStage => QuirkStage.Advanced;
        
        public override int BuffType => BuffID.Darkness;
        public override string ToggleOnText => "";
        public override string ToggleOffText => "";
        public override SoundStyle? ToggleSound => null; 
        public override float ToggleSoundVolume => 0.2f; 

       
        public override int OnomatopoeiaProjType => -1; 
        public override float OnomatipoeiaSpawnOffset => -100f;



        public override void OnToggleOff(Player player)
        {
            // var fajinPlayer = player.GetModPlayer<FajinPlayer>();  
            // fajinPlayer.FaJinCharges = 0;
        }
    }
}