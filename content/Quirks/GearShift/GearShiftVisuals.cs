using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;


namespace MyHeroMod.content.Quirks.Gearshift
{
    public partial class GearshiftPlayer : ModPlayer
    {
        
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
        
        if (isGearshiftActive)
        {
        // This adds a blue tint/glow to the character sprite itself
        drawInfo.colorArmorBody = Color.RoyalBlue;
        drawInfo.colorArmorHead = Color.RoyalBlue;
        drawInfo.colorArmorLegs = Color.RoyalBlue;
        
        // This creates a "God Mode" style afterimage trail which looks like a contour
        Player.armorEffectDrawShadow = true; 
        // Player.armorEffectDrawOutlines = true; // This forces a faint outline
        }
        
        }
    }
}