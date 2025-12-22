using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace MyHeroMod
{
    public class UISystem : ModSystem
    {
        internal UserInterface MyInterface;
        internal QuirkSelectionUI MyQuirkUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();
                MyQuirkUI = new QuirkSelectionUI();
                MyQuirkUI.Activate();
            }
        }
        public override void Unload()
        {
                MyInterface = null;
                MyQuirkUI = null;
            
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
        public override void UpdateUI(GameTime gameTime)
        {
            if (MyInterface?.CurrentState != null) {
                MyInterface.Update(gameTime);
            }
        }
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyHeroMod: QuirkSelection",
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