using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures; 
using MyHeroMod.content.Items.Weapons; 

namespace MyHeroMod.content.System
{
    public class HideArmsPlayer : ModPlayer
    {
        
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
           
            if (Player.HeldItem.type == ModContent.ItemType<PunchAttack>() && Player.itemAnimation > 0)
            {
                PlayerDrawLayers.ArmOverItem.Hide();
                PlayerDrawLayers.HandOnAcc.Hide();
            }
        }
    }
}