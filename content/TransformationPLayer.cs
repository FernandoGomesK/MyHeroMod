using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using System.Collections.Generic; 
using MyHeroMod.content.System;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.OFA9th;
using System;
using Terraria.ID;

namespace MyHeroMod.content
{
    // --- ENUMS ---
    public enum QuirkType { Quirkless, AllForOne, OneForAll9th, OneForAll8th,
                            Explosion, Engine, HellFlames, BlueFlames, HalfColdHalfHot,
                            Float, Flight, Gearshift, FaJin, SmokeScreen, DangerSense,
                            BlackWhip, Tape, Overclock, Erasure, SuperRegeneration, SlideAndGlide,
                            Decay, Rivet, SpringLikeLimbs, Rabbit, DarkShadow, Overhaul,
                            ZeroGravity,
                             }

    // public enum NatureType
    // {
    //     None, ThermalResistance, ColdResistance, HeatResistance, NauseaResistance, StrongMinded, PerfectVessel, Resourceful
    // }
                            
    public enum QuirkStage { Initial, Adequation, Intermediate, Advanced, Final }

    
    public class TransformationPlayer : BasePlayer
    {
        public List<QuirkType> ActiveQuirks = new List<QuirkType>(); 
        public int naturalQuirkLimit = 1;

        public QuirkStage CurrentStage = QuirkStage.Initial;
        public bool ManualStageOverride = false;

        public bool hasRolledInitialTraits = false;
        
        public QuirkSkills ActiveForm = QuirkSkills.None;

        public QuirkSkills Slot1 = QuirkSkills.None;
        public QuirkSkills Slot2 = QuirkSkills.None;
        public QuirkSkills Slot3 = QuirkSkills.None;
        public QuirkSkills Slot4 = QuirkSkills.None;

        public NatureType Nature = NatureType.None;

        
        public List<QuirkSkills> UnlockedSkills = new List<QuirkSkills>();

        public override void PostUpdateMiscEffects()
        {
            int QuirkCount = ActiveQuirks.Count;

            if (QuirkCount == naturalQuirkLimit + 1)
            {
                Player.moveSpeed *= 0.8f;
                Player.GetDamage(DamageClass.Generic) *= 0.9f; 
            }
            else if (QuirkCount >= naturalQuirkLimit + 2)
            {
                Player.moveSpeed *= 0.5f; 
                Player.statDefense -= 20; 
                
                
                Player.AddBuff(BuffID.Confused, 2); 
                
                
                Player.AddBuff(BuffID.Silenced, 2);
            }
        }

        public override void PreUpdate()
        {

            base.PreUpdate();
            
            int buffToAdd = Nature switch
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

        public override void ResetEffects()
        {

            base.ResetEffects();
            
            naturalQuirkLimit = 1;

            
            if (Nature == NatureType.StrongMinded)
            {
                naturalQuirkLimit = 2; 
            }
            
        }

    

        // Save and Load

        public override void SaveData(TagCompound tag)
        {
            List<string> quirkNames = new List<string>();
            foreach (var quirk in ActiveQuirks)
            {
                quirkNames.Add(quirk.ToString());
            }
            
            tag["ActiveQuirkList"] = quirkNames;
            tag["CurrentStageName"] = CurrentStage.ToString();
            tag["PlayerNature"] = (int)Nature;
            tag["Slot1Name"] = Slot1.ToString();
            tag["Slot2Name"] = Slot2.ToString();
            tag["Slot3Name"] = Slot3.ToString();
            tag["Slot4Name"] = Slot4.ToString();
            tag["HasRolledInitialTraits"] = hasRolledInitialTraits;

        }

        public override void LoadData(TagCompound tag)
        {
            ActiveQuirks.Clear();

            if (tag.ContainsKey("ActiveQuirkList"))
            {IList<string> savedQuirks = tag.GetList<string>("ActiveQuirkList");
                foreach (var quirkName in savedQuirks)
                {
                    if (Enum.TryParse(quirkName, out QuirkType parsedQuirk))
                    {
                        ActiveQuirks.Add(parsedQuirk);
                    }
                }
             
                if (Enum.TryParse(tag.GetString("CurrentStageName"), out QuirkStage parsedStage)) CurrentStage = parsedStage;
                if (Enum.TryParse(tag.GetString("Slot1Name"), out QuirkSkills parsedSlot1)) Slot1 = parsedSlot1;
                if (Enum.TryParse(tag.GetString("Slot2Name"), out QuirkSkills parsedSlot2)) Slot2 = parsedSlot2;
                if (Enum.TryParse(tag.GetString("Slot3Name"), out QuirkSkills parsedSlot3)) Slot3 = parsedSlot3;
                if (Enum.TryParse(tag.GetString("Slot4Name"), out QuirkSkills parsedSlot4)) Slot4 = parsedSlot4;
            }
            else if (tag.ContainsKey("SelectedQuirk")) 
            {
                ActiveQuirks.Add((QuirkType)tag.GetInt("SelectedQuirk"));
                if (tag.ContainsKey("CurrentStage")) CurrentStage = (QuirkStage)tag.GetInt("CurrentStage");
                if (tag.ContainsKey("Slot1")) Slot1 = (QuirkSkills)tag.GetInt("Slot1");
                if (tag.ContainsKey("Slot2")) Slot2 = (QuirkSkills)tag.GetInt("Slot2");
                if (tag.ContainsKey("Slot3")) Slot3 = (QuirkSkills)tag.GetInt("Slot3");
                if (tag.ContainsKey("Slot4")) Slot4 = (QuirkSkills)tag.GetInt("Slot4");
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
            Slot1 = QuirkSkills.None;
            Slot2 = QuirkSkills.None;
            Slot3 = QuirkSkills.None;
            Slot4 = QuirkSkills.None;
        }

        // --- LÓGICA DE JOGO ---
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            if (KeybindSystem.SkillMenu.JustPressed)
            {
                UISystem.ToggleSkillMenu();
            }

            if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(Slot1);
            if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(Slot2);
            if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(Slot3);
            if (KeybindSystem.SkillSlot4.JustPressed) ExecuteSkill(Slot4);
        }

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

            if (!hasRolledInitialTraits)            {
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
                if (skill != null && skill.CheckUnlock(this)) 
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
            clone.Slot1 = Slot1;
            clone.Slot2 = Slot2;
            clone.Slot3 = Slot3;
            clone.Slot4 = Slot4;
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
            packet.Write((int)Slot1);
            packet.Write((int)Slot2);
            packet.Write((int)Slot3);
            packet.Write((int)Slot4);
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

            if (quirksChanged || CurrentStage != clone.CurrentStage ||
                Slot1 != clone.Slot1 || Slot2 != clone.Slot2 || Slot3 != clone.Slot3 || Slot4 != clone.Slot4)
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
                packet.Write((int)Slot1);
                packet.Write((int)Slot2);
                packet.Write((int)Slot3);
                packet.Write((int)Slot4);
                
                packet.Send(-1, Player.whoAmI); 
            }
        }
    }
}