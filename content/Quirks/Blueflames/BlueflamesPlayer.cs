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

using MyHeroMod.content.Quirks.Blueflames.Buffs;

namespace MyHeroMod.content.Quirks.Blueflames
{
    public partial class BlueFlamesPlayer : ModPlayer
    {
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

        // Heat Logic
        public int MaxHeat = 100;
        public int CurrentHeat = 0;
        public int HeatTimer = 0;
        

        // Buffs

        public bool IsFlashFireFistActive = false;
        public bool IsRageActive = false;
        public bool IsPhosphorActive = false;

        public override void PreUpdate()
        {
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }
        }



        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (mainPlayer.SelectedQuirk != QuirkType.BlueFlames)  
                return;

            if (CurrentHeat > 0)
            {
                Player.AddBuff(ModContent.BuffType<BlueHeatBuff>(), 2);
            }
             

            // Verifica se a individualidade atual é Blue Flames
            if (mainPlayer.CurrentStage >= QuirkStage.Adequation && mainPlayer.SelectedQuirk == QuirkType.BlueFlames)
            {
                // 1. Define o tempo de voo (100 = voo curto/médio)
                Player.wingTimeMax = 50;

                // 2. Se o jogador NÃO tiver asas equipadas, simula uma
                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29; // Física das Solar Wings
                    Player.wings = -1; // Esconde o sprite da asa
                }

                // 3. Anula dano de queda
                Player.noFallDmg = true;
            }
            if (IsFlashFireFistActive)
            {
                Player.AddBuff(ModContent.BuffType<Buffs.BlueFlashFireFistBuff>(), 2);
            }
            if (IsRageActive)
            {
                Player.AddBuff(ModContent.BuffType<Buffs.BlueRage>(), 2);
            }
        }
        

        public override void PostUpdate()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (mainPlayer.SelectedQuirk == QuirkType.BlueFlames)
            {
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
                        Main.dust[dustFire].velocity *= 0.5f; // Suaviza o movimento
                    }

                    // Lado Direito (Fogo tbm)
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


                if (CurrentHeat > 0)
            {
                HeatTimer++; // Conta +1 frame

                // 60 frames = 1 segundo
                if (HeatTimer >= 60)
                {
                    HeatTimer = 0; // Reseta
                    
                    // 1. Diminui o calor
                    CurrentHeat -= 1;

                    if (CurrentHeat >= 50)
                        {
                            Player.statLife -= 5;
                            if (Player.statLife <= 0)
                            {
                                var reason = PlayerDeathReason.ByCustomReason(
                                Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.BlueFireDeathMessage", Player.name));
                                Player.KillMe(reason, 5, 0);
                            }
                    if (CurrentHeat >= MaxHeat)
                            {
                                Player.AddBuff(ModContent.BuffType<Heatstroke>(), 300);
                            }
                            
                        }
                }
            }
            else
            {
                HeatTimer = 0; // Garante que o timer não rode se não tiver calor
            }
            }
        }
    }
}