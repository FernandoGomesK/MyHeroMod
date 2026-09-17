using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Debuffs;
using KhacesCore.Content.System;

namespace MyHeroMod.content.System
{
    public abstract class QuirkBaseSkill : BaseSkill<TransformationPlayer>
    {
        public virtual QuirkType RequiredQuirk => QuirkType.Quirkless;
        public virtual QuirkStage RequiredStage => QuirkStage.Initial;
        public virtual QuirkStage RequiredOfaStage => RequiredStage;
        public virtual bool isOfaSkill => false;

        public override bool CheckNegation(Player player) =>
            !player.HasBuff(ModContent.BuffType<QuirkErased>());

        public override bool CheckUnlock(TransformationPlayer player)
        {
           
            if (IsDefaultSkill) return true;

            if (IsItemSkill && !CheckItemSkill(player.Player))
                return false;

            if (IsItemSkill && RequiredQuirk == QuirkType.Quirkless)
                return true;

           
            if (player.HasActiveQuirk(QuirkType.OneForAll9th))
            {
                var ofaPlayer = player.Player.GetModPlayer<OneForAll9thPlayer>();
                if (ofaPlayer.HasInternalQuirk(RequiredQuirk))
                    return player.CurrentStage >= RequiredOfaStage;
            }

            if (player.HasActiveQuirk(QuirkType.AllForOne))
            {
                var afoPlayer = player.Player.GetModPlayer<AllForOnePlayer>();
                if (afoPlayer.HasInternalQuirk(RequiredQuirk))
                    return player.CurrentStage >= RequiredStage;
            }

            return player.HasActiveQuirk(RequiredQuirk) && player.CurrentStage >= RequiredStage;
        }
    }
}