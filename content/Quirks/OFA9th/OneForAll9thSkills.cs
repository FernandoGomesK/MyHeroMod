using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Buffs;
using Terraria.Audio;
using Terraria.DataStructures;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer : ModPlayer, IQuirkResetter, IHeroDashModifier
    {
    
    
    public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash)
    {
    var transPlayer = Player.GetModPlayer<TransformationPlayer>();
    var ofaPlayer = Player.GetModPlayer<OneForAll9thPlayer>();
    
    
    isEnhanced = false;
    
    if (transPlayer.CurrentStage == QuirkStage.Initial) 
    {
        speed = 40;
        
        Player.statLife -= 50;
                if (Player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
                        Player.KillMe(reason, 100, 0);        
                }
        
    }
    else if  (Player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 5)
            {
                speed = 20;
                
            }
            else if (Player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 10)
            {
                speed = 25;
        
            }
            else if (Player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 45)
            {
                speed = 28;
        
            }
            else
            {
                speed = 20;
            
            }
    
        
    }
}

}
