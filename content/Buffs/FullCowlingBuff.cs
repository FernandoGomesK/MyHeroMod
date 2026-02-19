using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.OFA9th.Buffs
{
    public class FullCowlingBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/OneForAllFullCowling5Percent";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            ofaPlayer.isFullCowlingBuffActive = true;

            if (transformPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling5)
            {
                
                player.moveSpeed += 0.50f; 
                player.statDefense += 2;    
                player.jumpSpeedBoost += 2.0f;
                player.noFallDmg = true;
            }
            else if (transformPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling8)
            {
                
                player.moveSpeed += 0.80f; 
                player.statDefense += 3;    
                player.jumpSpeedBoost += 3f; 
                player.noFallDmg = true;
            }
            else if (transformPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling45)
            {
                
                player.moveSpeed += 2f; 
                player.statDefense += 3;  
                player.jumpSpeedBoost += 5f;
                player.noFallDmg = true;
            }
        }
    }
}