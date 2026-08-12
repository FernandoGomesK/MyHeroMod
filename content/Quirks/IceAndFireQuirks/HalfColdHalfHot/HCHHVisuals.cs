using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot
{
    public partial class HalfColdHalfHotPlayer : ModPlayer, IQuirkResetter
    {
        
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (IsFlashFireFistActive)
            {
            
                drawInfo.colorArmorBody = Color.Orange;
                drawInfo.colorArmorHead = Color.Orange;
                drawInfo.colorArmorLegs = Color.Orange;
            }
        }

        
       public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (IsFlashFireFistActive)
            {
                drawInfo.colorArmorBody = Color.Orange;
                drawInfo.colorArmorHead = Color.Orange;
                drawInfo.colorArmorLegs = Color.Orange;
                
                Lighting.AddLight(Player.Center, Color.Cyan.ToVector3() * 0.8f);
                
                
            }

           if (IsPhosphorActive)
            {
               
                if (!IsFlashFireFistActive)
                {
                    DrawReducedPhosphorAura();
                }
                DrawPhosphorFire();
            }
        }

        
        private void DrawReducedPhosphorAura()
        {
          
            Lighting.AddLight(Player.Center, new Vector3(0.2f, 0.4f, 0.8f)); 

            
            if (Main.rand.NextBool(2)) 
            {
               
                int blueFire = Dust.NewDust(Player.position - new Vector2(4, 4), Player.width + 8, Player.height + 8, DustID.Torch, 0f, 0f, 150, default, 1.5f);
                Main.dust[blueFire].noGravity = true;
                Main.dust[blueFire].velocity.Y -= Main.rand.NextFloat(0.5f, 1.5f);
                Main.dust[blueFire].velocity.X *= 0.2f;
                Main.dust[blueFire].velocity += Player.velocity * 0.4f; 
            }

            
            if (Main.rand.NextBool(4)) 
            {
                int whiteFire = Dust.NewDust(Player.position, Player.width, Player.height, DustID.RedTorch, 0f, 0f, 100, default, 1.2f);
                Main.dust[whiteFire].noGravity = true;
                Main.dust[whiteFire].velocity.Y -= Main.rand.NextFloat(1f, 2f); 
                Main.dust[whiteFire].velocity += Player.velocity * 0.5f;
            }
        }

        
        private void DrawPhosphorFire()
        {
            float tamanhoX = 15f; 
            Vector2 chestCenter = Player.Center + new Vector2(0, 5f); 
            
            
            int density = IsFlashFireFistActive ? 6 : 3; 
            
            for (int k = 0; k < density; k++)
            {
                float progressoRandom = Main.rand.NextFloat(-1f, 1f);

                Vector2 pos1 = chestCenter + new Vector2(progressoRandom * tamanhoX, progressoRandom * tamanhoX);
                SpawnFireDust(pos1, chestCenter.X);

                Vector2 pos2 = chestCenter + new Vector2(progressoRandom * tamanhoX, -progressoRandom * tamanhoX);
                SpawnFireDust(pos2, chestCenter.X);
            }
        }

        private void SpawnFireDust(Vector2 position, float centerX)
        {
            
            int dustID = (position.X < centerX) ? DustID.IceTorch : DustID.Torch;

            
            int d = Dust.NewDust(position - new Vector2(2,2), 4, 4, dustID, 0, 0, 100, default, 1.3f);
            
            Main.dust[d].noGravity = true; 

            
            Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-2f, -1f));

            
            Main.dust[d].velocity += Player.velocity * 0.3f;
        } 

        private void UpdateFlyingDust()
        {
            bool isFlying = (Player.velocity.Y != 0) && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active;
            
            if (isFlying)
            {
                if (Main.rand.NextBool(2)) 
                {
                    int dustFire = Dust.NewDust(Player.position + new Vector2(-5, Player.height - 10), Player.width / 2, 10, DustID.Torch, 0, 2f, 100, default, 1.5f);
                    Main.dust[dustFire].noGravity = true;
                    Main.dust[dustFire].velocity *= 0.5f; 
                }
                
                if (Main.rand.NextBool(2))
                {
                    int dustIce = Dust.NewDust(Player.position + new Vector2(Player.width / 2, Player.height - 10), Player.width / 2, 10, DustID.IceTorch, 0, 2f, 100, default, 1.5f);
                    Main.dust[dustIce].noGravity = true;
                    Main.dust[dustIce].velocity *= 0.5f;
                }
            }
        }
            }
        
            
    }
