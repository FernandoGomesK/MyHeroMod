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
using MyHeroMod.content.Quirks.Explosion.Buffs;

namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer
    {
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

        public bool IsClusterActive = false;

        public int MaxSweat = 100;  
        public int CurrentSweat = 0;

        public bool IsGrenadierBracersOn = false;
        public bool IsStrafePanzerOn = false;


        public override void OnRespawn()
        {
            IsClusterActive = false;
            SkillCooldowns.Clear();
        }

        

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

             if (CurrentSweat > 0) {
                Player.AddBuff(ModContent.BuffType<Buffs.SweatBuff>(), 2);
                
            }

            if (mainPlayer.SelectedQuirk != QuirkType.Explosion)
                return;
            
            if (IsClusterActive)
            {
                Player.AddBuff(ModContent.BuffType<ClusterBuff>(),2 );
            }

            if (mainPlayer.CurrentStage >= QuirkStage.Advanced && mainPlayer.SelectedQuirk == QuirkType.Explosion && IsClusterActive)
            {
                Player.wingTimeMax = 150;

                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29;
                    Player.wings = -1;
                }
                Player.noFallDmg = true;
            }
            else if (mainPlayer.CurrentStage >= QuirkStage.Adequation && mainPlayer.SelectedQuirk == QuirkType.Explosion)
            {
                Player.wingTimeMax = 30;

                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29;
                    Player.wings = -1;
                }

                Player.noFallDmg = true;
            }
            
           
        }

        

        public override void ResetEffects()
        {
            
            var ModPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se é Explosão e se o estágio é Adequation ou maior
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se é Explosão e se o estágio é Adequation ou maior
            if (transPlayer.SelectedQuirk == QuirkType.Explosion && transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                // "Player" é a referência correta ao jogador dono deste ModPlayer
                Player.noFallDmg = true; 
            }
            IsGrenadierBracersOn = false;
            IsStrafePanzerOn = false;
        }
        
        public override void PreUpdate()
        {
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }
        }
        public override void PostUpdate()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (mainPlayer.SelectedQuirk == QuirkType.Explosion && mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
            if (Player.velocity.Y != 0 && !Player.mount.Active)
                {
                    // Lado Esquerdo (Fogo)
                    if (Main.rand.NextBool(10)) 
                    {
                        int dustFire = Dust.NewDust(
                            Player.position + new Vector2(-5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Torch, 
                            0, 2f, 100, default, 3.5f 
                        );
                        int dustSmoke = Dust.NewDust(
                            Player.position + new Vector2(-5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Ash, 
                            0, 2f, 100, default, 3.5f 
                        );
                        Main.dust[dustFire].noGravity = true;
                        Main.dust[dustFire].velocity *= 0.5f; // Suaviza o movimento
                        Main.dust[dustSmoke].noGravity = true;
                        Main.dust[dustSmoke].velocity *= 0.5f;
                    }

                    // Lado Direito (Fogo tbm)
                    if (Main.rand.NextBool(10))
                    {
                        int dustFire2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / 2, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Torch, 
                            0, 2f, 100, default, 3.5f
                        );
                        int dustSmoke2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / -5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            DustID.Ash, 
                            0, 2f, 100, default, 3.5f 
                        );
                        Main.dust[dustFire2].noGravity = true;
                        Main.dust[dustFire2].velocity *= 0.5f;
                        Main.dust[dustSmoke2].noGravity = true;
                        Main.dust[dustSmoke2].velocity *= 0.5f;
                    }
                    // cluster
                    if (Main.rand.NextBool(6) && IsClusterActive)
                    {
                         int dustFire2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / 2, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            ModContent.DustType<ClusterDust>(), 
                            0, 2f, 100, default, 2.5f
                        );
                        int dustSmoke2 = Dust.NewDust(
                            Player.position + new Vector2(Player.width / -5, Player.height - 10), 
                            Player.width / 2, 
                            10, 
                            ModContent.DustType<ClusterDust>(), 
                            0, 2f, 100, default, 2.5f 
                        );
                        Main.dust[dustFire2].noGravity = true;
                        Main.dust[dustFire2].velocity *= 0.5f;
                        Main.dust[dustSmoke2].noGravity = true;
                        Main.dust[dustSmoke2].velocity *= 0.5f;
                        
                    }
                }
           
            
        }
    }
    }
    }

