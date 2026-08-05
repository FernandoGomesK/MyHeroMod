using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using MyHeroMod.content.Buffs;
using Terraria.Audio;
using MyHeroMod.content.System;
using Terraria.GameContent.Bestiary;
using MyHeroMod.content.Quirks.OFA9th;


namespace MyHeroMod.content.Quirks.DangerSense;

    public partial class DangerSensePlayer : ModPlayer, IHeroDodgeModifier, IQuirkResetter
    {
        
        
        public bool isDangerSenseActive = false;
        public bool IsOvertimeActive = false;

        public int overtimeTimer = 0;
        public int overtimeMaxTimer = 220;
        
        public int VisualTimer = 0;
        public int VisualMaxTimer = 8;

        public float dodgeChance = 0;
        public QuirkStage CurrentStage => Player.GetModPlayer<TransformationPlayer>().CurrentStage;

        public void FullReset()
    {
     overtimeTimer = 0;
     isDangerSenseActive = false;
     isDangerSenseActive = false;
     VisualTimer = 0;   
    }

        public bool HasDangerSenseAccess()
        {
        var transPlayer = Player.GetModPlayer<TransformationPlayer>();
        
        if (transPlayer.HasActiveQuirk(QuirkType.DangerSense))
        {
            return true;
        }

        if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
        {
            var ofaPlayer = Player.GetModPlayer<OneForAll9thPlayer>();
            if (ofaPlayer.HasInternalQuirk(QuirkType.DangerSense))
            return true;
        }
        return false;
        }

        public override void OnRespawn()
        {
            isDangerSenseActive = false;
            IsOvertimeActive = false;
        }

        
        public bool TryDodge(Player.HurtInfo info) 
        {
           
            if (Main.rand.NextFloat() < dodgeChance)
            {
               {
            Player.SetImmuneTimeForAllTypes(80); 
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/DangerSenseSound") with { Volume = 2.0f }, Player.position);
            triggerVisual(); 
            return true; 
        }
        }
            return false;
        }
                 
                

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

        if (!HasDangerSenseAccess()) return;
  
        if (isDangerSenseActive)
        {
            Player.AddBuff(ModContent.BuffType<DangerSenseBuff>(), 10);
        }
}

        public override void ResetEffects()
        {
            
        

            if (!HasDangerSenseAccess())
            {
                isDangerSenseActive = false;
                return;
            }   

            if (!isDangerSenseActive)
            {
                dodgeChance = 0f;
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
    

