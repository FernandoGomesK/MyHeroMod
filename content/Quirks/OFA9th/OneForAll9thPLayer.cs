using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th.Buffs;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles.BlackWhip;
using MyHeroMod.content.System;
using Terraria.Audio;
using System.Collections.Generic;
using System.Runtime.Serialization;




namespace MyHeroMod.content.Quirks.OFA9th
{
    public partial class OneForAll9thPlayer : ModPlayer
    {
        public bool isGearshiftActive = false;
        public bool isGearshiftBuffActive = false;

        public int GearshiftTimer = 0;

        public int GearshiftMaxTime = 6000;

        public bool isFullCowlingBuffActive = false;

        public bool isDangerSenseActive = false;

        public bool isFloatActive = false;

        public int Fingers = 10;

        public int ParallelProcessing = 0;
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();
        private int ElectricSoundTimer = 0;

        public int ActivationTimer = 0;
        public int ActivationMaxTime = 40;
        private QuirkSkills PendingForm = QuirkSkills.None;

        // Resetar no renascer

        public override void OnRespawn()
        {
            Fingers = 10;
            Player.GetModPlayer<TransformationPlayer>().ActiveForm = QuirkSkills.None;
            SkillCooldowns.Clear();
            ElectricSoundTimer = 0;
            ActivationTimer = 0;
            GearshiftTimer = 0;
            PendingForm = QuirkSkills.None;
        }

        public override void PreUpdate()
        {
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0)
                {
                    SkillCooldowns[skill]--;
                }
            }
            if (isGearshiftActive)
            {
                GearshiftTimer++;
                if (GearshiftTimer >= GearshiftMaxTime)
                {
                    isGearshiftActive = false;
                    isGearshiftBuffActive = false;
                    Main.NewText("Gearshift Deactivated due to limit!", Color.White);
                }
            }
            if (ActivationTimer > 0)
            {
                ActivationTimer++;
                Player.velocity *= 0.6f;

                if (ActivationTimer >= ActivationMaxTime)
                {
                    var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
                    mainPlayer.ActiveForm = PendingForm;

                    ActivationTimer = 0;
                    PendingForm = QuirkSkills.None;
                }
                
            }
        }

        public override void PostUpdate()
        {
            if (isFloatActive && !Player.mount.Active && Player.velocity.Y != 0)
            {
            // If holding JUMP, stop falling (Hover)
            if (Player.controlJump) 
            {
            Player.velocity.Y = 0f; 
            Player.fallStart = (int)(Player.position.Y / 16f); // Prevents fall damage accumulating
            }
        // If NOT holding jump, fall very slowly (feather fall)
            else if (Player.velocity.Y > 0)
            {
            Player.velocity.Y *= 0.2f; // Slows down falling speed significantly
            }
            }
        }


        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (isDangerSenseActive)
            {
                Player.AddBuff(ModContent.BuffType<DangerSenseBuff>(), 2);
            }

            
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && mainPlayer.ActiveForm != QuirkSkills.None)
            {
                Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 2);
                Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
                ElectricSoundTimer++;

                if (ElectricSoundTimer >= 900 + Main.rand.Next(-120, 120))
                {
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingAura"), Player.position);
                    ElectricSoundTimer = 0;

                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, default, 0.5f);
                }
            }
            else
            {
                isFullCowlingBuffActive = false;
            }
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && isGearshiftActive)
            {
                Player.AddBuff(ModContent.BuffType<GearshiftBuff>(), 2);
                
            }
            else
            {
                isGearshiftActive = false;
                isGearshiftBuffActive = false;
                
            }
        }
        

    }
}

