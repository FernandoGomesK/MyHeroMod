using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;


namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer
    {
        private void HandleFullCowlingEffects()
        {
            Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
            ElectricSoundTimer++;

            if (ElectricSoundTimer >= 900 + Main.rand.Next(-120, 120))
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingAura"), Player.position);
                ElectricSoundTimer = 0;
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, default, 0.5f);
            }
        }
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
        Player.armorEffectDrawOutlines = true; // This forces a faint outline
        }
        }
    }
}