using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.Interfaces;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public abstract class BaseToggleFlashfireFistSkill : QuirkBaseSkill
{

    public abstract override string Name { get; }
    public abstract override string Category { get; }
    
    public override string Description => "Toggle the Flashfire Fist state, increasing heat and empowering skills.";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override int BaseCooldown => 30;
    public override bool IsDefaultSkill => false;
    
    public abstract override QuirkType RequiredQuirk { get; }
    public abstract override QuirkStage RequiredStage { get; }

    public override void OnUse(Player player)
    {
            if (player.HasBuff(ModContent.BuffType<FlashfireFistBuff>()))
            {
                player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
            }
            else
            {
                player.AddBuff(ModContent.BuffType<FlashfireFistBuff>(), 3600);

                foreach (var modPlayer in player.ModPlayers)
                {
                    if (modPlayer is IHeroTemperature heatUser) 
                    {
                        heatUser.AddHeat(15);
                    }
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/CremationSound") { Volume = 0.5f, PitchVariance = 1.0f }, player.Center);
                }
            }
        }
    }
}


namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public class HellflameFlashFireFist : BaseToggleFlashfireFistSkill
    {
      
        public override string Name => "Hellflame_FlashfireFist"; 
        public override string Category => "HellFlames";
        
        public override QuirkType RequiredQuirk => QuirkType.HellFlames;
        public override QuirkStage RequiredStage => QuirkStage.Adequation;
      
        public override string GetDisplayName(Player player) => "Flashfire Fist";
    }

    public class HCHHFlashfireFist : BaseToggleFlashfireFistSkill
    {
        public override string Name => "HCHH_FlashfireFist";
        public override string Category => "HalfColdHalfHot";
        
        public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
        public override QuirkStage RequiredStage => QuirkStage.Adequation;

        public override string GetDisplayName(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            if (transPlayer.CurrentStage >= QuirkStage.Intermediate) return "Flashfire Fist";
            return "Ignite";
        }
    }

    public class BlueflameFlashFireFist : BaseToggleFlashfireFistSkill
    {
        public override string Name => "Blueflame_FlashfireFist";
        public override string Category => "Blueflame";
        
        public override QuirkType RequiredQuirk => QuirkType.Blueflame;
        public override QuirkStage RequiredStage => QuirkStage.Adequation;

        public override string GetDisplayName(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            if (transPlayer.CurrentStage >= QuirkStage.Intermediate) return "Flashfire Fist";
            return "Crazy Torch";
        }
    }
}