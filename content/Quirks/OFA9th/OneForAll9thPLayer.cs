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

        // Fa jin

        public int FaJinCharges = 0;
        public int MaxFaJinCharges = 3;
        public bool FaJinStored = false;

        // Gearshift
        public bool isGearshiftActive = false;
        public bool isGearshiftBuffActive = false;
        public int GearshiftTimer = 0;
        public int GearshiftMaxTime = 6000;
        // Gearshift Buff
        public bool GearActivation = false;

        // Full Cowling
        public bool isFullCowlingBuffActive = false;

        // Danger Sense
        public bool isDangerSenseActive = false;
        // Smoke Screen
        public bool isSmokeScreenActive = false;

        // Float
        public bool isFloatActive = false;

        // Fingers

        public int Fingers = 10;

        // Parallel Processing
        public int ParallelProcessing = 0;
        public int MaxParallelProcessing = 0;
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();


        private int ElectricSoundTimer = 0;

        public int ActivationTimer = 0;
        public int ActivationMaxTime = 40;
        public QuirkSkills PendingForm = QuirkSkills.None;

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
            // Gerenciamento de Cooldown
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }

            // Timer de Duração do Gearshift
            if (isGearshiftActive)
            {
                GearshiftTimer++;
                if (GearshiftTimer >= GearshiftMaxTime)
                {
                    isGearshiftActive = false;
                    isGearshiftBuffActive = false;
                    Main.NewText("Gearshift Deactivated due to limit!", Color.White);
                    SetCooldown(QuirkSkills.Gearshift, 6000);
                    GearshiftTimer = 0;
                }
            }

            // --- LÓGICA DE TRANSFORMAÇÃO (Full Cowling e Gearshift) ---
            if (ActivationTimer > 0)
            {
                ActivationTimer++;
                Player.velocity *= 0.6f; // Efeito de "carregar" (freia o jogador)

                // Visual durante o carregamento
                if (GearActivation)
                {
                    // Partículas Ciano para Gearshift
                    if (Main.rand.NextBool(2))
                    {
                        Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, Color.Cyan, 0.3f);
                        d.velocity *= 2f;
                        d.noGravity = true;
                    }
                }

                // Transformação Completa
                if (ActivationTimer >= ActivationMaxTime)
                {
                    // 1. Se for Full Cowling
                    if (PendingForm != QuirkSkills.None)
                    {
                        var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
                        mainPlayer.ActiveForm = PendingForm;
                        PendingForm = QuirkSkills.None;
                        
                    }

                    // 2. Se for Gearshift
                    if (GearActivation)
                    {
                        isGearshiftActive = true;
                        GearActivation = false;
                        GearshiftTimer = 0;

                        // EFEITOS FINAIS DA ATIVAÇÃO
                        Main.NewText("ONE FOR ALL 2ND - GEARSHIFT: TRANSMISSION !", Color.Cyan);
                        CombatText.NewText(Player.getRect(), Color.Cyan, "SECOND GEAR");
                        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/GearShiftSound"), Player.position);

                        // Explosão de partículas
                        for (int i = 0; i < 20; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2Circular(5f, 5f);
                            Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, speed.X, speed.Y, 0, Color.Cyan, 2f);
                        }
                    }
                    ActivationTimer = 0;

                    
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

        public override void ResetEffects()
        {
            // 1. Definir o Limite baseado no Estágio da Quirk
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            // 1. Zera a contagem para recalcular neste frame
            ParallelProcessing = 0;

            // 2. Conta quantas habilidades passivas estão ativas
            if (isFloatActive) ParallelProcessing++;
            if (isDangerSenseActive) ParallelProcessing++;
            if (isGearshiftActive) ParallelProcessing++;
            if (isSmokeScreenActive) ParallelProcessing++;

            // 3. Define o Limite Máximo baseado no Estágio (Progresso)
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th)
            {
                // AQUI VOCÊ CONTROLA A EVOLUÇÃO
                if (mainPlayer.CurrentStage == QuirkStage.Initial) 
                    MaxParallelProcessing = 0; // Nenhuma extra
                else if (mainPlayer.CurrentStage == QuirkStage.Adequation) 
                    MaxParallelProcessing = 1; // Consegue manter 1
                else if (mainPlayer.CurrentStage == QuirkStage.Intermediate) 
                    MaxParallelProcessing = 2; // Consegue manter 2
                else if (mainPlayer.CurrentStage == QuirkStage.Advanced) 
                    MaxParallelProcessing = 4; // Consegue manter 4
                else if (mainPlayer.CurrentStage >= QuirkStage.Final) 
                    MaxParallelProcessing = 6; // Consegue manter todas (6)
            }
            else
            {
                MaxParallelProcessing = 0;
            }
        }
        
        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (FaJinCharges >= MaxFaJinCharges)
            {
                FaJinStored = true;
                Player.AddBuff(ModContent.BuffType<FaJinBuff>(), 2);
            }
            else
            {
                FaJinStored = false;
            }

            if (ParallelProcessing > 0)
            {
                Player.AddBuff(ModContent.BuffType<ParallelProcessingBuff>(), 2);
            }

            if (isDangerSenseActive) Player.AddBuff(ModContent.BuffType<DangerSenseBuff>(), 2);

            if (isSmokeScreenActive)
            {
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Smoke, 0f, 0f, 100, Color.MediumPurple, 6.0f);
            }
            

            
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && mainPlayer.ActiveForm != QuirkSkills.None)
            {
                Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 2);
                HandleFullCowlingEffects();
                Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
                ElectricSoundTimer++;
           
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

