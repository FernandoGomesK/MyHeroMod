using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.Buffs;


namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer: ModPlayer
    {
        private void HandleFullCowlingEffects()
        {
            


            Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
            ElectricSoundTimer++;

            if (ElectricSoundTimer >= 900 + Main.rand.Next(-120, 120))
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingAura") with { Volume = 0.2f }, Player.position);
                ElectricSoundTimer = 0;
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, default, 0.5f);
            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
        if (Player.HasBuff(ModContent.BuffType<FaJinBuff>()))
        {
            drawInfo.colorArmorLegs = Color.Red;
        }
        if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
        {
        // This adds a blue tint/glow to the character sprite itself
        drawInfo.colorArmorBody = Color.RoyalBlue;
        drawInfo.colorArmorHead = Color.RoyalBlue;
        drawInfo.colorArmorLegs = Color.RoyalBlue;
        
        
        Player.armorEffectDrawShadow = true; 
        
        }
        // if (FaJinStored)
        //     {
                
        //     }
        }
    }
}