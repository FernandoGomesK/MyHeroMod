using Terraria.ModLoader;

namespace MyHeroMod.content
{
    public class KeybindSystem : ModSystem
    {
        // Variável estática para acessarmos a tecla em outros arquivos
        public static ModKeybind TransformKey { get; private set; }

        public override void Load() {
            // "Transformar" é o nome que aparecerá nos controles do Terraria
            TransformKey = KeybindLoader.RegisterKeybind(Mod, "Transformar", "G");
        }

        public override void Unload() {
            TransformKey = null;
        }
    }
}