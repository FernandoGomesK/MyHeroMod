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
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                return;
            }
            else
            {
               float tamanhoX = 10f; 
          
            Vector2 chestCenter = Player.Center + new Vector2(0, 5f); 

            
            int densidade = 3; 
            for (int k = 0; k < densidade; k++)
            {
                
                float progressoRandom = Main.rand.NextFloat(-1f, 1f);

                
                Vector2 pos1 = chestCenter + new Vector2(progressoRandom * tamanhoX, progressoRandom * tamanhoX);
                SpawnFireDust(pos1, chestCenter.X);

              
                Vector2 pos2 = chestCenter + new Vector2(progressoRandom * tamanhoX, -progressoRandom * tamanhoX);
                SpawnFireDust(pos2, chestCenter.X);
            }
            }}

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
