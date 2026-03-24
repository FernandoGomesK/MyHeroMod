using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.System;
using Terraria.Audio;
using System.Collections.Generic;
using MyHeroMod.content.Debuffs;


using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content.Quirks.Blueflames
{
    public partial class BlueFlamesPlayer : ModPlayer, IQuirkResetter, IHeroTemperature
    {

        
        // public int MaxHeat = 100;
        // public int CurrentHeat = 0;

        public int Temperature {get; set;} = 0;
        public int MaxTemperature { get; } = 100;
        public int MinTemperature { get; } = -50;        
        public int HeatTimer = 0;

        public void AddHeat(int amount)
        {
            Temperature += amount;
            if (Temperature > MaxTemperature) Temperature = MaxTemperature;
        }
        public void ReduceHeat(int amount)
        {
            Temperature -= amount;
            if (Temperature < MinTemperature) Temperature = MinTemperature;
        }

        

        public bool IsFlashFireFistActive = false;
        public bool IsRageActive = false;
        public bool IsPhosphorActive = false;

        public void FullReset()
        {
            Temperature = 0;
            HeatTimer = 0;
            IsFlashFireFistActive = false;
            IsRageActive = false;
            IsPhosphorActive = false;
            Player.ClearBuff(ModContent.BuffType<PhosphorBuff>());
            Player.ClearBuff(ModContent.BuffType<FlashFireFistBuff>());
        }

        public override void PreUpdate()
        {
            
            IsFlashFireFistActive = false;
        }



        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!mainPlayer.HasActiveQuirk(QuirkType.BlueFlames))  
                return;

            if (Temperature > 0)
            {
                Player.AddBuff(ModContent.BuffType<TemperatureBuff>(), 2);
            }
             

            
            if (mainPlayer.CurrentStage >= QuirkStage.Adequation && mainPlayer.SelectedQuirk == QuirkType.BlueFlames)
            {
                
                Player.wingTimeMax = 50;

                
                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29; 
                }

                // 3. Anula dano de queda
                Player.noFallDmg = true;
            }
            // if (IsFlashFireFistActive)
            // {
            //     Player.AddBuff(ModContent.BuffType<Buffs.FlashFireFistBuff>(), 2);
            // }
            // if (IsRageActive)
            // {
            //     Player.AddBuff(ModContent.BuffType<BlueRage>(), 2);
            // }
        }

        public override void OnRespawn()
        {
            Temperature = 0;
            
        }

        public override void PostUpdate()
        {
             var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!mainPlayer.HasActiveQuirk(QuirkType.BlueFlames))  
                return;

                // CORREÇÃO AQUI: Substituí !Player.onFloor por Player.velocity.Y != 0
                bool isFlying = (Player.velocity.Y != 0) && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active;

                // Se estiver apenas caindo ou voando (sem estar montado)
                if (Player.velocity.Y != 0 && !Player.mount.Active)
                {
                    // Lado Esquerdo (Fogo)
                    if (Main.rand.NextBool(2)) 
                    {
                        int dustFire = Dust.NewDust(
                            Player.position + new Vector2(-5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.BlueTorch, 
                            0, 2f, 100, default, 1.5f 
                        );
                        Main.dust[dustFire].noGravity = true;
                        Main.dust[dustFire].velocity *= 0.5f; 
                    }

                    
                    if (Main.rand.NextBool(2))
                    {
                        int dustIce = Dust.NewDust(
                            Player.position + new Vector2(Player.width / 2, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.BlueTorch, 
                            0, 2f, 100, default, 1.5f
                        );
                        Main.dust[dustIce].noGravity = true;
                        Main.dust[dustIce].velocity *= 0.5f;
                    }
                }


                if (Temperature > 0)
            {
                HeatTimer++; 

                
                if (HeatTimer >= 60)
                {
                    HeatTimer = 0;
                    
                    
                   Temperature -= 1;

                    if (Temperature >= 50)
                    {
                        
                        Player.lifeRegenTime = 0; 
                        
                        // Subtrai a vida
                        Player.statLife -= 20;
                        
                        
                        CombatText.NewText(Player.getRect(), Color.Cyan, 20, false, true);

                        if (Player.statLife <= 0)
                        {
                            var reason = PlayerDeathReason.ByCustomReason(
                                Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.BlueFireDeathMessage", Player.name));
                            Player.KillMe(reason, 5, 0);
                        }
                        
                        if (Temperature >= MaxTemperature)
                        {
                            Player.AddBuff(ModContent.BuffType<Heatstroke>(), 300);
                        }
                    }
        }}}}}
