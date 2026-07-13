// MyHeroMod/content/UI/SkillMenuTabContent.cs
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.Audio;
using Terraria.ID;
using System.Collections.Generic;
using MyHeroMod.content.Quirks;
using MyHeroMod.content.System;

namespace MyHeroMod.content.UI
{
    public class SkillMenuTabContent : UIElement
    {
        private UIText descriptionText;
        private QuirkSkills selectedSkill = QuirkSkills.None;

        public SkillMenuTabContent()
        {
            Width.Set(0, 1f);
            Height.Set(0, 1f);

            var player = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
            player.UpdateUnlockedSkills();

            UIPanel listPanel = new UIPanel();
            listPanel.Width.Set(230, 0);
            // Fix: Set height to 100% minus 90px to leave room for the description panel
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

            // 2. SLOT BUTTONS (Right side)
            // These are perfectly anchored to the right of the listPanel (Left = 250)
            CreateSlotButton(player, "Slot 1 (Z)", 10, 1);
            CreateSlotButton(player, "Slot 2 (X)", 70, 2);
            CreateSlotButton(player, "Slot 3 (C)", 130, 3);
            CreateSlotButton(player, "Slot 4 (V)", 190, 4);

            // 3. DESCRIPTION PANEL (Bottom span)
            UIPanel descPanel = new UIPanel();
            // Spans the full width of the menu minus 20px for padding
            descPanel.Width.Set(-20, 1f); 
            descPanel.Height.Set(60, 0);
            descPanel.Left.Set(10, 0);
            // Anchors perfectly 70px from the bottom
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
                if (skillInstance == null || !skillInstance.CheckUnlock(player)) continue;

                UIPanel button = new UIPanel();
                button.Width.Set(0, 1f);
                button.Height.Set(40, 0);
                button.BackgroundColor = new Color(60, 60, 100);

                UIText text = new UIText(skillInstance.Name, 0.7f);
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

        private void CreateSlotButton(TransformationPlayer player, string label, float top, int slotNum)
        {
            UIPanel slotBtn = new UIPanel();
            slotBtn.Width.Set(-260, 1f);
            slotBtn.Height.Set(50, 0);
            slotBtn.Left.Set(250, 0);
            slotBtn.Top.Set(top, 0);
            slotBtn.BackgroundColor = Color.DarkSlateBlue;

            UIText slotText = new UIText(label);
            slotText.HAlign = 0.5f;
            slotText.VAlign = 0.5f;
            slotBtn.Append(slotText);

            slotBtn.OnLeftClick += (evt, elem) =>
            {
                if (selectedSkill == QuirkSkills.None)
                {
                    Main.NewText("Select a skill first!", Color.Red);
                    return;
                }

                var skillInstance = SkillLibrary.GetSkill(selectedSkill);
                if (slotNum == 1) player.Slot1 = selectedSkill;
                if (slotNum == 2) player.Slot2 = selectedSkill;
                if (slotNum == 3) player.Slot3 = selectedSkill;
                if (slotNum == 4) player.Slot4 = selectedSkill;

                Main.NewText($"Assigned {skillInstance.Name} to Slot {slotNum}!", Color.Green);
                slotText.SetText($"{label}: {skillInstance.Name}");
                SoundEngine.PlaySound(SoundID.MenuOpen);
            };

            Append(slotBtn);
        }
    }
}