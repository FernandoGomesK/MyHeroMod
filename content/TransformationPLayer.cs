using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using MyHeroMod;




namespace MyHeroMod.content
{
    
     public enum QuirkSkills
        {
            None,
            // Ofa
            SuperJump,

            // Ofa 8th
            PrimeDetroitSmash,
            PrimeCaliforniaSmash,
            PrimeTexasSmash,
            PrimeCarolinaSmash,
            StockPile,
            StockPileMaximum,


            // Ofa 9th
            DelawareSmash,
            DetroitSmash,
            OneForAllFullCowling5, // Full Cowling 5%
            OneForAllFullCowling8, // Full Cowling 8%
            OneForAllFullCowling45, // Full Cowling 45%
            BlackWhipHook, 
            OneForAllFullCowling100, // Full Cowling 100%
            BlackWhipSurge,
            Float,
            DangerSense,
            FaJinStore,
            SmokeScreen,
            Gearshift,

            // Hell Flames -------------------------------------------------------------------------------------------------------------------

            FlashFireFist,
            ProminenceBurn,
            JetBurn,
            HellSpider,
            IgnitedArrow,

            // Blue Flames ---------------------------------------------------------------------------------------------------------- 

            BlueFlashFireFist,
            BlueRage,
            BluePhosphor,
            BlueFireWave,
            BlueHellMineField,
            BlueProminenceBurn,
            BlueFireBall,
            BlueVanishingFist,
            BlueFlamethrower,
            BlueJetBurn,
            BlueHellSpider,


            //HCHH -----------------------------------------------------------------------------------------------------------------------
            HCFireFist,

            HeavenPiercingWall,
            FlashFreezeHeatWave,
            JetKindling,
            HCHellSpider,
            HCPhosphor,


            

            // Explosion
            ExplosionBlast,
            StunGrenade,
            ApShot,
            ApMachineGun,
            HowitzerImpact,
            Cluster,

            

        }
    public enum QuirkType{ Quirkless, OneForAll9th, OneForAll8th, Explosion, HellFlames, BlueFlames, HalfColdHalfHot }
    public enum QuirkStage{ Initial, Adequation, Intermediate, Advanced, Final }
    public class TransformationPlayer : ModPlayer
    {
        public QuirkType SelectedQuirk = QuirkType.Quirkless;
        public QuirkStage CurrentStage = QuirkStage.Initial;

        public bool ManualStageOverride = false;
        
        public QuirkSkills ActiveForm = QuirkSkills.None;

        public QuirkSkills Slot1 = QuirkSkills.SuperJump;
        public QuirkSkills Slot2 = QuirkSkills.DelawareSmash;
        public QuirkSkills Slot3 = QuirkSkills.None;
        public QuirkSkills TransformSlot = QuirkSkills.OneForAllFullCowling5;
    

    public override void SaveData(TagCompound tag)
        {
            tag["SelectedQuirk"] = (int)SelectedQuirk;
            tag["CurrentStage"] = (int)CurrentStage;
            tag["Slot1"] = (int)Slot1;
            tag["Slot2"] = (int)Slot2;
            tag["Slot3"] = (int)Slot3;
            tag["TransformSlot"] = (int)TransformSlot;
        }

    public override void LoadData(TagCompound tag)

    
        {
            if (tag.ContainsKey("SelectedQuirk")) SelectedQuirk = (QuirkType)tag.GetInt("SelectedQuirk");
            if (tag.ContainsKey("CurrentStage")) CurrentStage = (QuirkStage)tag.GetInt("CurrentStage");
            if (tag.ContainsKey("Slot1")) Slot1 = (QuirkSkills)tag.GetInt("Slot1");
            if (tag.ContainsKey("Slot2")) Slot2 = (QuirkSkills)tag.GetInt("Slot2");
            if (tag.ContainsKey("Slot3")) Slot3 = (QuirkSkills)tag.GetInt("Slot3");
            if (tag.ContainsKey("TransformSlot")) TransformSlot = (QuirkSkills)tag.GetInt("TransformSlot");
        }
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            // Agora com o "using MyHeroMod;" acima, o UISystem será encontrado.
            if (KeybindSystem.SkillMenu.JustPressed)
            {
                UISystem.ToggleSkillMenu();
            }   
        }
        public override void PreUpdate()
{
    // Exemplo: Evolução automática baseada em progresso
    var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

    if (!mainPlayer.ManualStageOverride)
    {
                
           
    // Se matou o Moon Lord -> Estágio Final
    if (NPC.downedMoonlord)
    {
        mainPlayer.CurrentStage = QuirkStage.Final;
    }
    // Se matou Plantera -> Estágio Avançado
    else if (NPC.downedPlantBoss)
    {
        mainPlayer.CurrentStage = QuirkStage.Advanced;
    }
    // Se entrou no Hardmode -> Estágio Intermediário
    else if (Main.hardMode)
    {
        mainPlayer.CurrentStage = QuirkStage.Intermediate;
    }
    // Padrão -> Adequation
    else 
    {
        mainPlayer.CurrentStage = QuirkStage.Adequation;
    }
     }
    
    // ... resto do seu código ...
}
    }

    
}
        
        
    
    
    

  