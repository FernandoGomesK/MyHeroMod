using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;


namespace MyHeroMod.content.Quirks.DangerSense;

    public partial class DangerSensePlayer : BasePlayer
    {
        
        
        public bool IsDangerSenseActive = false;
        public bool IsOvertimeActive = false;

        public int overtimeTimer = 0;
        public int overtimeMaxTimer = 220;
        
        public int VisualTimer = 0;
        public int VisualMaxTimer = 8;
        public QuirkStage CurrentStage => Player.GetModPlayer<TransformationPlayer>().CurrentStage;

        
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

            
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if ( transPlayer.SelectedQuirk == QuirkType.DangerSense && transPlayer.CurrentStage >= QuirkStage.Adequation)
        {
            IsDangerSenseActive = true;
        }

            
        }
        
        public override void PreUpdate()
        {
            if (VisualTimer > 0)
            {
                VisualTimer--;
            }
        }
        

        public void triggerVisual()
        {
            VisualTimer = VisualMaxTimer;
        }
    }
    

