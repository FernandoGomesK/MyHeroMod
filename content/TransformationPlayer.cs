using KhacesCore.Content.System;
using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.Smokescreen;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MyHeroMod.content
{
    /// <summary>
    /// All of the quirks available in the mod
    /// </summary>
    public enum QuirkType
    {
        Quirkless, AllForOne, OneForAll9th, OneForAll8th,
        Explosion, Engine, HellFlames, Blueflame, HalfColdHalfHot,
        Float, Flight, Gearshift, FaJin, SmokeScreen, DangerSense,
        BlackWhip, Tape, Overclock, Erasure, SuperRegeneration, SlideAndGlide,
        Decay, Rivet, SpringLikeLimbs, Rabbit, DarkShadow, Overhaul,
        ZeroGravity, FierceWings, OpticBlast, Hardening
    }

    /// <summary>
    /// Quirk Variants, that mainly change the visuals for now (I.E pink or red Eye Beams)
    /// </summary>
    public enum QuirkVariant
    {
        Default,
        Variant1,
    }

    /// <summary>
    /// player progression states, mirroring CorePlayer.
    /// </summary>
    public enum QuirkStage { Initial, Adequation, Intermediate, Advanced, Final }

    /// <summary>
    /// ModPlayer central do MyHeroMod: guarda os quirks ativos do jogador, o estágio e a
    /// variante de progressão, e implementa <see cref="IPowerSystem"/> para se integrar ao Core.
    /// </summary>
    public class TransformationPlayer : BasePlayer, IPowerSystem
    {
        #region Campos e Propriedades

        /// <summary>List with the player current Active Quirks</summary>
        public List<QuirkType> ActiveQuirks = [];

        /// <summary>Current progression stage (mirrors CorePlayer).</summary>
        public QuirkStage CurrentStage = QuirkStage.Initial;

        /// <summary>Current variant of the quirk.</summary>
        public QuirkVariant CurrentVariant = QuirkVariant.Default;

        /// <summary>Inticates if the initial traits (quirk + nature) have already been rolled.</summary>
        public bool hasRolledInitialTraits = false;

        /// <summary>Name of the form/transformation active (mostly legacy).</summary>
        public string ActiveForm = "None";

        /// <summary>Nature (passive buffs to quirks) of the player.</summary>
        public NatureType Nature = NatureType.None;

        /// <summary>Channel time to activate certain skills.</summary>
        public int ChannelTime = 0;

        /// <summary>ID of the skills unlocked by the player.</summary>
        public List<string> UnlockedSkills = [];

        /// <summary>pointer to the corePlayer.</summary>
        public CorePlayer Core => Player.GetModPlayer<CorePlayer>();

        #endregion


        #region IPowerSystem

        /// <inheritdoc/>
        public bool IsEnabled => true;

        /// <inheritdoc/>
        public string PowerTypeName => "Quirk";

        /// <summary>Friendly dsiplay name of the active quirks of the player.</summary>
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

        /// <summary>Frienfly name of the current powerStage.</summary>
        public override string DisplayPowerStage => CurrentStage switch
        {
            QuirkStage.Initial => "Initial",
            QuirkStage.Adequation => "Adequation",
            QuirkStage.Intermediate => "Intermediate",
            QuirkStage.Advanced => "Advanced",
            QuirkStage.Final => "Final",
            _ => "N/A"
        };

        /// <summary>
        /// Called by the core to recalculate the unlocked Skills
        ///  <see cref="QuirkStage"/>
        /// </summary>
        public void OnCoreStageChanged(int stageIndex)
        {
            int maxIndex = Enum.GetValues(typeof(QuirkStage)).Length - 1;

            CurrentStage = (QuirkStage)Math.Clamp(
                stageIndex,
                0,
                maxIndex
            );

            UpdateUnlockedSkills();
        }

        #endregion


        #region Ciclo de Atualização

        /// <summary>Adjusts the maximum strain according to the current power stage</summary>
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

        /// <summary>

        /// Makes sure One for All 9th and 8th don't coexist, all for one 9th absorbs
        /// One for all 8th and sums 200 on the used time timer
        /// </summary>
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

        /// <summary>
        /// Dadges using the higher chance between Dangersense and Smokescreen.
        /// if the hit is dodged, triggers the visual and sound effect
        /// </summary>
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

        /// <summary>
        /// When entering the world, recaculates skills, the stage, if initial trais where rolled
        /// </summary>
        public override void OnEnterWorld()
        {
            UpdateUnlockedSkills();

            if (Main.LocalPlayer == Player)
            {
                CorePlayer core = Player.GetModPlayer<CorePlayer>();
                OnCoreStageChanged(core.CurrentStageIndex);
            }

            if (!hasRolledInitialTraits)
            {
                RandomNatureSelection.SelectRandomNature();
                RandomQuirkSelection.SelectRandomQuirk();
                hasRolledInitialTraits = true;
            }
        }

        #endregion


        #region Persistência (Save/Load)

        /// <summary>Saves the active quirks, the variant, the nature and if rolled initial traits.</summary>
        public override void SaveData(TagCompound tag)
        {
            List<string> quirkNames = [];

            foreach (var quirk in ActiveQuirks)
            {
                quirkNames.Add(quirk.ToString());
            }

            tag["ActiveQuirkList"] = quirkNames;
            tag["CurrentVariantName"] = CurrentVariant.ToString();
            tag["PlayerNature"] = (int)Nature;
            tag["HasRolledInitialTraits"] = hasRolledInitialTraits;
        }

        /// <summary>
        /// Loads Data. Mantains compatibility with the old architecture
        /// </summary>
        public override void LoadData(TagCompound tag)
        {
            ActiveQuirks.Clear();

            if (tag.ContainsKey("ActiveQuirkList"))
            {
                IList<string> savedQuirks =
                    tag.GetList<string>("ActiveQuirkList");

                foreach (var quirkName in savedQuirks)
                {
                    if (Enum.TryParse(quirkName, out QuirkType parsedQuirk))
                    {
                        ActiveQuirks.Add(parsedQuirk);
                    }
                }

                if (tag.ContainsKey("CurrentVariantName"))
                {
                    if (Enum.TryParse(
                        tag.GetString("CurrentVariantName"),
                        out QuirkVariant parsedVariant))
                    {
                        CurrentVariant = parsedVariant;
                    }
                }
            }
            else if (tag.ContainsKey("SelectedQuirk")) // compatibilidade com saves antigos
            {
                ActiveQuirks.Add(
                    (QuirkType)tag.GetInt("SelectedQuirk")
                );
            }

            if (tag.ContainsKey("PlayerNature"))
            {
                Nature = (NatureType)tag.GetInt("PlayerNature");
            }

            if (tag.ContainsKey("HasRolledInitialTraits"))
            {
                hasRolledInitialTraits =
                    tag.GetBool("HasRolledInitialTraits");
            }

            UpdateUnlockedSkills();
        }

        #endregion


        #region Utilitários de Quirk

        /// <summary>Friendly name for exhibition<see cref="QuirkType"/>.</summary>
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

        /// <summary>
        /// Verifies if the player has an active quirk, including stolen internal quirks
        /// via All For One or through One For All (9th)
        /// </summary>
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

        /// <summary>Recalculates from the SkillLibrary, which skills are unlocked now </summary>
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

        /// <summary>Activates the Complete reset when resetting the quirk <see cref="IQuirkResetter"/>.</summary>
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

        #endregion


        #region Multiplayer Synchronization

        /// <summary>Copies the latest relevant state to the clone using it as the "last known state" in the server.</summary>
        public override void CopyClientState(ModPlayer clientClone)
        {
            TransformationPlayer clone = clientClone as TransformationPlayer;
            clone.ActiveQuirks = [.. ActiveQuirks];
            clone.CurrentStage = CurrentStage;
            clone.CurrentVariant = CurrentVariant;
            clone.Nature = Nature;
        }

        /// <summary>Sends the complete state of the player when entering the world.</summary>
        public override void SyncPlayer(
            int toWho,
            int fromWho,
            bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();

            packet.Write(
                (byte)MyHeroMod.MessageType.SyncTransformationPlayer
            );

            packet.Write((byte)Player.whoAmI);

            packet.Write(ActiveQuirks.Count);

            foreach (var quirk in ActiveQuirks)
            {
                packet.Write((int)quirk);
            }

            packet.Write((int)CurrentVariant);
            packet.Write((int)Nature);

            packet.Send(toWho, fromWho);
        }

        /// <summary> Only sends the synchronization if something changed (delta sync).</summary>
        public override void SendClientChanges(
            ModPlayer clientPlayer)
        {
            TransformationPlayer clone =
                clientPlayer as TransformationPlayer;

            bool quirksChanged =
                ActiveQuirks.Count != clone.ActiveQuirks.Count;

            if (!quirksChanged)
            {
                for (int i = 0; i < ActiveQuirks.Count; i++)
                {
                    if (ActiveQuirks[i] != clone.ActiveQuirks[i])
                    {
                        quirksChanged = true;
                        break;
                    }
                }
            }

            if (quirksChanged ||
                CurrentVariant != clone.CurrentVariant ||
                Nature != clone.Nature)
            {
                ModPacket packet = Mod.GetPacket();

                packet.Write(
                    (byte)MyHeroMod.MessageType.SyncTransformationPlayer
                );

                packet.Write((byte)Player.whoAmI);

                packet.Write(ActiveQuirks.Count);

                foreach (var quirk in ActiveQuirks)
                {
                    packet.Write((int)quirk);
                }

                packet.Write((int)CurrentVariant);
                packet.Write((int)Nature);

                packet.Send(-1, Player.whoAmI);
            }
        }

        #endregion
    }
}
