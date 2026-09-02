using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using KhacesCore.Content.System;

namespace MyHeroMod.content.Items.Support.DekuArmor.DekuArmorGauntlets.Skills
{
    public class ActivateDekuArmorSkill : QuirkBaseSkill
    {
        public override string Name => "Suit Up: Deku Armor";
        public override string Description => "Deploy the Deku Armor from the briefcase.";
        
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/DekuArmor/SuitUpIcon";
        
        public override string Category => "Deku Armor";
        public override int BaseCooldown => 60;
        public override QuirkType RequiredQuirk => QuirkType.Quirkless;
        public override QuirkStage RequiredStage => QuirkStage.Initial;

        public override bool isItemSkill => true;
        public override int RequiredItemId => ModContent.ItemType<DekuArmorBriefcase>(); 
        
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            
            for (int i = 3; i < 10; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<DekuArmorBriefcase>())
                {
                    player.armor[i].SetDefaults(ModContent.ItemType<DekuArmorGauntlets>());
                    break;
                }
            }

            
            SafeEquipArmor(player, 1, ModContent.ItemType<DekuArmorChest>());
            SafeEquipArmor(player, 2, ModContent.ItemType<DekuArmorBoots>());

            
            SoundEngine.PlaySound(SoundID.Item37, player.position); 
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
            }

            
            string thisSkillId = "";
            string nextSkillId = "";
            foreach (var id in SkillLibrary.GetAllIds())
            {
                var skill = SkillLibrary.GetSkill(id);
                if (skill is ActivateDekuArmorSkill) thisSkillId = id;
                if (skill is DeactivateDekuArmorSkill) nextSkillId = id;
            }

            if (thisSkillId != "" && nextSkillId != "")
            {
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                if (transPlayer.Slot1 == thisSkillId) transPlayer.Slot1 = nextSkillId;
                if (transPlayer.Slot2 == thisSkillId) transPlayer.Slot2 = nextSkillId;
                if (transPlayer.Slot3 == thisSkillId) transPlayer.Slot3 = nextSkillId;
                if (transPlayer.Slot4 == thisSkillId) transPlayer.Slot4 = nextSkillId;
                if (transPlayer.Slot5 == thisSkillId) transPlayer.Slot5 = nextSkillId;
                if (transPlayer.Slot6 == thisSkillId) transPlayer.Slot6 = nextSkillId;
                if (transPlayer.Slot7 == thisSkillId) transPlayer.Slot7 = nextSkillId;
                if (transPlayer.Slot8 == thisSkillId) transPlayer.Slot8 = nextSkillId;
            }
        }

        private void SafeEquipArmor(Player player, int slotIndex, int newItemType)
        {
            Item currentItem = player.armor[slotIndex];
            if (currentItem.type == newItemType) return;

            if (!currentItem.IsAir)
            {
                player.QuickSpawnItem(player.GetSource_Misc("DekuArmorSuitUp"), currentItem, currentItem.stack);
            }

            Item newArmor = new Item();
            newArmor.SetDefaults(newItemType);
            player.armor[slotIndex] = newArmor;
        }
    }
}