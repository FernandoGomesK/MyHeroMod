using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Humanizer;

namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer
    {
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            

            if (IsClusterActive)
            {
                // Add a fiery glow effect to the player when Flash Fire Fist is active
                drawInfo.colorArmorBody = Color.Orange;
                drawInfo.colorArmorHead = Color.Orange;
                drawInfo.colorArmorLegs = Color.Orange;

                // Create a light effect around the player
                Lighting.AddLight(Player.Center, Color.Orange.ToVector3() * 0.8f);

                if (Main.rand.NextBool(5)){
                
                    int fire = Dust.NewDust(Player.position, Player.width, Player.height, DustID.YellowStarDust, 0f, 0f, 100, Color.Orange, 1.5f);
                    Main.dust[fire].noGravity = true;
                    Main.dust[fire].velocity *= 1.5f;
                    Main.dust[fire].velocity += Player.velocity * 0.5f;
                }
                
                
            }
                
        }
    }
}