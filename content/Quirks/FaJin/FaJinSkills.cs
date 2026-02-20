using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System.BasePlayer;

using Terraria.ID;

using Terraria.Audio;





namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FajinPlayer : ModPlayer
    {


        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash)
        {
            var transformationPlayer = Player.GetModPlayer<TransformationPlayer>();
            var fajinPlayer = Player.GetModPlayer<FajinPlayer>();
            if (transformationPlayer.SelectedQuirk == QuirkType.FaJin) {
                if (FaJinStored)
                {
                speed = 25f;
                isEnhanced = true;
                FaJinCharges = 0; 
                }
                else{
                ChargeFajin();
                isEnhanced = true;  
                }

    }   }
    }
    }
        


       
        // private void dashvfx()
        // {
        //         SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1"), Player.position);
        //         for (int i = 0; i < 4; i++)
        //         {
        //             Vector2 dustPosition = Player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
        //             Dust.NewDust(dustPosition, 0, 0, DustID.Smoke, Player.velocity.X * -0.5f, Player.velocity.Y * -0.5f);
        //         }
        //         for (int i = 0; i < 15; i++)
        //         {
        //             Vector2 dustPosition = Player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
        //             Dust.NewDust(dustPosition, 0, 0, DustID.RedTorch, Player.velocity.X * -0.5f, Player.velocity.Y * -0.5f, 0, default, 6f);
        //         }
        //     }
 
        
        // }



        
            
            

        

             
            
        

        
        

