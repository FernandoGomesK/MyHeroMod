using MyHeroMod.content.UI;
using Terraria.ModLoader;

namespace MyHeroMod.content
{
    public class KeybindSystem : ModSystem
    {
        
    
        
        public static ModKeybind SkillSlot1 { get; private set; }
        public static ModKeybind SkillSlot2 { get; private set; }
        public static ModKeybind SkillSlot3 { get; private set; }
        public static ModKeybind SkillSlot4 { get; private set; }
        public static ModKeybind SkillMenu { get; private set; }

        public override void Load() {
            // what appears on terraria keys
            
            SkillSlot1 = KeybindLoader.RegisterKeybind(Mod, "Skill Slot 1", "Z");
            SkillSlot2 = KeybindLoader.RegisterKeybind(Mod, "Skill Slot 2", "X");
            SkillSlot3 = KeybindLoader.RegisterKeybind(Mod, "Skill Slot 3", "C");
            SkillSlot4 = KeybindLoader.RegisterKeybind(Mod, "Skill Slot 4", "V");
            SkillMenu = KeybindLoader.RegisterKeybind(Mod, "Open Skill Menu", "K");
        }

        public override void Unload() {
            
            SkillSlot1 = null;
            SkillSlot2 = null;
            SkillSlot3 = null;
            SkillSlot4 = null;
            SkillMenu = null;
        }
    }
}