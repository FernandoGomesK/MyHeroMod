using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs.Natures;

namespace MyHeroMod.content.Handlers
{
    public class NatureHandler : ModPlayer
    {
        public override void PreUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
            int buffToAdd = transPlayer.Nature switch
            {
                NatureType.Aerodynamic => ModContent.BuffType<AerodynamicBuff>(),
                NatureType.ThermalResistance => ModContent.BuffType<ThermalResistanceBuff>(),
                NatureType.ColdResistance => ModContent.BuffType<ColdResistanceBuff>(),
                NatureType.HeatResistance => ModContent.BuffType<HeatResistanceBuff>(),
                NatureType.StrongMinded => ModContent.BuffType<StrongMindedBuff>(),
                NatureType.PerfectVessel => ModContent.BuffType<PerfectVesselBuff>(),
                NatureType.Resourceful => ModContent.BuffType<ResourcefulBuff>(),
                NatureType.ResistantBody => ModContent.BuffType<ResistantBodyBuff>(),
                NatureType.KinecticAbsorber => ModContent.BuffType<KinecticAbsorberBuff>(),
                _ => -1
            };

            if (buffToAdd != -1)
            {
                Player.AddBuff(buffToAdd, 2);
            }
        }
    }
}