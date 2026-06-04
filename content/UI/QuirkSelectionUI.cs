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
        public UIList quirkList;
        public UIScrollbar scrollbar;

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

            scrollbar = new UIScrollbar();
            scrollbar.Height.Set(-50f, 1f);
            scrollbar.Top.Set(40f, 0f);
            scrollbar.HAlign = 1f;
            MainPanel.Append(scrollbar);

            quirkList = new UIList();
            quirkList.Width.Set(-25f, 1f);
            quirkList.Height.Set(-50f, 1f);
            quirkList.Top.Set(40f, 0f);
            quirkList.HAlign = 0f;
            quirkList.ListPadding = 5f;
            MainPanel.Append(quirkList);

            quirkList.SetScrollbar(scrollbar);

            CreateButton("One For All 9th", QuirkType.OneForAll9th, Color.LimeGreen);
            CreateButton("Engine", QuirkType.Engine, Color.Black);
            CreateButton("All For One", QuirkType.AllForOne, Color.Black);
            CreateButton("Explosion", QuirkType.Explosion, Color.OrangeRed);
            CreateButton("One For All 8th", QuirkType.OneForAll8th, Color.YellowGreen);
            CreateButton("Hell Flames", QuirkType.HellFlames, Color.Orange);
            CreateButton("Blue Flames", QuirkType.BlueFlames, Color.CornflowerBlue);
            CreateButton("Super Regeneration", QuirkType.SuperRegeneration, Color.White);
            CreateButton("Half Cold Half Hot", QuirkType.HalfColdHalfHot, Color.LightBlue);
            CreateButton("Erasure" , QuirkType.Erasure, Color.Purple);
            CreateButton("Float", QuirkType.Float, Color.LightSkyBlue);
            CreateButton("Gearshift", QuirkType.Gearshift, Color.Blue);
            CreateButton("Fa Jin", QuirkType.FaJin, Color.MediumPurple);
            CreateButton("Smoke Screen", QuirkType.SmokeScreen, Color.Gray);
            CreateButton("Danger Sense", QuirkType.DangerSense, Color.Red);
            CreateButton("Black Whip", QuirkType.BlackWhip, Color.Black);
            CreateButton("Tape", QuirkType.Tape, Color.White);
            CreateButton("Overclock", QuirkType.Overclock, Color.Yellow);
            CreateButton("Flight", QuirkType.Flight, Color.White);
            CreateButton("Slide And Glide", QuirkType.SlideAndGlide, Color.Green);
            CreateButton("Decay", QuirkType.Decay, Color.Black);
            CreateButton("Rivet", QuirkType.Rivet, Color.Red);
            CreateButton("Spring Like Limbs", QuirkType.SpringLikeLimbs, Color.LightGreen);
            CreateButton("Overhaul", QuirkType.Overhaul, Color.LightPink);
            CreateButton("Zero Gravity", QuirkType.ZeroGravity, Color.Pink);
            CreateButton("Fierce Wings", QuirkType.FierceWings, Color.Red);
            CreateButton("Optic Blast", QuirkType.OpticBlast, Color.Red);


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
            MainPanel.Append(closeButton);

        }
        private void CreateButton(string text, QuirkType quirk, Color color)
        {
            UIPanel button = new UIPanel();
            button.Width.Set(0f, 1f);
            button.Height.Set(40f, 0f);
            button.BackgroundColor = color * 0.7f;

            button.OnLeftClick += (evt, element) =>
            {

                Player player = Main.LocalPlayer;
                var transPlayer = player.GetModPlayer<TransformationPlayer>();

                transPlayer.CompleteReset();
                transPlayer.ResetSlot();

                transPlayer.ActiveQuirks.Clear();
                transPlayer.ActiveQuirks.Add(quirk);

                transPlayer.UpdateUnlockedSkills();

                if (Main.netMode == NetmodeID.MultiplayerClient) 
                {
                    transPlayer.SendClientChanges(transPlayer);
                }



if (quirk == QuirkType.HellFlames || quirk == QuirkType.HalfColdHalfHot )
                {
                    // Código de dar item do fogo (comentado)
                }
                else if (quirk == QuirkType.OneForAll9th)
                {
                    if (!player.HasItem(ModContent.ItemType<content.Items.Weapons.PunchAttack>()))
                    {
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<content.Items.Weapons.PunchAttack>());
                    }
                }

                Main.NewText($"You have selected the quirk: {text}", color);
                SoundEngine.PlaySound(SoundID.Item4, player.position);
                UISystem.HideUI();
            };
            UIText btnText = new UIText(text);
            btnText.HAlign = 0.5f;
            btnText.VAlign = 0.5f;
            button.Append(btnText);
            quirkList.Add(button);
        }

       
    }
}