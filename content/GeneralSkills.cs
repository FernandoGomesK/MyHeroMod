using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.GeneralSkills1
{
    

    public class GeneralSkills : ModPlayer
    {
        public void Jump(float force, float defaultForce = 10f)
        {
            
            Player.velocity.Y = -force; 
        }

        public void Dash(float force = 15f) 
        {
            
            Vector2 dashDirection = Main.MouseWorld - Player.Center;
            
            
            if (dashDirection == Vector2.Zero) return;

            dashDirection.Normalize(); 

           
            Player.ChangeDir(Main.MouseWorld.X > Player.Center.X ? 1 : -1);

            
            Player.velocity = dashDirection * force; 

           
            Player.SetImmuneTimeForAllTypes(05);
        }
    }
}
    
