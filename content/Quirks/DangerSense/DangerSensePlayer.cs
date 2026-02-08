using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.System;
using Terraria.Audio;
using System.Collections.Generic;
using MyHeroMod.content.Dusts;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;


namespace MyHeroMod.content.Quirks.DangerSense;

    public partial class DangerSensePlayer : ModPlayer
    {
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

        public bool IsDangerSenseActive = false;
        public bool IsOvertimeActive = false;

        public int overtimeTimer = 0;
        public int overtimeMaxTimer = 220;
        
        public int VisualTimer = 0;
        public int VisualMaxTimer = 8;

        


        public override void OnRespawn()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if ( mainPlayer.CurrentStage >= QuirkStage.Adequation)
        {
            IsDangerSenseActive = true;
        }
        else
        {
            IsDangerSenseActive = false; 
        }
           
            IsOvertimeActive = false;
            SkillCooldowns.Clear();
        }

        

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();


            if (mainPlayer.SelectedQuirk != QuirkType.DangerSense)
                return;
  
            if (IsDangerSenseActive)
            {
                Player.AddBuff(ModContent.BuffType<DangerSenseBuff>(),10 );
            }
        }

        

        public override void ResetEffects()
        {
            
            var ModPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se é Explosão e se o estágio é Adequation ou maior
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if ( transPlayer.SelectedQuirk == QuirkType.DangerSense && transPlayer.CurrentStage >= QuirkStage.Adequation)
        {
            IsDangerSenseActive = true;
        }

            // Verifica se é Explosão e se o estágio é Adequation ou maior
            
        }
        
        public override void PreUpdate()
        {
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }
            if (VisualTimer > 0)
            {
                VisualTimer--;
            }
        }
        public override void PostUpdate()
        {
            
           
        }

        public void triggerVisual()
    {
        VisualTimer = VisualMaxTimer;
    }
    }
    

