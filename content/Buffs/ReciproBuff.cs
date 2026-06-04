using Terraria.ModLoader;
using Terraria;

using MyHeroMod.content.Quirks.SlideAndGlide;
using MyHeroMod.content.Quirks.Engine;

namespace MyHeroMod.content.Buffs 
{
    public class ReciproBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }
        

        public override void Update(Player player, ref int buffIndex)
        {
                var enginePlayer = player.GetModPlayer<EnginePlayer>();
                var mainPlayer = player.GetModPlayer<TransformationPlayer>();
    
                if (!mainPlayer.HasActiveQuirk(QuirkType.Engine))  
                    return;

                enginePlayer.isBoosting = true;
                
                
        }
    }}