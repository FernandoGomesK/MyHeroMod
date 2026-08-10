using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MyHeroMod.content.System
{
    public class MyHeroConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("VisualEffects")] 
        [DefaultValue(true)] 
        public bool EnableImpactFrames { get; set; } 
    }
}