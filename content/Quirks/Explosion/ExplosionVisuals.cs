using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Humanizer;
using MyHeroMod.content.Dusts;

namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer
    {
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            

            if (IsClusterActive)
{
    
    Lighting.AddLight(Player.Center, Color.Orange.ToVector3() * 0.8f);

    
    Vector2 randomPos = Player.Center + Main.rand.NextVector2Circular(20f, 20f);
    if (Main.rand.NextBool(25)) 
    { 
        int dust = Dust.NewDust(
        randomPos, 
        0, 0, 
        ModContent.DustType<ClusterDust>(),
        0f, 0f, 
        0, default, 1.5f
    );
        Main.dust[dust].noGravity = true;
        Main.dust[dust].velocity = Player.velocity;        
                
            }
                
        }
    }
    }}