using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.Audio;
using Terraria.ID;
using System.Collections.Generic;
using MyHeroMod.content.System;
using KhacesCore.Content.System;

namespace MyHeroMod.content.UI
{
    public class SkillMenuTabContent : UIElement
    {
        private UIText descriptionText;
        private string selectedSkill = "None"; 

        public SkillMenuTabContent()
        {
            Width.Set(0, 1f);
            Height.Set(0, 1f);

            var player = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
            player.UpdateUnlockedSkills();

            // --- LEFT PANEL: SKILL LIST ---
            UIPanel listPanel = new UIPanel();
            listPanel.Width.Set(230, 0);
            listPanel.Height.Set(-90, 1f); 
            listPanel.Left.Set(10, 0);
            listPanel.Top.Set(10, 0);
            listPanel.BackgroundColor = new Color(20, 20, 40);
            Append(listPanel);

            UIList skillList = new UIList();
            skillList.Width.Set(-20, 1f);
            skillList.Height.Set(0, 1f);
            listPanel.Append(skillList);

            var scrollbar = new UIScrollbar();
            scrollbar.SetView(100f, 1000f);
            scrollbar.Height.Set(0, 1f);
            scrollbar.HAlign = 1f;
            listPanel.Append(scrollbar);
            skillList.SetScrollbar(scrollbar);

            // --- RIGHT PANEL: SLOT SETTINGS ---
            UIPanel slotListPanel = new UIPanel();
            slotListPanel.Left.Set(250, 0);
            slotListPanel.Top.Set(10, 0);
            slotListPanel.Width.Set(-260, 1f);
            slotListPanel.Height.Set(-90, 1f); // Keeps room for the description panel
            slotListPanel.BackgroundColor = new Color(20, 20, 40);
            Append(slotListPanel);

            UIList slotList = new UIList();
            slotList.Width.Set(-20, 1f);
            slotList.Height.Set(0, 1f);
            slotList.ListPadding = 5f; // Adds a small gap between buttons
            slotListPanel.Append(slotList);

            var slotScrollbar = new UIScrollbar();
            slotScrollbar.SetView(100f, 1000f);
            slotScrollbar.Height.Set(0, 1f);
            slotScrollbar.HAlign = 1f;
            slotListPanel.Append(slotScrollbar);
            slotList.SetScrollbar(slotScrollbar);

            // Populate the right panel with all 8 slots!
            CreateSlotButton(player, "Slot 1 (Z)", 1, slotList);
            CreateSlotButton(player, "Slot 2 (X)", 2, slotList);
            CreateSlotButton(player, "Slot 3 (C)", 3, slotList);
            CreateSlotButton(player, "Slot 4 (V)", 4, slotList);
            
            CreateSlotButton(player, "Slot 5 (Alt+Z)", 5, slotList);
            CreateSlotButton(player, "Slot 6 (Alt+X)", 6, slotList);
            CreateSlotButton(player, "Slot 7 (Alt+C)", 7, slotList);
            CreateSlotButton(player, "Slot 8 (Alt+V)", 8, slotList);

            // --- BOTTOM PANEL: DESCRIPTION ---
            UIPanel descPanel = new UIPanel();
            descPanel.Width.Set(-20, 1f); 
            descPanel.Height.Set(60, 0);
            descPanel.Left.Set(10, 0);
            descPanel.Top.Set(-70, 1f); 
            Append(descPanel);

            descriptionText = new UIText("Select a skill from the list...", 0.7f);
            descriptionText.HAlign = 0.5f;
            descriptionText.VAlign = 0.5f;
            descPanel.Append(descriptionText);

            PopulateSkillList(player, skillList);
        }

        private void PopulateSkillList(TransformationPlayer player, UIList skillList)
        {
            foreach (var skillId in SkillLibrary.GetAllIds()) 
            {
                var skillInstance = SkillLibrary.GetSkill(skillId); 
                if (skillInstance == null) continue;
                
                if (skillInstance is QuirkBaseSkill quirkSkill)
                {
                    if (!quirkSkill.CheckUnlock(player)) continue;
                }
                else
                {
                    if (!skillInstance.CanUse(player.Player)) continue;   
                }
                        
                UIPanel button = new UIPanel();
                button.Width.Set(0, 1f);
                button.Height.Set(40, 0);
                button.BackgroundColor = new Color(60, 60, 100);

                UIText text = new UIText(skillInstance.GetDisplayName(Main.LocalPlayer), 0.7f);
                text.HAlign = 0.5f;
                text.VAlign = 0.5f;
                button.Append(text);

                button.OnMouseOver += (evt, elem) => button.BackgroundColor = new Color(80, 80, 140);
                button.OnMouseOut += (evt, elem) => button.BackgroundColor = new Color(60, 60, 100);
                button.OnLeftClick += (evt, elem) =>
                {
                    selectedSkill = skillId; 
                    descriptionText.SetText(skillInstance.Description);
                    SoundEngine.PlaySound(SoundID.MenuTick);
                };

                skillList.Add(button);
            }
        }

        // Updated parameters: removed 'float top' and added 'UIList parentList'
        private void CreateSlotButton(TransformationPlayer player, string baseLabel, int slotNum, UIList parentList)
        {
            UIPanel slotBtn = new UIPanel();
            // Width is set to fill the parent UIList. Top/Left are ignored inside UILists!
            slotBtn.Width.Set(0, 1f);
            slotBtn.Height.Set(50, 0);
            slotBtn.BackgroundColor = Color.DarkSlateBlue;

            string currentSkill = "None";
            if (slotNum == 1) currentSkill = player.Slot1;
            if (slotNum == 2) currentSkill = player.Slot2;
            if (slotNum == 3) currentSkill = player.Slot3;
            if (slotNum == 4) currentSkill = player.Slot4;
            if (slotNum == 5) currentSkill = player.Slot5;
            if (slotNum == 6) currentSkill = player.Slot6;
            if (slotNum == 7) currentSkill = player.Slot7;
            if (slotNum == 8) currentSkill = player.Slot8;

            string displayLabel = baseLabel;
            if (currentSkill != "None")
            {
                var existingSkill = SkillLibrary.GetSkill(currentSkill);
                if (existingSkill != null)
                {
                    displayLabel = $"{baseLabel}: {existingSkill.GetDisplayName(Main.LocalPlayer)}";
                }
            }

            UIText slotText = new UIText(displayLabel);
            slotText.HAlign = 0.5f;
            slotText.VAlign = 0.5f;
            slotBtn.Append(slotText);

            slotBtn.OnLeftClick += (evt, elem) =>
            {
                if (selectedSkill == "None")
                {
                    Main.NewText("Select a skill first!", Color.Red);
                    return;
                }

                var skillInstance = SkillLibrary.GetSkill(selectedSkill);
                
                if (slotNum == 1) player.Slot1 = selectedSkill;
                if (slotNum == 2) player.Slot2 = selectedSkill;
                if (slotNum == 3) player.Slot3 = selectedSkill;
                if (slotNum == 4) player.Slot4 = selectedSkill;
                if (slotNum == 5) player.Slot5 = selectedSkill;
                if (slotNum == 6) player.Slot6 = selectedSkill;
                if (slotNum == 7) player.Slot7 = selectedSkill;
                if (slotNum == 8) player.Slot8 = selectedSkill;

                Main.NewText($"Assigned {skillInstance.GetDisplayName(Main.LocalPlayer)} to Slot {slotNum}!", Color.Green);
                slotText.SetText($"{baseLabel}: {skillInstance.GetDisplayName(Main.LocalPlayer)}");
                SoundEngine.PlaySound(SoundID.MenuOpen);
            };

            
            parentList.Add(slotBtn);
        }
    }
}