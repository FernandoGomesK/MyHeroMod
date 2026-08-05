using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Debuffs 
{
    
    public class GearshiftRecoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; 
            Main.buffNoSave[Type] = true; 
            Main.pvpBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HasBuff<OverlayBuff>())
            {
                
            }
            else
            {
                player.moveSpeed *= 0.1f;
                player.accRunSpeed *= 0.2f;
                player.maxRunSpeed *= 0.2f;
                player.GetDamage(DamageClass.Generic) *= 0.2f;
            }
              
        }
    }
}