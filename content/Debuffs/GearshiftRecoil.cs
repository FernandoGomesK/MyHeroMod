using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

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
            
            player.moveSpeed *= 0.1f;
            player.accRunSpeed *= 0.2f;
            player.maxRunSpeed *= 0.2f;
            player.GetDamage(DamageClass.Generic) *= 0.2f;

            // Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Ice, 0, 0, 100, default, 1.5f);
            //         d.noGravity = true;
            //         d.velocity *= 0.5f;   
        }
    }
}