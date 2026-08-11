using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame
{
    public partial class BlueflamePlayer : ModPlayer
    {
        // MODIFY DRAW INFO: Usado para mudar a cor do SPRITE (Armadura/Pele)
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (IsFlashFireFistActive)
            {
                // Deixa o personagem incandescente (Azul)
                drawInfo.colorArmorBody = Color.Blue;
                drawInfo.colorArmorHead = Color.Blue;
                drawInfo.colorArmorLegs = Color.Blue;
            }
        }

        
       public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (IsFlashFireFistActive)
            {
                drawInfo.colorArmorBody = Color.Blue;
                drawInfo.colorArmorHead = Color.Blue;
                drawInfo.colorArmorLegs = Color.Blue;
                
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
               
                int blueFire = Dust.NewDust(Player.position - new Vector2(4, 4), Player.width + 8, Player.height + 8, DustID.BlueTorch, 0f, 0f, 150, default, 1.5f);
                Main.dust[blueFire].noGravity = true;
                Main.dust[blueFire].velocity.Y -= Main.rand.NextFloat(0.5f, 1.5f);
                Main.dust[blueFire].velocity.X *= 0.2f;
                Main.dust[blueFire].velocity += Player.velocity * 0.4f; 
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
            int dustID = DustID.PurpleTorch;
            
           
            float scale = IsFlashFireFistActive ? 1.8f : 1.5f;

            int d = Dust.NewDust(position - new Vector2(2,2), 4, 4, dustID, 0, 0, 100, default, scale); 
            Main.dust[d].noGravity = true; 
            
            
            float riseSpeed = IsFlashFireFistActive ? -0.5f : -1.5f;
            Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(riseSpeed - 0.5f, riseSpeed));
            
            Main.dust[d].velocity += Player.velocity * 0.3f;
        }
    }
}