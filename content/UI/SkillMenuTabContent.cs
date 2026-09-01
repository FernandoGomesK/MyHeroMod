using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.Audio;
using Terraria.ID;
using System.Collections.Generic;
using System;
using ReLogic.Content;
using MyHeroMod.content.System;
using KhacesCore.Content.System;
using KhacesCore.Content.System.BaseProjectiles;
using MyHeroMod.content.Handlers;
using Terraria.ModLoader;

namespace MyHeroMod.content.UI
{
    public class SkillMenuTabContent : UIElement
    {
        private string selectedSkill = "None"; 
        private string hoveredTooltip = null; 
        
        
        private UIList skillList;
        private UIText infoText;
        private UIText selectedSkillText;
        
        private List<string> activeCategories = new List<string>();
        private int currentCategoryIndex = 0;

        private TransformationPlayer playerRef;
        private QuirkHandler quirkHandlerRef;

        public SkillMenuTabContent()
        {
            Width.Set(0, 1f);
            Height.Set(0, 1f);

            playerRef = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
            quirkHandlerRef = Main.LocalPlayer.GetModPlayer<QuirkHandler>();
            playerRef.UpdateUnlockedSkills();

            BuildCategoryList();

            
            UIPanel quirkInfoPanel = new UIPanel();
            quirkInfoPanel.Width.Set(-20, 1f);
            quirkInfoPanel.Height.Set(100, 0); 
            quirkInfoPanel.Left.Set(10, 0);
            quirkInfoPanel.Top.Set(10, 0);
            quirkInfoPanel.BackgroundColor = new Color(30, 30, 60);
            Append(quirkInfoPanel);

           
            UIPanel leftArrow = new UIPanel();
            leftArrow.Width.Set(40, 0);
            leftArrow.Height.Set(40, 0);
            leftArrow.Left.Set(10, 0);
            leftArrow.VAlign = 0.5f;
            leftArrow.BackgroundColor = new Color(50, 50, 90);
            leftArrow.OnLeftClick += (evt, elem) => { CycleCategory(-1); };
            leftArrow.OnMouseOver += (evt, elem) => leftArrow.BackgroundColor = new Color(80, 80, 140);
            leftArrow.OnMouseOut += (evt, elem) => leftArrow.BackgroundColor = new Color(50, 50, 90);
            UIText leftText = new UIText("<"); leftText.HAlign = 0.5f; leftText.VAlign = 0.5f;
            leftArrow.Append(leftText);
            quirkInfoPanel.Append(leftArrow);

            
            UIPanel rightArrow = new UIPanel();
            rightArrow.Width.Set(40, 0);
            rightArrow.Height.Set(40, 0);
            rightArrow.Left.Set(-50, 1f); 
            rightArrow.VAlign = 0.5f;
            rightArrow.BackgroundColor = new Color(50, 50, 90);
            rightArrow.OnLeftClick += (evt, elem) => { CycleCategory(1); };
            rightArrow.OnMouseOver += (evt, elem) => rightArrow.BackgroundColor = new Color(80, 80, 140);
            rightArrow.OnMouseOut += (evt, elem) => rightArrow.BackgroundColor = new Color(50, 50, 90);
            UIText rightText = new UIText(">"); rightText.HAlign = 0.5f; rightText.VAlign = 0.5f;
            rightArrow.Append(rightText);
            quirkInfoPanel.Append(rightArrow);

            
            UIElement textContainer = new UIElement();
            textContainer.Left.Set(60, 0);
            textContainer.Width.Set(-120, 1f);
            textContainer.Height.Set(0, 1f);
            textContainer.VAlign = 0.5f;
            textContainer.IgnoresMouseInteraction = true;
            quirkInfoPanel.Append(textContainer);

            infoText = new UIText("", 0.8f); 
            infoText.Width.Set(0, 1f);
            infoText.Height.Set(0, 1f);
            infoText.HAlign = 0.5f;
            infoText.VAlign = 0.5f;
            infoText.IsWrapped = true;
            textContainer.Append(infoText);

            
            UIPanel listPanel = new UIPanel();
            listPanel.Width.Set(230, 0);
            listPanel.Height.Set(-120, 1f); 
            listPanel.Left.Set(10, 0);
            listPanel.Top.Set(110, 0); 
            listPanel.BackgroundColor = new Color(20, 20, 40);
            Append(listPanel);

            skillList = new UIList();
            skillList.Width.Set(-20, 1f);
            skillList.Height.Set(0, 1f);
            listPanel.Append(skillList);

            var scrollbar = new UIScrollbar();
            scrollbar.SetView(100f, 1000f);
            scrollbar.Height.Set(0, 1f);
            scrollbar.HAlign = 1f;
            listPanel.Append(scrollbar);
            skillList.SetScrollbar(scrollbar);

            
            UIPanel slotListPanel = new UIPanel();
            slotListPanel.Left.Set(250, 0);
            slotListPanel.Top.Set(110, 0); 
            slotListPanel.Width.Set(-260, 1f);
            slotListPanel.Height.Set(-120, 1f); 
            slotListPanel.BackgroundColor = new Color(20, 20, 40);
            Append(slotListPanel);

            selectedSkillText = new UIText("Currently Selected: None", 0.9f);
            selectedSkillText.HAlign = 0.5f;
            selectedSkillText.Top.Set(10, 0);
            slotListPanel.Append(selectedSkillText);

            UIList slotList = new UIList();
            slotList.Top.Set(40, 0);
            slotList.Width.Set(-20, 1f);
            slotList.Height.Set(-40, 1f);
            slotList.ListPadding = 5f; 
            slotListPanel.Append(slotList);

            var slotScrollbar = new UIScrollbar();
            slotScrollbar.SetView(100f, 1000f);
            slotScrollbar.Top.Set(40, 0);
            slotScrollbar.Height.Set(-40, 1f);
            slotScrollbar.HAlign = 1f;
            slotListPanel.Append(slotScrollbar);
            slotList.SetScrollbar(slotScrollbar);

            CreateSlotButton("Slot 1 (Z)", 1, slotList);
            CreateSlotButton("Slot 2 (X)", 2, slotList);
            CreateSlotButton("Slot 3 (C)", 3, slotList);
            CreateSlotButton("Slot 4 (V)", 4, slotList);
            CreateSlotButton("Slot 5 (Alt+Z)", 5, slotList);
            CreateSlotButton("Slot 6 (Alt+X)", 6, slotList);
            CreateSlotButton("Slot 7 (Alt+C)", 7, slotList);
            CreateSlotButton("Slot 8 (Alt+V)", 8, slotList);

            RefreshCategory();
        }

        public override void OnActivate()
        {
            base.OnActivate();
            Recalculate();
        }

        private Asset<Texture2D> GetIconAsset(BaseSkill skillInstance)
        {
            if (skillInstance != null && !string.IsNullOrEmpty(skillInstance.IconPath))
            {
                if (ModContent.RequestIfExists<Texture2D>(skillInstance.IconPath, out var customIcon))
                    return customIcon;
            }
            return Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay");
        }

        private void BuildCategoryList()
        {
            activeCategories.Clear();
            foreach (var skillId in SkillLibrary.GetAllIds()) 
            {
                var skill = SkillLibrary.GetSkill(skillId); 
                if (skill == null) continue;

                bool unlocked = skill is QuirkBaseSkill qs ? qs.CheckUnlock(playerRef) : skill.CanUse(playerRef.Player);
                
                if (unlocked && !activeCategories.Contains(skill.Category))
                {
                    activeCategories.Add(skill.Category);
                }
            }
            
            if (activeCategories.Count == 0) activeCategories.Add("None");
        }

        private void CycleCategory(int dir)
        {
            if (activeCategories.Count <= 1) return;
            SoundEngine.PlaySound(SoundID.MenuTick);
            currentCategoryIndex = (currentCategoryIndex + dir + activeCategories.Count) % activeCategories.Count;
            RefreshCategory();
        }

        private void RefreshCategory()
        {
            string currentCategory = activeCategories[currentCategoryIndex];
            skillList.Clear(); 

            if (Enum.TryParse(currentCategory, out QuirkType parsedQuirk))
            {
                string displayName = quirkHandlerRef.GetQuirkDisplayName(parsedQuirk);
                infoText.SetText($"[c/00FFFF:{displayName}:] {quirkHandlerRef.GetQuirkDescription(parsedQuirk)}");
            }
            else
            {
                infoText.SetText($"[c/00FFFF:{currentCategory}:] General techniques and abilities.");
            }

            foreach (var skillId in SkillLibrary.GetAllIds()) 
            {
                var skillInstance = SkillLibrary.GetSkill(skillId); 
                if (skillInstance == null || skillInstance.Category != currentCategory) continue;
                
                bool unlocked = skillInstance is QuirkBaseSkill qs ? qs.CheckUnlock(playerRef) : skillInstance.CanUse(playerRef.Player);
                if (!unlocked) continue;
                        
                UIPanel button = new UIPanel();
                button.Width.Set(0, 1f);
                button.Height.Set(50, 0); 
                button.BackgroundColor = new Color(60, 60, 100);

                UIImage icon = new UIImage(GetIconAsset(skillInstance));
                icon.Width.Set(32, 0); 
                icon.Height.Set(32, 0); 
                icon.Left.Set(5, 0);
                icon.VAlign = 0.5f;
                icon.IgnoresMouseInteraction = true; 
                button.Append(icon);

                UIText text = new UIText(skillInstance.GetDisplayName(Main.LocalPlayer), 0.7f);
                text.Left.Set(45, 0); 
                text.VAlign = 0.5f;
                text.IgnoresMouseInteraction = true;
                button.Append(text);

                button.OnMouseOver += (evt, elem) => button.BackgroundColor = new Color(80, 80, 140);
                button.OnMouseOut += (evt, elem) => button.BackgroundColor = new Color(60, 60, 100);
                
                button.OnLeftClick += (evt, elem) =>
                {
                    selectedSkill = skillId; 
                    selectedSkillText.SetText($"Currently Selected: [c/00FF00:{skillInstance.GetDisplayName(Main.LocalPlayer)}]");
                    SoundEngine.PlaySound(SoundID.MenuTick);
                };

                button.OnUpdate += (elem) =>
                {
                    if (elem.IsMouseHovering) hoveredTooltip = skillInstance.Description;
                };

                skillList.Add(button);
            }
            
            Recalculate();
        }

        private void CreateSlotButton(string baseLabel, int slotNum, UIList parentList)
        {
            UIPanel slotBtn = new UIPanel();
            slotBtn.Width.Set(0, 1f);
            slotBtn.Height.Set(50, 0);
            slotBtn.BackgroundColor = Color.DarkSlateBlue;

            string currentSkill = slotNum switch {
                1 => playerRef.Slot1, 2 => playerRef.Slot2, 3 => playerRef.Slot3, 4 => playerRef.Slot4,
                5 => playerRef.Slot5, 6 => playerRef.Slot6, 7 => playerRef.Slot7, 8 => playerRef.Slot8,
                _ => "None"
            };

            string displayLabel = baseLabel;
            UIImage currentIconElement = null; 

            if (currentSkill != "None")
            {
                var existingSkill = SkillLibrary.GetSkill(currentSkill);
                if (existingSkill != null)
                {
                    displayLabel = $"{baseLabel}: {existingSkill.GetDisplayName(Main.LocalPlayer)}";
                    
                    currentIconElement = new UIImage(GetIconAsset(existingSkill));
                    currentIconElement.Width.Set(32, 0);
                    currentIconElement.Height.Set(32, 0);
                    currentIconElement.Left.Set(5, 0);
                    currentIconElement.VAlign = 0.5f;
                    currentIconElement.IgnoresMouseInteraction = true;
                    slotBtn.Append(currentIconElement);
                }
            }

            UIText slotText = new UIText(displayLabel);
            slotText.Left.Set(45, 0); 
            slotText.VAlign = 0.5f;
            slotText.IgnoresMouseInteraction = true;
            slotBtn.Append(slotText);

            slotBtn.OnLeftClick += (evt, elem) =>
            {
                if (selectedSkill == "None")
                {
                    Main.NewText("Select a skill from the list first!", Color.Red);
                    return;
                }

                var skillInstance = SkillLibrary.GetSkill(selectedSkill);
                
                switch (slotNum) {
                    case 1: playerRef.Slot1 = selectedSkill; break;
                    case 2: playerRef.Slot2 = selectedSkill; break;
                    case 3: playerRef.Slot3 = selectedSkill; break;
                    case 4: playerRef.Slot4 = selectedSkill; break;
                    case 5: playerRef.Slot5 = selectedSkill; break;
                    case 6: playerRef.Slot6 = selectedSkill; break;
                    case 7: playerRef.Slot7 = selectedSkill; break;
                    case 8: playerRef.Slot8 = selectedSkill; break;
                }

                if (currentIconElement != null)
                {
                    slotBtn.RemoveChild(currentIconElement);
                    currentIconElement = null;
                }

                currentIconElement = new UIImage(GetIconAsset(skillInstance));
                currentIconElement.Width.Set(32, 0);
                currentIconElement.Height.Set(32, 0);
                currentIconElement.Left.Set(5, 0);
                currentIconElement.VAlign = 0.5f;
                currentIconElement.IgnoresMouseInteraction = true;
                slotBtn.Append(currentIconElement);

                slotText.SetText($"{baseLabel}: {skillInstance.GetDisplayName(Main.LocalPlayer)}");
                slotBtn.Recalculate(); 

                Main.NewText($"Assigned {skillInstance.GetDisplayName(Main.LocalPlayer)} to Slot {slotNum}!", Color.Green);
                SoundEngine.PlaySound(SoundID.MenuOpen);
            };

            parentList.Add(slotBtn);
        }

        public override void Update(GameTime gameTime)
        {
            hoveredTooltip = null;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (!string.IsNullOrEmpty(hoveredTooltip))
            {
                Vector2 mousePos = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(16, 16);
                var font = Terraria.GameContent.FontAssets.MouseText.Value;
                Vector2 textSize = font.MeasureString(hoveredTooltip);

                Rectangle bgRect = new Rectangle((int)mousePos.X, (int)mousePos.Y, (int)textSize.X + 20, (int)textSize.Y + 20);

                if (bgRect.Right > Main.screenWidth) bgRect.X = Main.screenWidth - bgRect.Width - 10;
                if (bgRect.Bottom > Main.screenHeight) bgRect.Y = Main.screenHeight - bgRect.Height - 10;

                Utils.DrawInvBG(spriteBatch, bgRect, new Color(20, 20, 40, 220));
                Utils.DrawBorderStringFourWay(spriteBatch, font, hoveredTooltip, bgRect.X + 10, bgRect.Y + 10, Color.White, Color.Black, Vector2.Zero);
            }
        }
    }
}