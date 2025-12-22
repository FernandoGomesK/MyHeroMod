using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using Terraria.Audio;

namespace MyHeroMod
{
    public class QuirkSelectionUI : UIState
    {
        public UIPanel MainPanel;

        public override void OnInitialize()
        {
            MainPanel = new UIPanel();
            MainPanel.Width.Set(400f, 0f);
            MainPanel.Height.Set(300f, 0f);
            MainPanel.HAlign = 0.5f;
            MainPanel.VAlign = 0.5f;
            MainPanel.BackgroundColor = new Color(73, 94, 171);
            Append(MainPanel);

            UIText title = new UIText("Select Your Quirk");
            title.HAlign = 0.5f;
            title.Top.Set(10f, 0f);
            MainPanel.Append(title);

            CreateButton("One For All 9th", 60f, QuirkType.OneForAll9th, Color.LimeGreen);
        }
        private void CreateButton(string text, float top, QuirkType quirk, Color color)
        {
            UIPanel button = new UIPanel();
            button.Width.Set(200f, 0f);
            button.Height.Set(40f, 0f);
            button.Left.Set(30f, 0f);
            button.Top.Set(top, 0f);
            button.BackgroundColor = color * 0.7f;

            button.OnLeftClick += (evt, element) =>
            {
                Player player = Main.LocalPlayer;
                var modPlayer = player.GetModPlayer<TransformationPlayer>();
                modPlayer.SelectedQuirk = quirk;
                modPlayer.CurrentStage = QuirkStage.Initial;

                Main.NewText($"You have selected the quirk: {text}", color);
                SoundEngine.PlaySound(SoundID.Item4, player.position);
                UISystem.HideUI();
            };
            UIText btnText = new UIText(text);
            btnText.HAlign = 0.5f;
            btnText.VAlign = 0.5f;
            button.Append(btnText);
            MainPanel.Append(button);
        }
    }
}