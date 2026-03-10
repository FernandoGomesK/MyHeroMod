using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Quirks;
using System.Collections.Generic;
using MyHeroMod.content;
using MyHeroMod.content.System;

namespace MyHeroMod.content.UI
{
    public class SkillMenuUI : UIState
    {
        private UIText title;
        private UIPanel mainPanel;
        private UIList skillList;
        private UIText descriptionText;
        private QuirkSkills selectedSkill = QuirkSkills.None; 

        public override void OnInitialize()
        {
            

            // 1. Painel Principal
            mainPanel = new UIPanel();
            mainPanel.Width.Set(700, 0);
            mainPanel.Height.Set(550, 0);
            mainPanel.HAlign = 0.5f;
            mainPanel.VAlign = 0.5f;
            mainPanel.BackgroundColor = new Color(33, 43, 79); 
            Append(mainPanel);

            title = new UIText("Status", 0.8f, true);
            title.HAlign = 0.5f;
            title.Top.Set(10, 0);
            mainPanel.Append(title);

            // 2. LADO ESQUERDO: Lista de Skills
            UIPanel listPanel = new UIPanel();
            listPanel.Width.Set(300, 0);
            listPanel.Height.Set(350, 0);
            listPanel.Left.Set(20, 0);
            listPanel.Top.Set(50, 0);
            listPanel.BackgroundColor = new Color(20, 20, 40);
            mainPanel.Append(listPanel);

            skillList = new UIList();
            skillList.Width.Set(0, 1f);
            skillList.Height.Set(0, 1f);
            listPanel.Append(skillList);

            var scrollbar = new UIScrollbar();
            scrollbar.SetView(100f, 1000f);
            scrollbar.Height.Set(0, 1f);
            scrollbar.Left.Set(0, 0.9f); 
            listPanel.Append(scrollbar);
            skillList.SetScrollbar(scrollbar);

            // 3. LADO DIREITO: Slots (Z, X, C, G)
            CreateSlotButton("Slot 1 (Z)", 60, 1);
            CreateSlotButton("Slot 2 (X)", 140, 2);
            CreateSlotButton("Slot 3 (C)", 220, 3);
            CreateSlotButton("Slot 4 (v)", 300, 4);

            // 4. RODAPÉ: Descrição
            UIPanel descPanel = new UIPanel();
            descPanel.Width.Set(540, 0);
            descPanel.Height.Set(100, 0);
            descPanel.Top.Set(410, 0);
            descPanel.HAlign = 0.5f;
            mainPanel.Append(descPanel);

            descriptionText = new UIText("Select a skill from the list...", 0.8f);
            descriptionText.HAlign = 0.5f;
            descriptionText.VAlign = 0.5f;
            descPanel.Append(descriptionText);
        }

        public override void OnActivate()
        {
            base.OnActivate();

            var modPlayer = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
            
            // Garante que a lista de desbloqueios está atualizada antes de abrir o menu
            modPlayer.UpdateUnlockedSkills();

            string quirkName = modPlayer.SelectedQuirk.ToString();
            
            // Título Bonito
            if (modPlayer.SelectedQuirk == QuirkType.OneForAll9th) quirkName = "One For All 9th";
            else if (modPlayer.SelectedQuirk == QuirkType.OneForAll8th) quirkName = "One For All 8th";
            else if (modPlayer.SelectedQuirk == QuirkType.Quirkless) quirkName = "Quirkless";
            else if (modPlayer.SelectedQuirk == QuirkType.Gearshift) quirkName = "Gearshift";

            string dynamicText = $"{quirkName} - Stage: {modPlayer.CurrentStage}";
            title.SetText(dynamicText);
            
            PopulateSkillList(); 
        }

        private void PopulateSkillList()
        {
            skillList.Clear();    
            if (Main.LocalPlayer == null || !Main.LocalPlayer.active) return;

            var player = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();

            foreach (var skillId in SkillLibrary.GetAllIds())
            {
                var skillInstance = SkillLibrary.GetSkill(skillId);
                if (skillInstance == null) continue;

                
                if (skillInstance.CheckUnlock(player))
                {
                    UIPanel button = new UIPanel();
                    button.Width.Set(250, 0);
                    button.Height.Set(40, 0);
                    button.BackgroundColor = new Color(60, 60, 100);

                    UIText text = new UIText(skillInstance.Name, 0.7f);
                    text.HAlign = 0.5f;
                    text.VAlign = 0.5f;
                    button.Append(text);

                    // Hover (Opcional, muda a cor quando passa o mouse)
                    button.OnMouseOver += (evt, elem) => button.BackgroundColor = new Color(80, 80, 140);
                    button.OnMouseOut += (evt, elem) => button.BackgroundColor = new Color(60, 60, 100);

                    button.OnLeftClick += (evt, elem) => {
                        selectedSkill = skillId;
                        descriptionText.SetText(skillInstance.Description);
                        SoundEngine.PlaySound(SoundID.MenuTick);
                    };

                    skillList.Add(button);
                }
            }
        }

        private void CreateSlotButton(string label, float top, int slotNum)
        {
            UIPanel slotBtn = new UIPanel();
            slotBtn.Width.Set(320, 0);
            slotBtn.Height.Set(60, 0);
            slotBtn.Left.Set(335, 0); 
            slotBtn.Top.Set(top, 0);
            slotBtn.BackgroundColor = Color.DarkSlateBlue;

            UIText slotText = new UIText(label);
            slotText.HAlign = 0.5f;
            slotText.VAlign = 0.5f;
            slotBtn.Append(slotText);

            slotBtn.OnLeftClick += (evt, elem) => {
                if (selectedSkill == QuirkSkills.None) {
                    Main.NewText("Select a skill first!", Color.Red);
                    return;
                }

                var player = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
                var skillInstance = SkillLibrary.GetSkill(selectedSkill); // Puxa da Library
                
                if (slotNum == 1) player.Slot1 = selectedSkill;
                if (slotNum == 2) player.Slot2 = selectedSkill;
                if (slotNum == 3) player.Slot3 = selectedSkill;
                if (slotNum == 4) player.Slot4 = selectedSkill;

                
                Main.NewText($"Assigned {skillInstance.Name} to Slot {slotNum}!", Color.Green);
                slotText.SetText($"{label}: {skillInstance.Name}");
                
                // Atualiza o texto do botão para mostrar o que está equipado
                SoundEngine.PlaySound(SoundID.MenuOpen);
            };

            mainPanel.Append(slotBtn);

            // red X to close
            UIText closeButton = new UIText("X", 1.2f); 
            closeButton.HAlign = 0.98f; 
            closeButton.Top.Set(10f, 0f);
            closeButton.TextColor = Color.LightGray;
            
            
            closeButton.OnMouseOver += (evt, elem) => closeButton.TextColor = Color.Red;
            closeButton.OnMouseOut += (evt, elem) => closeButton.TextColor = Color.LightGray;
            
            
            closeButton.OnLeftClick += (evt, elem) => {
                SoundEngine.PlaySound(SoundID.MenuClose);
                UISystem.HideUI();
            };
            mainPanel.Append(closeButton);
        }
    }
}