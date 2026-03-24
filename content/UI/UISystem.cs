using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

using MyHeroMod.content;
using MyHeroMod.content.UI;

namespace MyHeroMod
{
    public class UISystem : ModSystem
    {
        internal UserInterface MyInterface;
        internal QuirkSelectionUI MyQuirkUI;
        internal SkillMenuUI MySkillMenuUI;
        internal AllForOneQuirksUI SeeQuirkUI;

        private UserInterface breathUserInterface;
        internal BreathUIState breathUIState;

        public override void Load()
        {
            if (!Main.dedServ) // Só cria interfaces se for um jogador (tela ligada)
            {
                MyInterface = new UserInterface();
                MyQuirkUI = new QuirkSelectionUI();
                MySkillMenuUI = new SkillMenuUI();
                
                SeeQuirkUI = new AllForOneQuirksUI();
                SeeQuirkUI.Activate();

                breathUIState = new BreathUIState();
                breathUIState.Activate();
                breathUserInterface = new UserInterface();
                breathUserInterface.SetState(breathUIState);
            }
        }
        
        public override void Unload()
        {
            MyInterface = null;
            MyQuirkUI = null;
            MySkillMenuUI = null;
            SeeQuirkUI = null;
            
            breathUIState = null;
            breathUserInterface = null;
        }

        public static void ToggleSkillMenu()
        {
            var system = ModContent.GetInstance<UISystem>();
            if (system.MyInterface.CurrentState is SkillMenuUI)
            {
                system.MyInterface.SetState(null); // Fecha se já estiver aberto
            }
            else
            {
                system.MyInterface.SetState(system.MySkillMenuUI); // Abre
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
                // if the player presses esc the inventory closes
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
            
            if (breathUserInterface != null)
                breathUserInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "MyHeroMod: Breath Bar",
                    delegate
                    {
                        // DEFESA: Só desenha se a interface foi realmente criada!
                        if (breathUserInterface != null)
                        {
                            breathUserInterface.Draw(Main.spriteBatch, Main.gameTimeCache);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

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
        }
    }
}