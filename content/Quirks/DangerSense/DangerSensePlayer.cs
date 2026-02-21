using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.BasePlayer;
using Terraria.Audio;
using MyHeroMod.content.System;


namespace MyHeroMod.content.Quirks.DangerSense;

    public partial class DangerSensePlayer : ModPlayer, IHeroDodgeModifier
    {
        
        
        public bool IsDangerSenseActive = false;
        public bool IsOvertimeActive = false;

        public int overtimeTimer = 0;
        public int overtimeMaxTimer = 220;
        
        public int VisualTimer = 0;
        public int VisualMaxTimer = 8;

        public float dodgeChance = 0;
        public QuirkStage CurrentStage => Player.GetModPlayer<TransformationPlayer>().CurrentStage;

        public override void OnRespawn()
        {
            var mainPlayer = Player.GetModPlayer<DangerSensePlayer>();
            if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                IsDangerSenseActive = true;
            }
            else
            {
                IsDangerSenseActive = false; 
            }
            
            IsOvertimeActive = false;
           

        }

        
        public bool TryDodge(Player.HurtInfo info) 
        {
           
            
            if (Main.rand.NextFloat() < dodgeChance)
            {
               {
            Player.SetImmuneTimeForAllTypes(80); 
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/DangerSenseSound"), Player.position);
            triggerVisual(); 
            return true; 
        }
        }
            return false;
        }
                 
                
                
        

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (mainPlayer.SelectedQuirk != QuirkType.DangerSense) return;
  
            if (IsDangerSenseActive)
            {
                Player.AddBuff(ModContent.BuffType<DangerSenseBuff>(), 10);
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
            base.PreUpdate();
            
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
    

