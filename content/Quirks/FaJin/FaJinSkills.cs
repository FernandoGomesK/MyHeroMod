using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.Buffs;

using Terraria.ID;

using Terraria.Audio;
using MyHeroMod.content.System;


namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FajinPlayer : ModPlayer, IHeroDashModifier
    {
public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor, ref int dustType)
{
    var transPlayer = Player.GetModPlayer<TransformationPlayer>();
    

    bool hasFajinAccess = transPlayer.SelectedQuirk == QuirkType.FaJin || transPlayer.SelectedQuirk == QuirkType.OneForAll9th;
    if (hasFajinAccess) 
    {
        if (FaJinStored) 
        {
            speed = 25f;
            isEnhanced = true;  
            Player.ClearBuff(ModContent.BuffType<FaJinBuff>());
            FaJinCharges = 0;  
        }
        else 
        {
            if (isFaJinActive)
                    {
                        ChargeFajin();
            isEnhanced = true; 
                    }
             
        }
        
    }
}
      

}}
        


       
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



        
            
            

        

             
            
        

        
        

