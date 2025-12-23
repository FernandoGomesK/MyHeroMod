using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Quirks.OFA9th;
using System.Collections.Generic;
using MyHeroMod.content;

namespace MyHeroMod.content.UI
{
    public class SkillMenuUI : UIState
    {
        private UIText title;
        private UIPanel mainPanel;
        private UIList skillList;
        private UIText descriptionText;
        private OfaSkills selectedSkill = OfaSkills.None; // Qual habilidade está selecionada agora


        public override void OnInitialize()
        {
            // Garante que os dados estão carregados
            SkillData.Load();

            // 1. Painel Principal (Fundo Azul Escuro)
            mainPanel = new UIPanel();
            mainPanel.Width.Set(600, 0);
            mainPanel.Height.Set(450, 0);
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
            listPanel.Width.Set(220, 0);
            listPanel.Height.Set(300, 0);
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
            scrollbar.Left.Set(0, 0.9f); // Canto direito do painel
            listPanel.Append(scrollbar);
            skillList.SetScrollbar(scrollbar);

            // Popula a lista com as skills

            // 3. LADO DIREITO: Slots (Z, X, C)
            CreateSlotButton("Slot 1 (Z)", 60, 1);
            CreateSlotButton("Slot 2 (X)", 140, 2);
            CreateSlotButton("Slot 3 (C)", 220, 3);

            // 4. RODAPÉ: Descrição
            UIPanel descPanel = new UIPanel();
            descPanel.Width.Set(540, 0);
            descPanel.Height.Set(70, 0);
            descPanel.Top.Set(360, 0);
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

            string quirkName = modPlayer.SelectedQuirk.ToString();

            if (modPlayer.SelectedQuirk == QuirkType.OneForAll9th)
            {
                quirkName = "One For All 9th";
            }
            else if (modPlayer.SelectedQuirk == QuirkType.Quirkless)
            {
                quirkName = "Quirkless";
            }
            string dynamicText = $"{quirkName} - Stage: {modPlayer.CurrentStage}";
            title.SetText(dynamicText);
            
            PopulateSkillList(); // Agora é seguro chamar, pois o Player existe!
        }


        private void PopulateSkillList()
        {
            skillList.Clear(); // Limpa a lista anterior para não duplicar botões
            
            // Verificação de segurança: Se não tiver player (ex: menu principal), não faz nada
            if (Main.LocalPlayer == null || !Main.LocalPlayer.active) return;

            var player = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();

            foreach (var kvp in SkillData.Skills)
            {
                OfaSkills skillType = kvp.Key;
                SkillInfo info = kvp.Value;

                // Só mostra se o jogador tiver nível suficiente
                if (player.CurrentStage >= info.MinStage)
                {
                    UIPanel button = new UIPanel();
                    button.Width.Set(180, 0);
                    button.Height.Set(40, 0);
                    button.BackgroundColor = new Color(60, 60, 100);

                    UIText text = new UIText(info.Name, 0.7f);
                    text.HAlign = 0.5f;
                    text.VAlign = 0.5f;
                    button.Append(text);

                    button.OnLeftClick += (evt, elem) => {
                        selectedSkill = skillType;
                        descriptionText.SetText(info.Description);
                        SoundEngine.PlaySound(SoundID.MenuTick);
                    };

                    skillList.Add(button);
                }
            }
        }

        private void CreateSlotButton(string label, float top, int slotNum)
        {
            UIPanel slotBtn = new UIPanel();
            slotBtn.Width.Set(250, 0);
            slotBtn.Height.Set(60, 0);
            slotBtn.Left.Set(280, 0); 
            slotBtn.Top.Set(top, 0);
            slotBtn.BackgroundColor = Color.DarkSlateBlue;

            UIText text = new UIText(label);
            text.HAlign = 0.5f;
            text.VAlign = 0.5f;
            slotBtn.Append(text);

            slotBtn.OnLeftClick += (evt, elem) => {
                if (selectedSkill == OfaSkills.None) {
                    Main.NewText("Select a skill first!", Color.Red);
                    return;
                }

                var player = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
                
                if (slotNum == 1) player.Slot1 = selectedSkill;
                if (slotNum == 2) player.Slot2 = selectedSkill;
                if (slotNum == 3) player.Slot3 = selectedSkill;

                Main.NewText($"Assigned {SkillData.Skills[selectedSkill].Name} to Slot {slotNum}!", Color.Green);
                SoundEngine.PlaySound(SoundID.MenuOpen);
                
                text.SetText($"{label}: {SkillData.Skills[selectedSkill].Name}");
            };

            mainPanel.Append(slotBtn);
        }
    }
}