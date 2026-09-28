using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.System;
using MyHeroMod.content.Items.QuirkItems.QuirkSyringes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA.Skills;
using MyHeroMod.content.Items.QuirkItems;
using MyHeroMod.content.Items.QuirkItems.QuirkEssences;
using MyHeroMod.content.Quirks.OFA8th;

namespace MyHeroMod.content.Quirks.OFA.Skills
{
    public abstract class PassForwardBase : QuirkBaseSkill
    {
        public override string Name => "One For All: Pass Forward";
        public override string Description => "Pass the torch to a new successor. You will retain the embers of One For All for a limited time.";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA/PassForwardIcon";
        public override int BaseCooldown => 3600; 
        public override string Category => "OneForAll";
        public override bool IsDefaultSkill => false;
        
       
        public abstract QuirkType OFAType { get; }
        public abstract int EssenceItemType { get; }

        public override void OnUse(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.HasActiveQuirk(OFAType))
            {
                int capturedTimeUsed = 0;
                
                if (OFAType == QuirkType.OneForAll9th)
                {
                    var ofa9thPlayer = player.GetModPlayer<OneForAll9thPlayer>();
                    
                    if (ofa9thPlayer.isQuirkless)
                    {
                        Main.NewText("You have already passed on One For All. Only embers remain.", Color.Red);
                        return;
                    }

                    ofa9thPlayer.isQuirkless = true;
                    ofa9thPlayer.SetEmbers();
                    capturedTimeUsed = ofa9thPlayer.timeUsed;
                }
                else if (OFAType == QuirkType.OneForAll8th)
                {
                    var ofa8thPlayer = player.GetModPlayer<OneForAll8thPlayer>();

                    if (ofa8thPlayer.isQuirkless)
                    {
                        Main.NewText("You have already passed on One For All. Only embers remain.", Color.Red);
                        return;
                    }

                    ofa8thPlayer.isQuirkless = true;
                    ofa8thPlayer.SetEmbers();
                    capturedTimeUsed = ofa8thPlayer.timeUsed;
                }

                int itemIndex = Item.NewItem(player.GetSource_DropAsItem(), player.getRect(), EssenceItemType);

            
                if (Main.item[itemIndex].ModItem is BaseQuirkEssence essenceItem)
                {
                    
                    essenceItem.OriginPlayerName = player.name;
                    essenceItem.EssenceQuirkType = OFAType; 
                    essenceItem.timeUsed = capturedTimeUsed;
                }

                Main.NewText("You have passed on One For All...", Color.LightGoldenrodYellow);
            }
        }
    }
}



    public class PassForward9th : PassForwardBase
    {
       
        public override QuirkType OFAType => QuirkType.OneForAll9th;
        public override int EssenceItemType => ModContent.ItemType<OneForAll9thEssence>();
        public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
        public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    }

    