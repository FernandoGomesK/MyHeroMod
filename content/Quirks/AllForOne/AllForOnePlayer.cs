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
    public int quirkCounter = 0;   
    public int maxQuirks = 0;

    

    public void FullReset() => InternalQuirks.Clear();
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
            }}
            public int CurrentQuirkCount => InternalQuirks.Count;

            public override void PreUpdate()
            {
                var transPlayer = Player.GetModPlayer<TransformationPlayer>();

                    maxQuirks = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 2,
                QuirkStage.Adequation => 3,
                QuirkStage.Intermediate => 5,
                QuirkStage.Advanced => 8,
                QuirkStage.Final => 10,
                _ => 15, // Default
            };
                    
                    
            }

        // public void quirkCount(){
        //     foreach (var quirk in InternalQuirks)
        // {
        //     quirkCounter++;
        // }}

        

        
        

        
    }
        