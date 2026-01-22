using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.HellFlames
{
    public partial class HellFlamesPlayer : ModPlayer
    {
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (IsFlashFireFistActive)
            {
                // Add a fiery glow effect to the player when Flash Fire Fist is active
                drawInfo.colorArmorBody = Color.OrangeRed;
                drawInfo.colorArmorHead = Color.OrangeRed;
                drawInfo.colorArmorLegs = Color.OrangeRed;

                // Create a light effect around the player
                Lighting.AddLight(Player.Center, Color.OrangeRed.ToVector3() * 0.8f);
                
                    int fire = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[fire].noGravity = true;
                    Main.dust[fire].velocity *= 3f;
                    Main.dust[fire].velocity += Player.velocity * 0.5f;
                
                // Main.dust[Player.whoAmI].noGravity = true;
                // if (Main.rand.NextBool(3))
                // {
                //     Dust.NewDust(Player.position, Player.width, Player.height, DustID.Fire, 0f, 0f, 100, default, 1.5f);
                // }
                
            }

            if (Main.rand.NextBool(5)){
                
            
            if (CurrentHeat >= MaxHeat)
            {
                int steam = Dust.NewDust(Player.position, Player.width, Player.height, DustID.SteampunkSteam, 0f, 0f, 100, Color.White, 1f);
                Main.dust[steam].noGravity = false;
                Main.dust[steam].velocity *= 1f;
                Main.dust[steam].velocity += Player.velocity * 0.5f;
            }
            }

        }
    }
}