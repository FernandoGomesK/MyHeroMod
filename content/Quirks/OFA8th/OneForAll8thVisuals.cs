using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content.System.BasePlayer;
using Terraria.ModLoader;
using Terraria;
using MyHeroMod.content.Quirks.OFA8th;


namespace MyHeroMod.content.Quirks.OFA8th
{
    public partial class OneForAll8thPlayer : ModPlayer
    {

        
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            var ofa8 = Player.GetModPlayer<OneForAll8thPlayer>();


            
       if (Player.HasBuff(ModContent.BuffType<StockPileBuff>())  && ofa8.form == 2)
       {
           

            Player.armorEffectDrawShadow = true; 
               

            
            
        
            
        }
    }
    }
    }