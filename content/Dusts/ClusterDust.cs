using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Dusts
{
    public class ClusterDust: ModDust
    {

        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true; 
            dust.noLight = false;   
            dust.rotation = 0f;    
            dust.alpha = 0;
            dust.frame = new Rectangle(0, 0, 6, 6);
        }
        public override bool Update(Dust dust)
{
    

    dust.velocity *= 0.1f; 
    
    
    dust.position += dust.velocity;

    
    dust.rotation = 0f; 

    

    dust.scale -= 0.05f; 
            
            
            if (dust.scale < 0.1f)
            {
                dust.active = false;
            }
    
    
   dust.alpha += 5;
    if (dust.alpha >= 255) dust.active = false;

   
    if (dust.alpha < 150) 
    {
        
        Lighting.AddLight(dust.position, 2.0f, 1.0f, 0.2f); 
    }

    
    return false; 
}
public override Color? GetAlpha(Dust dust, Color lightColor)
{
    
    return new Color(255, 255, 255, 255 - dust.alpha);
}
}
}

