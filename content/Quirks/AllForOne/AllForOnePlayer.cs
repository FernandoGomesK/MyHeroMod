using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;
using Terraria.Audio;
using MyHeroMod.content.System;
using Terraria.GameContent.Bestiary;
using MyHeroMod.content.Quirks.OFA9th;
using Terraria.ModLoader.IO;


namespace MyHeroMod.content.Quirks.AllForOne;

    public partial class AllForOnePlayer : ModPlayer, IQuirkResetter


{

    public void FullReset() => InternalQuirks.Clear();
    public List<QuirkType> InternalQuirks = new List<QuirkType>();

    // public void listQuirks()
    // {
    //     foreach (var quirk in InternalQuirks)
    //     {
    //         Main.NewText(quirk.ToString());
    //     }
    // }

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

public override void SaveData(TagCompound tag)
        {
            List<int> savedQuirks = new List<int>();
            foreach (var quirk in InternalQuirks)
            {
                savedQuirks.Add((int)quirk); // Converte o Enum para Número para poder salvar
            }
            tag["AfoStolenQuirks"] = savedQuirks;
        }

        
        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("AfoStolenQuirks"))
            {
                InternalQuirks.Clear();
                var savedQuirks = tag.GetList<int>("AfoStolenQuirks");
                foreach (var quirkId in savedQuirks)
                {
                    InternalQuirks.Add((QuirkType)quirkId); // Converte o Número de volta para Enum
                }
            }
}}
        