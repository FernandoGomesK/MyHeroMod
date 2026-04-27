using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Overhaul;

namespace MyHeroMod.content.Buffs
{
    public class ChimeraBuff : ModBuff
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
            var overhaul = player.GetModPlayer<OverhaulPlayer>();


            overhaul.isChimeraActive = true;
            
                player.moveSpeed += 1.5f; 
                player.statDefense += 2;    
                player.jumpSpeedBoost += 2.0f;
                player.noFallDmg = true;
            
}
    }}