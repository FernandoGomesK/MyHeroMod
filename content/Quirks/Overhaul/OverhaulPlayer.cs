using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Overhaul
{
    public partial class OverhaulPlayer : ModPlayer
    {

        public bool isChimeraActive = false;
        
        public override void ResetEffects()
        {
            isChimeraActive = false;
        }

        
        public override void FrameEffects()
        {
            
            if (Player.HasBuff(ModContent.BuffType<Buffs.ChimeraBuff>()))
            {
                // Forçamos o ID do braço da frente para ser o nosso Acessório Fantasma
                Player.handon = EquipLoader.GetEquipSlot(Mod, "ChimeraArms", EquipType.HandsOn);
                
                // Forçamos o ID do braço de trás
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "ChimeraArms", EquipType.HandsOff);
            }
        }
    }

}