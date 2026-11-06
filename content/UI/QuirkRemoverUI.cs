using KhacesCore.Content.System;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Handlers;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Handlers;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace MyHeroMod.content.UI
{
    public class QuirkRemoverUI : UIState
    {
        public UIPanel MainPanel;
        public UIList quirkList;
        public UIScrollbar scrollbar;

        public override void OnInitialize()
        {
            MainPanel = new UIPanel();
            MainPanel.Width.Set(400f, 0f);
            MainPanel.Height.Set(340f, 0f);
            MainPanel.HAlign = 0.5f;
            MainPanel.VAlign = 0.5f;
            MainPanel.BackgroundColor = new Color(30, 30, 35);
            Append(MainPanel);

            UIText title = new UIText("Current Quirks", 1f);
            title.HAlign = 0.5f;
            title.Top.Set(10f, 0f);
            MainPanel.Append(title);

            UIPanel listPanel = new UIPanel();
            listPanel.Width.Set(340, 0);
            listPanel.Height.Set(260, 0);
            listPanel.HAlign = 0.5f;
            listPanel.Top.Set(50, 0);
            listPanel.BackgroundColor = new Color(20, 20, 40);
            MainPanel.Append(listPanel);

            quirkList = new UIList();
            quirkList.Width.Set(0, 1f);
            quirkList.Height.Set(0, 1f);
            listPanel.Append(quirkList);

            scrollbar = new UIScrollbar();
            scrollbar.SetView(100f, 1000f);
            scrollbar.Height.Set(0, 1f);
            scrollbar.HAlign = 1f;
            listPanel.Append(scrollbar);
            quirkList.SetScrollbar(scrollbar);

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
            MainPanel.Append(closeButton);
        }

        public override void OnActivate()
        {
            base.OnActivate();
            PopulateSkillList();
        }

        public void PopulateSkillList()
        {
            quirkList.Clear();

            if (Main.LocalPlayer == null || !Main.LocalPlayer.active) return;
            var transPlayer = Main.LocalPlayer.GetModPlayer<TransformationPlayer>();
            var corePlayer = Main.LocalPlayer.GetModPlayer<CorePlayer>();

            if (transPlayer.ActiveQuirks.Count == 0)
            {
                UIText emptyText = new UIText("No Quirks in the Body", 0.9f);
                emptyText.TextColor = Color.Gray;
                emptyText.HAlign = 0.5f;
                quirkList.Add(emptyText);
                return;
            }

            foreach (QuirkType quirk in transPlayer.ActiveQuirks)
            {
                UIPanel quirkItemPanel = new UIPanel();
                quirkItemPanel.Width.Set(0, 1f);
                quirkItemPanel.Height.Set(40, 0);
                quirkItemPanel.BackgroundColor = new Color(50, 50, 70);

                // Obtém o nome amigável da Quirk e o nome da variante ativa
                string quirkName = QuirkHandler.GetQuirkDisplayName(quirk);
                string variantName = QuirkHandler.GetVariantDisplayName(quirk, transPlayer.CurrentVariant);

                string displayText = string.IsNullOrEmpty(variantName)
                    ? quirkName
                    : $"{quirkName} [c/00FF99:({variantName})]";

                UIText quirkText = new UIText(displayText);
                quirkText.VAlign = 0.5f;
                quirkText.Left.Set(10, 0);
                quirkItemPanel.Append(quirkText);

                UIText extractButton = new UIText("[Remove]", 0.8f);
                extractButton.VAlign = 0.5f;
                extractButton.HAlign = 0.98f;
                extractButton.TextColor = Color.Salmon;

                extractButton.OnMouseOver += (evt, elem) => extractButton.TextColor = Color.Red;
                extractButton.OnMouseOut += (evt, elem) => extractButton.TextColor = Color.Salmon;

                extractButton.OnLeftClick += (evt, elem) => {
                    SoundEngine.PlaySound(SoundID.NPCDeath11);

                    transPlayer.ActiveQuirks.Remove(quirk);
                    corePlayer.ResetSlots();
                    transPlayer.UpdateUnlockedSkills();

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        transPlayer.SendClientChanges(transPlayer);
                    }

                    PopulateSkillList();
                };

                quirkItemPanel.Append(extractButton);
                quirkList.Add(quirkItemPanel);
            }
        }
    }
}