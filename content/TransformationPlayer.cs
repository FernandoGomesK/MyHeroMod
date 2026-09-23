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
using Microsoft.Xna.Framework;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content
{
    public enum QuirkType
    {
        Quirkless, AllForOne, OneForAll9th, OneForAll8th,
        Explosion, Engine, HellFlames, Blueflame, HalfColdHalfHot,
        Float, Flight, Gearshift, FaJin, SmokeScreen, DangerSense,
        BlackWhip, Tape, Overclock, Erasure, SuperRegeneration, SlideAndGlide,
        Decay, Rivet, SpringLikeLimbs, Rabbit, DarkShadow, Overhaul,
        ZeroGravity, FierceWings, OpticBlast, Hardening
    }

    public enum QuirkVariant
    {
        Default,
        Variant1,
    }

    public enum QuirkStage { Initial, Adequation, Intermediate, Advanced, Final }

    public class TransformationPlayer : BasePlayer
    {
        public List<QuirkType> ActiveQuirks = [];
        public int naturalQuirkLimit = 1;

        public QuirkStage CurrentStage = QuirkStage.Initial;
        public QuirkVariant CurrentVariant = QuirkVariant.Default;
        public bool ManualStageOverride = false;
        public bool hasRolledInitialTraits = false;

        public string ActiveForm = "None";
        public NatureType Nature = NatureType.None;
        public int ChannelTime = 0;

        public List<string> UnlockedSkills = [];
        public CorePlayer Core => Player.GetModPlayer<CorePlayer>();

        public override string DisplayPowerName
        {
            get
            {
                if (ActiveForm != "None")
                    return ActiveForm;

                if (ActiveQuirks.Count == 0)
                    return "Quirkless";

                List<string> names = [];
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
                    dangerPlayer.TriggerVisual();
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

        private static string GetFriendlyQuirkName(QuirkType quirk) => quirk switch
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

        public override void PostUpdateEquips()
        {
            Core.maxStrain = CurrentStage switch
            {
                QuirkStage.Initial => 300,
                QuirkStage.Adequation => 500,
                QuirkStage.Intermediate => 600,
                QuirkStage.Advanced => 800,
                QuirkStage.Final => 1200,
                _ => 100
            };
        }

        public override void PreUpdate()
        {
            base.PreUpdate();

            if (ActiveQuirks.Contains(QuirkType.OneForAll8th) && ActiveQuirks.Contains(QuirkType.OneForAll9th))
            {
                var ofa9Player = Player.GetModPlayer<OneForAll9thPlayer>();
                if (ActiveQuirks.Contains(QuirkType.OneForAll9th))
                {
                    ofa9Player.timeUsed += 200;
                }
                ActiveQuirks.Remove(QuirkType.OneForAll8th);
                CombatText.NewText(Player.getRect(), Color.Orange, "Your one for all power has been absorbed!", true);
            }
        }

        public override void SaveData(TagCompound tag)
        {
            List<string> quirkNames = [];
            foreach (var quirk in ActiveQuirks)
            {
                quirkNames.Add(quirk.ToString());
            }

            tag["ActiveQuirkList"] = quirkNames;
            tag["CurrentStageName"] = CurrentStage.ToString();
            tag["CurrentVariantName"] = CurrentVariant.ToString();
            tag["PlayerNature"] = (int)Nature;
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

                if (tag.ContainsKey("CurrentVariantName"))
                {
                    if (Enum.TryParse(tag.GetString("CurrentVariantName"), out QuirkVariant parsedVariant))
                        CurrentVariant = parsedVariant;
                }
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
            clone.ActiveQuirks = [.. ActiveQuirks];
            clone.CurrentStage = CurrentStage;
            clone.CurrentVariant = CurrentVariant;
            clone.Nature = Nature;

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

            if (quirksChanged || CurrentStage != clone.CurrentStage || CurrentVariant != clone.CurrentVariant ||
                Nature != clone.Nature)
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

                packet.Send(-1, Player.whoAmI);
            }
        }
    }
}