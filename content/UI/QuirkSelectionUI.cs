using KhacesCore.Content.System;
using Microsoft.Xna.Framework;
using MyHeroMod.content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace MyHeroMod.content.UI
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
            CreateButton("Engine (Tenya)", QuirkType.Engine, Color.Black, QuirkVariant.Default);
            CreateButton("Engine (Tensei)", QuirkType.Engine, Color.DarkSlateGray, QuirkVariant.Variant1);
            CreateButton("All For One", QuirkType.AllForOne, Color.Black);
            CreateButton("Explosion", QuirkType.Explosion, Color.OrangeRed);
            CreateButton("One For All 8th", QuirkType.OneForAll8th, Color.YellowGreen);
            CreateButton("Hell Flames", QuirkType.HellFlames, Color.Orange);
            CreateButton("Blue Flames", QuirkType.Blueflame, Color.CornflowerBlue);
            CreateButton("Super Regeneration", QuirkType.SuperRegeneration, Color.White);
            CreateButton("Half Cold Half Hot", QuirkType.HalfColdHalfHot, Color.LightBlue);
            CreateButton("Erasure", QuirkType.Erasure, Color.Purple);
            CreateButton("Float", QuirkType.Float, Color.LightSkyBlue);
            CreateButton("Gearshift", QuirkType.Gearshift, Color.Blue);
            CreateButton("Fa Jin", QuirkType.FaJin, Color.MediumPurple);
            CreateButton("Smoke Screen", QuirkType.SmokeScreen, Color.Gray);
            CreateButton("Danger Sense", QuirkType.DangerSense, Color.Red);
            CreateButton("Black Whip", QuirkType.BlackWhip, Color.Black);
            CreateButton("Tape", QuirkType.Tape, Color.White);
            CreateButton("Overclock", QuirkType.Overclock, Color.Yellow, QuirkVariant.Default);
            CreateButton("Overclock (Chrome)", QuirkType.Overclock, Color.Gold, QuirkVariant.Variant1);
            CreateButton("Flight", QuirkType.Flight, Color.White);
            CreateButton("Slide And Glide", QuirkType.SlideAndGlide, Color.Green);
            CreateButton("Decay", QuirkType.Decay, Color.Black);
            CreateButton("Rivet", QuirkType.Rivet, Color.Red);
            CreateButton("Spring Like Limbs", QuirkType.SpringLikeLimbs, Color.LightGreen);
            CreateButton("Overhaul", QuirkType.Overhaul, Color.LightPink);
            CreateButton("Zero Gravity", QuirkType.ZeroGravity, Color.Pink);
            CreateButton("Fierce Wings", QuirkType.FierceWings, Color.Red);
            CreateButton("Optic Blast", QuirkType.OpticBlast, Color.Red, QuirkVariant.Default);
            CreateButton("Optic Blast (Beams)", QuirkType.OpticBlast, Color.HotPink, QuirkVariant.Variant1);
            CreateButton("DarkShadow", QuirkType.DarkShadow, Color.Black);
            CreateButton("Rabbit", QuirkType.Rabbit, Color.White);
            CreateButton("Hardening", QuirkType.Hardening, Color.Red);
            CreateButton("Jet", QuirkType.Jet, Color.LightBlue);
            CreateButton("Muscle Augmentation", QuirkType.MuscleAugmentation, Color.Red);
            CreateButton("Grasshoper", QuirkType.Grasshoper, Color.Green, QuirkVariant.Default);
            CreateButton("Shin", QuirkType.Grasshoper, Color.LimeGreen, QuirkVariant.Variant1);
            CreateButton("Grasshoper (Variant 2)", QuirkType.Grasshoper, Color.YellowGreen, QuirkVariant.Variant2);

            // Red X to close
            UIText closeButton = new UIText("X", 1.2f);
            closeButton.HAlign = 0.98f;
            closeButton.Top.Set(10f, 0f);
            closeButton.TextColor = Color.LightGray;

            closeButton.OnMouseOver += (evt, elem) => closeButton.TextColor = Color.Red;
            closeButton.OnMouseOut += (evt, elem) => closeButton.TextColor = Color.LightGray;

            closeButton.OnLeftClick += (evt, elem) =>
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                UISystem.HideUI();
            };
            MainPanel.Append(closeButton);
        }

        private void CreateButton(string text, QuirkType quirk, Color color, QuirkVariant? forcedVariant = null)
        {
            UIPanel button = new UIPanel();
            button.Width.Set(0f, 1f);
            button.Height.Set(40f, 0f);
            button.BackgroundColor = color * 0.7f;

            button.OnLeftClick += (evt, element) =>
            {
                Player player = Main.LocalPlayer;
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                var corePlayer = player.GetModPlayer<CorePlayer>();

                transPlayer.CompleteReset();
                corePlayer.ResetSlots();

                transPlayer.ActiveQuirks.Clear();
                transPlayer.ActiveQuirks.Add(quirk);

                if (forcedVariant.HasValue)
                {
                    transPlayer.CurrentVariant = forcedVariant.Value;
                }
                else
                {
          
                    if (quirk == QuirkType.OpticBlast)
                        transPlayer.CurrentVariant = Main.rand.Next(100) < 80 ? QuirkVariant.Variant1 : QuirkVariant.Default;
                    else if (quirk == QuirkType.Engine)
                        transPlayer.CurrentVariant = Main.rand.Next(100) < 48 ? QuirkVariant.Variant1 : QuirkVariant.Default;
                    else
                        transPlayer.CurrentVariant = QuirkVariant.Default;
                }

                transPlayer.UpdateUnlockedSkills();

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    transPlayer.SyncPlayer(-1, Main.myPlayer, false);
                }

                if (quirk == QuirkType.OneForAll9th)
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