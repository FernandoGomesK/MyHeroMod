using MyHeroMod.content.UI;
using Terraria.ModLoader;

namespace MyHeroMod.content
{
    public class KeybindSystem : ModSystem
    {
        
    
        public static ModKeybind TransformKey { get; private set; }
        public static ModKeybind SkillSlot1 { get; private set; }
        public static ModKeybind SkillSlot2 { get; private set; }
        public static ModKeybind SkillSlot3 { get; private set; }
        public static ModKeybind SkillMenu { get; private set; }

        public override void Load() {
            // "Transformar" é o nome que aparecerá nos controles do Terraria
            TransformKey = KeybindLoader.RegisterKeybind(Mod, "Transformar", "G");
            SkillSlot1 = KeybindLoader.RegisterKeybind(Mod, "Skill Slot 1", "Z");
            SkillSlot2 = KeybindLoader.RegisterKeybind(Mod, "Skill Slot 2", "X");
            SkillSlot3 = KeybindLoader.RegisterKeybind(Mod, "Skill Slot 3", "C");
            SkillMenu = KeybindLoader.RegisterKeybind(Mod, "Open Skill Menu", "K");
        }

        public override void Unload() {
            TransformKey = null;
            SkillSlot1 = null;
            SkillSlot2 = null;
            SkillSlot3 = null;
            SkillMenu = null;
        }
    }
}