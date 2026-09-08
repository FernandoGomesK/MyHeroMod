using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MyHeroMod.content.System
{
   public class MyHeroConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("VisualEffects")] 
        
    
        [DefaultValue(false)] 
        public bool EnableImpactFrames { get; set; } 

        
        [DefaultValue(0.8f)]
        [Range(0f, 1f)]
        [Increment(0.05f)]
        [DrawTicks]
        [Slider] 
        public float ImpactFrameIntensity { get; set; } 

        public bool DecayDestroys { get; set; }
        
    }
}