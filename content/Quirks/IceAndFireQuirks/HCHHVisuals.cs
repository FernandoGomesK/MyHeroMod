using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.HalfColdHalfHot
{
    public partial class HalfColdHalfHotPlayer : ModPlayer, IQuirkResetter
    {
        
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            // if (IsFlashFireFistActive)
            // {
            
            //     drawInfo.colorArmorBody = Color.OrangeRed;
            //     drawInfo.colorArmorHead = Color.OrangeRed;
            //     drawInfo.colorArmorLegs = Color.OrangeRed;
            // }
        }

        
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            
            
            
            if (IsPhosphorActive)
            {
                DrawPhosphorFire();
            }
        }

    

        private void DrawPhosphorFire()
        {
        
            float tamanhoX = 10f; 
            // moves to the chest area
            Vector2 chestCenter = Player.Center + new Vector2(0, 5f); 

            // 2. Densidade do Fogo
           //  Generates the flames
            int densidade = 3; 
            for (int k = 0; k < densidade; k++)
            {
                // adds variatioin to the flames
                float progressoRandom = Main.rand.NextFloat(-1f, 1f);

                //  Diagonal 1 
                Vector2 pos1 = chestCenter + new Vector2(progressoRandom * tamanhoX, progressoRandom * tamanhoX);
                SpawnFireDust(pos1, chestCenter.X);

                // Diagonal 2
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
    }
}