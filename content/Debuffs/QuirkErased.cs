using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Quirks.FaJin;
using MyHeroMod.content.Quirks.Gearshift;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.Overclock;
using MyHeroMod.content.Quirks.OFA8th;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.Explosion;
using MyHeroMod.Buffs;
using MyHeroMod.content.Quirks.Smokescreen;
using MyHeroMod.content.Quirks.Float;
using MyHeroMod.content.Quirks.Erasure;


namespace MyHeroMod.content.Debuffs 
{
    
    public class QuirkErased : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; 
            Main.buffNoSave[Type] = true; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var fajinPlayer = player.GetModPlayer<FajinPlayer>();
            fajinPlayer.FaJinCharges = 0;

            var gearshiftPlayer = player.GetModPlayer<GearshiftPlayer>();
            gearshiftPlayer.isGearshiftBuffActive = false;
            player.ClearBuff(ModContent.BuffType<GearshiftBuff>());

            var erasurePlayer = player.GetModPlayer<ErasurePlayer>();
            erasurePlayer.isErasureActive = false;
            player.ClearBuff(ModContent.BuffType<ErasingBuff>());

            var overclockPlayer = player.GetModPlayer<OverclockPlayer>();
            overclockPlayer.isOverclockBuffActive = false;
            player.ClearBuff(ModContent.BuffType<OverclockBuff>());

            var explosionPlayer = player.GetModPlayer<ExplosionPlayer>();
            explosionPlayer.IsClusterActive = false;
            player.ClearBuff(ModContent.BuffType<ClusterBuff>());

            var smokePlayer = player.GetModPlayer<SmokescreenPlayer>();
            smokePlayer.isSmokescreenActive = false;
            player.ClearBuff(ModContent.BuffType<SmokescreenBuff>());

            var floatPlayer = player.GetModPlayer<FloatPlayer>();
            floatPlayer.isFloatActive = false;
            player.ClearBuff(ModContent.BuffType<FloatBuff>());

            var ofa9Player = player.GetModPlayer<OneForAll9thPlayer>();
            ofa9Player.isFullCowlingBuffActive = false;
            ofa9Player.percentage = 0;
            player.ClearBuff(ModContent.BuffType<FullCowlingBuff>());

            var ofa8Player = player.GetModPlayer<OneForAll8thPlayer>();
            ofa8Player.form = 0;
            player.ClearBuff(ModContent.BuffType<StockPileBuff>());

            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            hchhPlayer.IsFlashFireFistActive = false;
            hchhPlayer.IsPhosphorActive = false;
            player.ClearBuff(ModContent.BuffType<FlashFireFistBuff>());
            player.ClearBuff(ModContent.BuffType<PhosphorBuff>());
        }
    }}