using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using MyHeroMod.content.System;
using Terraria.ID;
using MyHeroMod.content;
using Terraria.Audio;
using MyHeroMod.content.Quirks.AllForOne;

namespace MyHeroMod
{
    public class AllForOneQuirksUI : UIState
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

            UIText title = new UIText("Stolen Quirks", 1f);
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
            scrollbar.HAlign = 1f; // Gruda na direita
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

       public void PopulateSkillList()
        {
            quirkList.Clear(); // Limpa a lista para não duplicar

            if (Main.LocalPlayer == null || !Main.LocalPlayer.active) return;
            var afoPlayer = Main.LocalPlayer.GetModPlayer<AllForOnePlayer>();

            // Se o AFO não tiver roubado nada ainda
            if (afoPlayer.InternalQuirks.Count == 0)
            {
                UIText emptyText = new UIText("No Quirks Stolen Yet...", 0.9f);
                emptyText.TextColor = Color.Gray;
                emptyText.HAlign = 0.5f;
                quirkList.Add(emptyText);
                return;
            }
foreach (QuirkType quirk in afoPlayer.InternalQuirks)
            {
                // 1. Cria uma "caixinha" para a Quirk
                UIPanel quirkItemPanel = new UIPanel();
                quirkItemPanel.Width.Set(0, 1f);
                quirkItemPanel.Height.Set(40, 0);
                quirkItemPanel.BackgroundColor = new Color(50, 50, 70);

                // 2. Coloca o nome da Quirk na caixinha
                UIText quirkText = new UIText(quirk.ToString());
                quirkText.VAlign = 0.5f;
                quirkText.Left.Set(10, 0);
                quirkItemPanel.Append(quirkText);

                // 3. Cria o botão de Excluir/Extrair
                UIText extractButton = new UIText("[Remove]", 0.8f);
                extractButton.VAlign = 0.5f;
                extractButton.HAlign = 0.98f; // Canto direito da caixinha
                extractButton.TextColor = Color.Salmon;

                extractButton.OnMouseOver += (evt, elem) => extractButton.TextColor = Color.Red;
                extractButton.OnMouseOut += (evt, elem) => extractButton.TextColor = Color.Salmon;

                extractButton.OnLeftClick += (evt, elem) => {
                    SoundEngine.PlaySound(SoundID.NPCDeath11); // Um som de extração
                    
                    
                    afoPlayer.InternalQuirks.Remove(quirk); 
                    
                    
                    PopulateSkillList(); 
                };

                quirkItemPanel.Append(extractButton);

                // 4. Adiciona a caixinha completa na Lista Principal
                quirkList.Add(quirkItemPanel);
            }
        }
    }
}