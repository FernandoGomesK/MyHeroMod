using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Handlers
{
    public class NatureHandler : ModPlayer
    {
        public override void PreUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            int buffToAdd = transPlayer.Nature switch
            {
                NatureType.ThermalResistance => ModContent.BuffType<Buffs.ThermalResistanceBuff>(),
                NatureType.ColdResistance => ModContent.BuffType<Buffs.ColdResistanceBuff>(),
                NatureType.HeatResistance => ModContent.BuffType<Buffs.HeatResistanceBuff>(),
                NatureType.NauseaResistance => ModContent.BuffType<Buffs.NauseaResistanceBuff>(),
                NatureType.StrongMinded => ModContent.BuffType<Buffs.StrongMindedBuff>(),
                NatureType.PerfectVessel => ModContent.BuffType<Buffs.PerfectVesselBuff>(),
                NatureType.Resourceful => ModContent.BuffType<Buffs.ResourcefulBuff>(),
                _ => -1
            };

            if (buffToAdd != -1)
            {
                Player.AddBuff(buffToAdd, 2);
            }
        }
    }
}