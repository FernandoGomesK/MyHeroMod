using MyHeroMod.content;
using MyHeroMod.content.Items.QuirkItems.QuirkEssences;
using MyHeroMod.content.Quirks.OFA.Skills;
using Terraria.ModLoader;

public class PassForward8th: PassForwardBase
    {
        public override QuirkType OFAType => QuirkType.OneForAll8th;
        public override int EssenceItemType => ModContent.ItemType<OneForAll8thEssence>();
        
        public override QuirkType RequiredQuirk => QuirkType.OneForAll8th;
        public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    }