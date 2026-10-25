using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

using MyHeroMod.content.Quirks.OpticBlast;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.UI.AllForOneUI;
using MyHeroMod.content.UI.ErasureUI;
using MyHeroMod.content.UI.OverclockUI;
using MyHeroMod.content.UI.TemperatureUI;
using MyHeroMod.content.UI.EngineUI;
using MyHeroMod.content.UI.FierceWingsUI;
using MyHeroMod.content.UI.FlightUI;
using MyHeroMod.content.UI.FullCowlUI;
using MyHeroMod.content.UI.ZeroGravityUI;
using MyHeroMod.content.UI.OpticUI;
using MyHeroMod.content.UI.RedRiotUI;
using MyHeroMod.content.UI.ExplosionUI;

namespace MyHeroMod.content.UI
{
    public class UISystem : ModSystem
    {
       
        internal UserInterface MyInterface;
        internal QuirkSelectionUI MyQuirkUI;
        internal AllForOneQuirksUI SeeQuirkUI;
        internal QuirkRemoverUI MyQuirkRemoverUI;

       
        private class HudBar
        {
            public UserInterface Interface;
            public UIState State;
            public string LayerName;

            public HudBar(UIState state, string layerName)
            {
                State = state;
                State.Activate();
                Interface = new UserInterface();
                Interface.SetState(State);
                LayerName = layerName;
            }
        }

        private List<HudBar> activeHudBars;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                // Initialize Menus
                MyInterface = new UserInterface();
                MyQuirkUI = new QuirkSelectionUI();

                SeeQuirkUI = new AllForOneQuirksUI();
                SeeQuirkUI.Activate();

                MyQuirkRemoverUI = new QuirkRemoverUI();
                MyQuirkRemoverUI.Activate();

              
                activeHudBars = new List<HudBar>
                {
                    new HudBar(new BreathUIState(), "MyHeroMod: Breath Bar"),
                    new HudBar(new TemperatureUIState(), "MyHeroMod: Temperature Bar"),
                    new HudBar(new BlinkUIState(), "MyHeroMod: Blink Bar"),
                    new HudBar(new FlightShieldUIState(), "MyHeroMod: Flight Shield Bar"),
                    new HudBar(new EngineGearUIState(), "MyHeroMod: Engine Gear Bar"),
                    new HudBar(new OpticChargeUIState(), "MyHeroMod: Optic Blast Bar"),
                    new HudBar(new SweatUIState(), "MyHeroMod: Sweat Bar"),
                    new HudBar(new NauseaUIState(), "MyHeroMod: Nausea Bar"),
                    new HudBar(new FullCowlingUIState(), "MyHeroMod: Full Cowling Bar"),
                    new HudBar(new RedRiotShieldUIState(), "MyHeroMod: Red Riot Bar"),
                    new HudBar(new FierceUIState(), "MyHeroMod: Fierce Wings Bar")
                };

                KhacesCore.Content.System.CoreUISystem.RegisterTab(
                    "Skill Menu",
                    () => new SkillMenuTabContent()
                );
            }
        }

        public override void Unload()
        {
            MyInterface = null;
            MyQuirkUI = null;
            SeeQuirkUI = null;
            MyQuirkRemoverUI = null;

           
            if (activeHudBars != null)
            {
                activeHudBars.Clear();
                activeHudBars = null;
            }
        }

       
        public static void ShowUI()
        {
            var system = ModContent.GetInstance<UISystem>();
            system.MyInterface?.SetState(system.MyQuirkUI);
        }

        public static void ShowSeeQuirksUI()
        {
            var system = ModContent.GetInstance<UISystem>();
            system.SeeQuirkUI.PopulateSkillList();
            system.MyInterface?.SetState(system.SeeQuirkUI);
        }

        public static void ShowQuirkRemoverUI()
        {
            var system = ModContent.GetInstance<UISystem>();
            system.MyInterface?.SetState(system.MyQuirkRemoverUI);
        }

        public static void HideUI()
        {
            var system = ModContent.GetInstance<UISystem>();
            system.MyInterface?.SetState(null);
        }

        public static bool IsUiOpen()
        {
            var system = ModContent.GetInstance<UISystem>();
            return system.MyInterface?.CurrentState != null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (MyInterface?.CurrentState != null)
            {
                if (Terraria.GameInput.PlayerInput.Triggers.JustPressed.Inventory)
                {
                    HideUI();
                    Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.MenuClose);
                }
                else
                {
                    MyInterface.Update(gameTime);
                }
            }

            
            if (activeHudBars != null)
            {
                foreach (var hud in activeHudBars)
                {
                    hud.Interface?.Update(gameTime);
                }
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyHeroMod: Interfaces",
                    delegate
                    {
                        if (MyInterface?.CurrentState != null)
                        {
                            MyInterface.Draw(Main.spriteBatch, Main.gameTimeCache);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

            
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1 && activeHudBars != null)
            {
                foreach (var hud in activeHudBars)
                {
                    layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                        hud.LayerName,
                        delegate
                        {
                            hud.Interface?.Draw(Main.spriteBatch, Main.gameTimeCache);
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }
    }
}