using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using KhacesCore.Content.System;

namespace MyHeroMod.content.Items.Support.DekuArmor.DekuArmorGauntlets.Skills
{
    public class DeactivateDekuArmorSkill : QuirkBaseSkill
    {
        public override string Name => "Suit Down: Deku Armor";
        public override string Description => "Pack the Deku Armor back into the briefcase.";
        
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/DekuArmor/SuitDownIcon";
        
        public override string Category => "Deku Armor";
        public override int BaseCooldown => 60;
        public override QuirkType RequiredQuirk => QuirkType.Quirkless;
        public override QuirkStage RequiredStage => QuirkStage.Initial;

        public override bool isItemSkill => true;
        public override int RequiredItemId => ModContent.ItemType<DekuArmorGauntlets>();
        
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
 
            for (int i = 3; i < 10; i++) 
            {
                if (player.armor[i].type == ModContent.ItemType<DekuArmorGauntlets>())
                {
                    player.armor[i].SetDefaults(ModContent.ItemType<DekuArmorBriefcase>());
                    break;
                }
            }

         
            for (int i = 0; i < 20; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<DekuArmorChest>())
                {
                    player.armor[i] = new Item();
                    player.armor[i].SetDefaults(0);
                    player.body = -1; 
                }
                
                if (player.armor[i].type == ModContent.ItemType<DekuArmorBoots>())
                {
                    player.armor[i] = new Item(); 
                    player.armor[i].SetDefaults(0);
                    player.legs = -1; 
                }
            }

            
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
                if (skill is DeactivateDekuArmorSkill) thisSkillId = id;
                if (skill is ActivateDekuArmorSkill) nextSkillId = id;
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
    }
}