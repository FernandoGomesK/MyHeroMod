using Terraria.ModLoader;
using Terraria;
using MyHeroMod.content.Quirks.Hardening;
using MyHeroMod.content.Quirks.Grasshoper;

namespace MyHeroMod.content.Buffs.Grasshoper
{
    public class GrasshoperBuff : ModBuff
    {
        
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GrasshoperPlayer>().IsGrasshoper = true;
            player.moveSpeed += 1.5f;
            player.jumpSpeedBoost += 1.5f;
            player.statDefense += 10;
            player.GetDamage(DamageClass.Melee) += 0.15f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
            player.noFallDmg = true;
        }
    }
}