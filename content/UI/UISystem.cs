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

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();
                MyQuirkUI = new QuirkSelectionUI();
                MySkillMenuUI = new SkillMenuUI();
                
            }
        }
        public override void Unload()
        {
                MyInterface = null;
                MyQuirkUI = null;
                MySkillMenuUI = null;
            
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
        }
    }
}