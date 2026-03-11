using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace MyHeroMod.content.Debuffs 
{
    
    public class QuirkErased : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; 
            Main.buffNoSave[Type] = true; 
        }
    }}