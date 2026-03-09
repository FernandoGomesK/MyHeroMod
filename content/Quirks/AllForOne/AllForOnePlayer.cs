using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;
using Terraria.Audio;
using MyHeroMod.content.System;
using Terraria.GameContent.Bestiary;
using MyHeroMod.content.Quirks.OFA9th;


namespace MyHeroMod.content.Quirks.AllForOne;

    public partial class AllForOnePlayer : ModPlayer
{

    public List<QuirkType> InternalQuirks = new List<QuirkType>();

    public void UnlockQuirks(){
        var transPlayer = Player.GetModPlayer<TransformationPlayer>();


        if (transPlayer.SelectedQuirk == QuirkType.AllForOne)
        {
            
        }
    
    
        }
        public bool HasInternalQuirk(QuirkType type)
    {
        return InternalQuirks.Contains(type);
}
}
        