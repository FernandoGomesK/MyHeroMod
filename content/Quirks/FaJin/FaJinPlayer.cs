using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.OFA9th.Buffs;
using Terraria.Audio;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OFA9th;


namespace MyHeroMod.content.Quirks.FaJin;

    public partial class FajinPlayer : ModPlayer, IHeroDashModifier, IQuirkResetter
    {
        
        public int FaJinCharges = 0;
        public int MaxFaJinCharges = 5;
        public bool isFaJinActive = false;
        public bool FaJinStored => FaJinCharges >= MaxFaJinCharges;

        public override void OnRespawn()
        {
            FaJinCharges = 0;
            isFaJinActive = false;
            Player.ClearBuff(ModContent.BuffType<FaJinBuff>());
            Player.ClearBuff(ModContent.BuffType<FaJinActiveBuff>());

        }


        public override void ResetEffects()
        {
            
            isFaJinActive = false;
        }

        public void FullReset()
    {
        FaJinCharges = 0; 
        isFaJinActive = false;
        Player.ClearBuff(ModContent.BuffType<FaJinBuff>());
        Player.ClearBuff(ModContent.BuffType<FaJinActiveBuff>());
    }

        

        public bool HasFaJinAccess()
        {
        var transPlayer = Player.GetModPlayer<TransformationPlayer>();
        
        if (transPlayer.SelectedQuirk == QuirkType.FaJin)
        {
            return true;
        }

        if (transPlayer.SelectedQuirk == QuirkType.OneForAll9th)
        {
            var ofaPlayer = Player.GetModPlayer<OneForAll9thPlayer>();
            if (ofaPlayer.HasInternalQuirk(QuirkType.FaJin))
            return true;
        }
        return false;
        }

        public override void PostUpdateEquips()
        {
            if (FaJinStored) {
            Player.AddBuff(ModContent.BuffType<FaJinBuff>(), 2);
            }
        }
       public override void PostUpdateMiscEffects() {


        var transformationPlayer = Player.GetModPlayer<TransformationPlayer>();

        if (HasFaJinAccess()) 
        {
            if (isFaJinActive)
            {
                
            
            if (!FaJinStored && Player.controlJump && Player.velocity.Y == 0 && Player.releaseJump) {
                ChargeFajin( );
            }
            }
        }
        }

    

        public void ChargeFajin() {
            FaJinCharges++;
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinStoringSound"), Player.position);
            CombatText.NewText(Player.getRect(), Color.Orange, $"Fa Jin: {FaJinCharges}/{MaxFaJinCharges}");

            if (FaJinCharges >= MaxFaJinCharges) {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinSound"), Player.position);
                Main.NewText("Fa Jin storage is full!", Color.Red);
            }
        

        
    }


      
    }


    

