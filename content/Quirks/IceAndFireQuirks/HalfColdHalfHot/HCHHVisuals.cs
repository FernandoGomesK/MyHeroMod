using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot
{
    
    public partial class HalfColdHalfHotPlayer : BaseIceAndFirePlayer
    {
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (IsFlashFireFistActive)
            {
                drawInfo.colorArmorBody = Color.OrangeRed;
                drawInfo.colorArmorHead = Color.OrangeRed;
                drawInfo.colorArmorLegs = Color.OrangeRed;
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (IsFlashFireFistActive)
            {
                drawInfo.colorArmorBody = Color.OrangeRed;
                drawInfo.colorArmorHead = Color.OrangeRed;
                drawInfo.colorArmorLegs = Color.OrangeRed;
                Lighting.AddLight(Player.Center, Color.OrangeRed.ToVector3() * 0.8f); 
            }

            if (IsPhosphorActive)
            {
                if (!IsFlashFireFistActive) DrawReducedPhosphorAura();
                DrawPhosphorFire();
            }
        }

        private void DrawReducedPhosphorAura()
        {
           
            Lighting.AddLight(Player.Center, new Vector3(0.8f, 0.4f, 0.8f)); 

            
            if (Main.rand.NextBool(2)) 
            {
                int redFire = Dust.NewDust(Player.position - new Vector2(4, 4), Player.width + 8, Player.height + 8, DustID.Torch, 0f, 0f, 150, default, 1.5f);
                Main.dust[redFire].noGravity = true;
                Main.dust[redFire].velocity.Y -= Main.rand.NextFloat(0.5f, 1.5f);
                Main.dust[redFire].velocity.X *= 0.2f;
                Main.dust[redFire].velocity += Player.velocity * 0.4f; 
            }

            
            if (Main.rand.NextBool(4)) 
            {
                int whiteFire = Dust.NewDust(Player.position, Player.width, Player.height, DustID.IceTorch, 0f, 0f, 100, default, 1.2f);
                Main.dust[whiteFire].noGravity = true;
                Main.dust[whiteFire].velocity.Y -= Main.rand.NextFloat(1f, 2f); 
                Main.dust[whiteFire].velocity += Player.velocity * 0.5f;
            }
        }

        private void DrawPhosphorFire()
        {
            float sizeX = 10f; 
            
            Vector2 chestCenter = Player.Center + new Vector2(0, 5f); 

          
            int density = IsFlashFireFistActive ? 6 : 3; 
            
            for (int k = 0; k < density; k++)
            {
                
                float randomProgress = Main.rand.NextFloat(-1f, 1f);

                Vector2 pos1 = chestCenter + new Vector2(randomProgress * sizeX, randomProgress * sizeX);
                SpawnFireDust(pos1, chestCenter.X);

                
                Vector2 pos2 = chestCenter + new Vector2(randomProgress * sizeX, -randomProgress * sizeX);
                SpawnFireDust(pos2, chestCenter.X);
            }
        }

        private void SpawnFireDust(Vector2 position, float centerX)
        {
            
            int dustID = (position.X < centerX) ? DustID.IceTorch : DustID.Torch;
          
            float scale = IsFlashFireFistActive ? 1.6f : 1.3f;

            int d = Dust.NewDust(position - new Vector2(2, 2), 4, 4, dustID, 0, 0, 100, default, scale);
            Main.dust[d].noGravity = true; 

        
            float riseSpeed = IsFlashFireFistActive ? -1.5f : -2f;
            Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(riseSpeed, riseSpeed + 1f));
            
            Main.dust[d].velocity += Player.velocity * 0.3f;
        }
    }
}