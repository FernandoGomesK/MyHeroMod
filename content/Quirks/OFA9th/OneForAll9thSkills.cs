using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using KhacesCore.Content.System.Interfaces;
using Terraria.Audio;
using MyHeroMod.content.Projectiles;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer : ModPlayer, IDashModifier
    {

        // ========================================= Dash Modifier =====================================================================
        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, 
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var ofaPlayer = Player.GetModPlayer<OneForAll9thPlayer>();
            
            
            if (!transPlayer.HasActiveQuirk(QuirkType.OneForAll9th)) return;

            
            
            if (transPlayer.CurrentStage == QuirkStage.Initial) 
            {
                speed = 80;
                Player.statLife -= 50;
                if (Player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
                    Player.KillMe(reason, 100, 0);        
                }
                onomatopoeiaType = ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>();
                isEnhanced = true;
                dustType = DustID.Cloud;
                explosionColor = Color.White; 
            }
            else if (Player.HasBuff(ModContent.BuffType<FullCowlingBuff>()))
            {
                isEnhanced = true;
                dustType = DustID.Cloud;
                explosionColor = Color.White; 
                if (ofaPlayer.percentage == 5) speed = 20;
                else if (ofaPlayer.percentage == 10) speed = 40;
                else if (ofaPlayer.percentage == 45) speed = 65;
                else speed = 20;

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
            if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            {
                onomatopoeiaType = ModContent.ProjectileType<GearDekuDetroitSmashOnomatopoeia>();
            }
            else
            {
                onomatopoeiaType = ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>();
            }
            }

            
            

            
            
        }
    }
}