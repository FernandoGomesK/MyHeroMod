using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.DarkShadow;

namespace MyHeroMod.content.Buffs
{
    public class RagnarokBuff : ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {

            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var darkPlayer = player.GetModPlayer<DarkShadowPlayer>();

            darkPlayer.isRagnarokDarkShadowOn = true;

            
          
        }
    }
}