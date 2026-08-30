using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using System.Collections.Generic; 
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.OFA9th;
using System;
using Terraria.ID;
using KhacesCore.Content.System;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Quirks.Smokescreen;

namespace MyHeroMod.content
{
    // --- ENUMS ---
    public enum QuirkType { Quirkless, AllForOne, OneForAll9th, OneForAll8th,
                            Explosion, Engine, HellFlames, Blueflame, HalfColdHalfHot,
                            Float, Flight, Gearshift, FaJin, SmokeScreen, DangerSense,
                            BlackWhip, Tape, Overclock, Erasure, SuperRegeneration, SlideAndGlide,
                            Decay, Rivet, SpringLikeLimbs, Rabbit, DarkShadow, Overhaul,
                            ZeroGravity, FierceWings, OpticBlast,  }
// Hardening
    public enum QuirkVariant
{
    Default,    
    Variant1,  
    
}

    public enum QuirkStage { Initial, Adequation, Intermediate, Advanced, Final }

    public class TransformationPlayer : BasePlayer
    {
        public List<QuirkType> ActiveQuirks = new List<QuirkType>(); 
        public int naturalQuirkLimit = 1;

        public QuirkStage CurrentStage = QuirkStage.Initial;
        public QuirkVariant CurrentVariant = QuirkVariant.Default;
        public bool ManualStageOverride = false;
        public bool hasRolledInitialTraits = false;
        
        
        public string ActiveForm = "None";

        

        public NatureType Nature = NatureType.None;
        public int ChannelTime = 0;

        
    
        
        public List<string> UnlockedSkills = new List<string>();

       public override string DisplayPowerName
        {
            get
            {
                if (ActiveForm != "None")
                    return ActiveForm;

                if (ActiveQuirks.Count == 0)
                    return "Quirkless";

                
                List<string> names = new List<string>();
                foreach (var quirk in ActiveQuirks)
                {
                    names.Add(GetFriendlyQuirkName(quirk));
                }
                return string.Join(", ", names); 
            }
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            var dangerPlayer = Player.GetModPlayer<DangerSensePlayer>();
            var smokePlayer = Player.GetModPlayer<SmokescreenPlayer>();

            
            float dangerChance = dangerPlayer.isDangerSenseActive ? dangerPlayer.dodgeChance : 0f;
            float smokeChance = smokePlayer.isSmokescreenActive ? smokePlayer.dodgeChance : 0f;

            
            float maxChance = Math.Max(dangerChance, smokeChance);

            if (maxChance > 0f && Main.rand.NextFloat() < maxChance)
            {
                
                if (dangerChance >= smokeChance && dangerChance > 0f)
                {
                    dangerPlayer.triggerVisual();
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/DangerSenseSound") with { Volume = 2.0f }, Player.position);
                }
                else if (smokeChance > 0f)
                {
                   
                }

                Player.SetImmuneTimeForAllTypes(80); 
                return true; 
            }

            return false; 
        }
    

        public override string DisplayPowerStage => CurrentStage switch
        {
            QuirkStage.Initial => "Initial",
            QuirkStage.Adequation => "Adequation",
            QuirkStage.Intermediate => "Intermediate",
            QuirkStage.Advanced => "Advanced",
            QuirkStage.Final => "Final",
            _ => "N/A"
        };

        private string GetFriendlyQuirkName(QuirkType quirk) => quirk switch
        {
            QuirkType.OneForAll9th => "One For All (9th)",
            QuirkType.OneForAll8th => "One For All (8th)",
            QuirkType.AllForOne => "All For One",
            QuirkType.HalfColdHalfHot => "Half-Cold Half-Hot",
            QuirkType.BlackWhip => "Black Whip",
            QuirkType.SmokeScreen => "Smoke Screen",
            QuirkType.SuperRegeneration => "Super Regeneration",
            QuirkType.SlideAndGlide => "Slide and Glide",
            QuirkType.SpringLikeLimbs => "Spring-Like Limbs",
            QuirkType.ZeroGravity => "Zero Gravity",
            QuirkType.FierceWings => "Fierce Wings",
            QuirkType.OpticBlast => "Optic Blast",
            _ => quirk.ToString()
        };

        

        public override void PostUpdateMiscEffects()
        {
            // int QuirkCount = ActiveQuirks.Count;

            // if (QuirkCount == naturalQuirkLimit + 1)
            // {
            //     Player.moveSpeed *= 0.8f;
            //     Player.GetDamage(DamageClass.Generic) *= 0.9f; 
            // }
            // else if (QuirkCount >= naturalQuirkLimit + 2)
            // {
            //     Player.moveSpeed *= 0.5f; 
            //     Player.statDefense -= 20; 
            //     Player.AddBuff(BuffID.Confused, 2); 
            //     Player.AddBuff(BuffID.Silenced, 2);
            //     Player.AddBuff(BuffID.Darkness, 2);
            // }
            // else if (QuirkCount >= naturalQuirkLimit + 3)
            // {
            //     Player.moveSpeed *= 0.5f; 
            //     Player.statDefense -= 20; 
            //     Player.AddBuff(BuffID.Confused, 2); 
            //     Player.AddBuff(BuffID.Silenced, 2);
            //     Player.AddBuff(BuffID.Darkness, 2);
            //     Player.AddBuff(BuffID.Blackout, 2);
            //     Player.AddBuff(BuffID.Obstructed, 2);
            // }
            // else if (QuirkCount >= naturalQuirkLimit + 4)
            // {
            //     Player.moveSpeed *= 0.5f; 
            //     Player.statDefense -= 20; 
            //     Player.AddBuff(BuffID.Confused, 2); 
            //     Player.AddBuff(BuffID.Silenced, 2);
            //     Player.AddBuff(BuffID.Darkness, 2);
            //     Player.AddBuff(BuffID.Blackout, 2);
            //     Player.AddBuff(BuffID.Obstructed, 2);
            //     Player.AddBuff(BuffID.Weak, 2);
            // }
        }

        public override void PostUpdateEquips()
        {
            maxStrain = CurrentStage switch
            {
                QuirkStage.Initial => 300,
                QuirkStage.Adequation => 500,
                QuirkStage.Intermediate => 600,
                QuirkStage.Advanced => 800,
                QuirkStage.Final => 1200,
                _ => 0
            };
        }

        public override void PreUpdate()
        {

            

            base.PreUpdate();
            
            // int buffToAdd = Nature switch
            // {
            //     NatureType.ThermalResistance => ModContent.BuffType<Buffs.ThermalResistanceBuff>(),
            //     NatureType.ColdResistance => ModContent.BuffType<Buffs.ColdResistanceBuff>(),
            //     NatureType.HeatResistance => ModContent.BuffType<Buffs.HeatResistanceBuff>(),
            //     NatureType.NauseaResistance => ModContent.BuffType<Buffs.NauseaResistanceBuff>(), 
            //     NatureType.StrongMinded => ModContent.BuffType<Buffs.StrongMindedBuff>(),
            //     NatureType.PerfectVessel => ModContent.BuffType<Buffs.PerfectVesselBuff>(),
            //     NatureType.Resourceful => ModContent.BuffType<Buffs.ResourcefulBuff>(),
            //     _ => -1 
            // };

            // if (buffToAdd != -1)
            // {
            //     Player.AddBuff(buffToAdd, 2);
            // }
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            
            // naturalQuirkLimit = 1;
            // if (Nature == NatureType.StrongMinded)
            // {
            //     naturalQuirkLimit = 2; 
            // }
        }

    
        public override void SaveData(TagCompound tag)
        {
            List<string> quirkNames = new List<string>();
            foreach (var quirk in ActiveQuirks)
            {
                quirkNames.Add(quirk.ToString());
            }
            
            tag["ActiveQuirkList"] = quirkNames;
            tag["CurrentStageName"] = CurrentStage.ToString();
            
            // --- NEW: Save Variant ---
            tag["CurrentVariantName"] = CurrentVariant.ToString(); 
            
            tag["PlayerNature"] = (int)Nature;
            
            tag["Slot1Name"] = Slot1;
            tag["Slot2Name"] = Slot2;
            tag["Slot3Name"] = Slot3;
            tag["Slot4Name"] = Slot4;
            tag["Slot5Name"] = Slot5;
            tag["Slot6Name"] = Slot6;
            tag["Slot7Name"] = Slot7;
            tag["Slot8Name"] = Slot8;
            
            tag["HasRolledInitialTraits"] = hasRolledInitialTraits;
        }

        public override void LoadData(TagCompound tag)
        {
            ActiveQuirks.Clear();

            if (tag.ContainsKey("ActiveQuirkList"))
            {
                IList<string> savedQuirks = tag.GetList<string>("ActiveQuirkList");
                foreach (var quirkName in savedQuirks)
                {
                    if (Enum.TryParse(quirkName, out QuirkType parsedQuirk))
                    {
                        ActiveQuirks.Add(parsedQuirk);
                    }
                }
             
                if (Enum.TryParse(tag.GetString("CurrentStageName"), out QuirkStage parsedStage)) CurrentStage = parsedStage;
                
                // --- NEW: Load Variant ---
                if (tag.ContainsKey("CurrentVariantName"))
                {
                    if (Enum.TryParse(tag.GetString("CurrentVariantName"), out QuirkVariant parsedVariant))
                        CurrentVariant = parsedVariant;
                }
                
                if (tag.ContainsKey("Slot1Name")) Slot1 = tag.GetString("Slot1Name");
                if (tag.ContainsKey("Slot2Name")) Slot2 = tag.GetString("Slot2Name");
                if (tag.ContainsKey("Slot3Name")) Slot3 = tag.GetString("Slot3Name");
                if (tag.ContainsKey("Slot4Name")) Slot4 = tag.GetString("Slot4Name");
                if (tag.ContainsKey("Slot5Name")) Slot5 = tag.GetString("Slot5Name");
                if (tag.ContainsKey("Slot6Name")) Slot6 = tag.GetString("Slot6Name");
                if (tag.ContainsKey("Slot7Name")) Slot7 = tag.GetString("Slot7Name");
                if (tag.ContainsKey("Slot8Name")) Slot8 = tag.GetString("Slot8Name");
            }
            else if (tag.ContainsKey("SelectedQuirk"))
            {
                ActiveQuirks.Add((QuirkType)tag.GetInt("SelectedQuirk"));
                if (tag.ContainsKey("CurrentStage")) CurrentStage = (QuirkStage)tag.GetInt("CurrentStage");
            }

            if (tag.ContainsKey("PlayerNature")) 
            {
                Nature = (NatureType)tag.GetInt("PlayerNature");
            }
            if (tag.ContainsKey("HasRolledInitialTraits"))
            {
                hasRolledInitialTraits = tag.GetBool("HasRolledInitialTraits");
            }
            
            UpdateUnlockedSkills();
        }

        public void ResetSlot()
        {
            Slot1 = "None"; Slot2 = "None"; Slot3 = "None"; Slot4 = "None";
            Slot5 = "None"; Slot6 = "None"; Slot7 = "None"; Slot8 = "None";
        }

        // public bool HasLethalStrainQuirk()
        // {
        //     foreach (var quirk in ActiveQuirks)
        //     {
            
        //         if (quirk == QuirkType.OneForAll9th || quirk == QuirkType.Blueflame)
        //         {
        //             return true;
        //         }
        //     }
        //     return false;
        // }

//         public override void UpdateBadLifeRegen()
// {
//     if (HasActiveQuirk(QuirkType.SuperRegeneration) && !HasLethalStrainQuirk())
//         return;

//     float strainRatio = maxStrain > 0 ? (float)currentStrain / maxStrain : 0f;
//     if (strainRatio < 0.25f)
//         return;

//     bool lethal = HasLethalStrainQuirk();


//     float damagePercent = strainRatio switch
//     {
//         >= 0.75f => lethal ? 0.06f : 0.10f,
//         >= 0.50f => lethal ? 0.03f : 0.05f,
//         _        => lethal ? 0.01f : 0.02f,
//     };

    
//     int floorPercent = lethal ? 5 : strainRatio switch
//     {
//         >= 0.75f => 25,
//         >= 0.50f => 50,
//         _        => 75,
//     };
//     int floorHealth = (int)(Player.statLifeMax2 * (floorPercent / 100f));

//     if (Player.statLife > floorHealth)
//     {
//         int damagePerSecond = (int)(Player.statLifeMax2 * damagePercent);
//         if (Player.lifeRegen > 0) Player.lifeRegen = 0;
//         Player.lifeRegen -= damagePerSecond * 2;
//     }
//     else
//     {
//         Player.statLife = floorHealth;
//         if (Player.lifeRegen < 0) Player.lifeRegen = 0;
//     }
// }

        public bool HasActiveQuirk(QuirkType typeToCheck)
        {
            if (ActiveQuirks.Contains(typeToCheck)) return true;
            
            var afoPlayer = Player.GetModPlayer<AllForOnePlayer>();
            var ofaPlayer = Player.GetModPlayer<OneForAll9thPlayer>(); 

            if ((ActiveQuirks.Contains(QuirkType.AllForOne) && afoPlayer.HasInternalQuirk(typeToCheck)) || 
                (ActiveQuirks.Contains(QuirkType.OneForAll9th) && ofaPlayer.HasInternalQuirk(typeToCheck)))
            {
                return true;
            }

            return false;
        }

        public override void OnEnterWorld() 
        {
            UpdateUnlockedSkills();
            ProgressionSystem.UpdateStage(this);

            if (!hasRolledInitialTraits)            
            {
                RandomNatureSelection.SelectRandomNature();
                RandomQuirkSelection.SelectRandomQuirk();
                hasRolledInitialTraits = true;
            }
        }

        public override void PostUpdate()
        {
            ProgressionSystem.UpdateStage(this);
        }

        public void CompleteReset()
        {
            foreach (var modPlayer in Player.ModPlayers)
            {
                if (modPlayer is IQuirkResetter quirkResetter)
                {
                    quirkResetter.FullReset();
                }
            }
        }

        public void UpdateUnlockedSkills() 
        {
            UnlockedSkills.Clear();
            foreach (var skillId in SkillLibrary.GetAllIds()) 
            {
                var skill = SkillLibrary.GetSkill(skillId);
                
                if (skill is QuirkBaseSkill quirkSkill && quirkSkill.CheckUnlock(this)) 
                {
                    UnlockedSkills.Add(skillId);
                }
            }
        }
        
        public override void CopyClientState(ModPlayer clientClone)
        {
            TransformationPlayer clone = clientClone as TransformationPlayer;
            clone.ActiveQuirks = new List<QuirkType>(ActiveQuirks); 
            clone.CurrentStage = CurrentStage;
            clone.CurrentVariant = CurrentVariant; 
            
            clone.Slot1 = Slot1; clone.Slot2 = Slot2; clone.Slot3 = Slot3; clone.Slot4 = Slot4;
            clone.Slot5 = Slot5; clone.Slot6 = Slot6; clone.Slot7 = Slot7; clone.Slot8 = Slot8;
            
            clone.Nature = Nature;
            clone.currentStrain = currentStrain;

            clone.UseSecondaryBar = UseSecondaryBar;
            clone.CurrentRaceId = CurrentRaceId;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncTransformationPlayer); 
            packet.Write((byte)Player.whoAmI); 
            
            packet.Write(ActiveQuirks.Count);
            foreach (var quirk in ActiveQuirks)
            {
                packet.Write((int)quirk);
            }

            packet.Write((int)CurrentStage);
            packet.Write((int)CurrentVariant); 
            packet.Write((int)Nature);
            packet.Write(currentStrain); 
            
            packet.Write(Slot1 ?? "None"); packet.Write(Slot2 ?? "None");
            packet.Write(Slot3 ?? "None"); packet.Write(Slot4 ?? "None");
            packet.Write(Slot5 ?? "None"); packet.Write(Slot6 ?? "None");
            packet.Write(Slot7 ?? "None"); packet.Write(Slot8 ?? "None");
            
            packet.Write(UseSecondaryBar);
            packet.Write(CurrentRaceId ?? "Human");

            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            TransformationPlayer clone = clientPlayer as TransformationPlayer;

            bool quirksChanged = ActiveQuirks.Count != clone.ActiveQuirks.Count;
            if (!quirksChanged)
            {
                for (int i = 0; i < ActiveQuirks.Count; i++)
                {
                    if (ActiveQuirks[i] != clone.ActiveQuirks[i]) quirksChanged = true;
                }
            }
            
            if (quirksChanged || CurrentStage != clone.CurrentStage || CurrentVariant != clone.CurrentVariant || // NEW
                Nature != clone.Nature || currentStrain != clone.currentStrain ||
                Slot1 != clone.Slot1 || Slot2 != clone.Slot2 || Slot3 != clone.Slot3 || Slot4 != clone.Slot4 ||
                Slot5 != clone.Slot5 || Slot6 != clone.Slot6 || Slot7 != clone.Slot7 || Slot8 != clone.Slot8 ||
                UseSecondaryBar != clone.UseSecondaryBar || CurrentRaceId != clone.CurrentRaceId)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncTransformationPlayer);
                packet.Write((byte)Player.whoAmI);
                
                packet.Write(ActiveQuirks.Count);
                foreach (var quirk in ActiveQuirks)
                {
                    packet.Write((int)quirk);
                }

                packet.Write((int)CurrentStage);
                packet.Write((int)CurrentVariant); 
                packet.Write((int)Nature); 
                packet.Write(currentStrain); 
                
                packet.Write(Slot1 ?? "None"); packet.Write(Slot2 ?? "None");
                packet.Write(Slot3 ?? "None"); packet.Write(Slot4 ?? "None");
                packet.Write(Slot5 ?? "None"); packet.Write(Slot6 ?? "None");
                packet.Write(Slot7 ?? "None"); packet.Write(Slot8 ?? "None");
                
                packet.Write(UseSecondaryBar);
                packet.Write(CurrentRaceId ?? "Human");

                packet.Send(-1, Player.whoAmI); 
            }
        }
    }
}