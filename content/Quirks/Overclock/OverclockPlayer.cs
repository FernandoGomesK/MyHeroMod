using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using KhacesCore.Content.System.Interfaces;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content.Quirks.Overclock
{
    public partial class OverclockPlayer : ModPlayer, IDashModifier, IQuirkResetter
    {
        public int form = 0;
        public bool isOverclockBuffActive = false;

        public int maxBreath = 0;
        public float currentBreath = 90f;
        public float breathDrainRate = 1f;

        private int ElectricSoundTimer = 0;

        public Vector2[] oldPos = new Vector2[10];

        public void FullReset()
        {
            isOverclockBuffActive = false;
        }

        public override void OnRespawn()
        {
            currentBreath = maxBreath;
            isOverclockBuffActive = false;
        }

        public override void PreUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            int baseBreath = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 80,
                QuirkStage.Adequation => 100,
                QuirkStage.Intermediate => 150,
                QuirkStage.Advanced => 240,
                QuirkStage.Final => 300,
                _ => 90
            };

            int bonusBreath = 0;

            if (transPlayer.Nature == NatureType.HigherBrainPower)
            {
                bonusBreath = 50;
            }

            maxBreath = baseBreath + bonusBreath;

            if (currentBreath > maxBreath)
            {
                currentBreath = maxBreath;
            }


            if (isOverclockBuffActive)
            {
                for (int i = oldPos.Length - 1; i > 0; i--)
                {
                    oldPos[i] = oldPos[i - 1];
                }
                oldPos[0] = Player.position;
            }
            else
            {
                for (int i = 0; i < oldPos.Length; i++)
                {
                    oldPos[i] = Player.position;
                }
            }
        }

        public override void ResetEffects()
        {
            isOverclockBuffActive = false;
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            breathDrainRate = 1f;

            if (transPlayer.Nature == NatureType.HigherBrainPower)
            {
                breathDrainRate = 0.66f;
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (isOverclockBuffActive)
                Player.GetModPlayer<AfterimagePlayer>().Request(AfterimagePalettes.Rainbow);
        }

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.Overclock)) return;

            if (Player.HasBuff(ModContent.BuffType<OverclockBuff>()))
            {
                isOverclockBuffActive = true; 
                currentBreath -= breathDrainRate;

                if (currentBreath <= 0)
                {
                    currentBreath = 0;
                    isOverclockBuffActive = false;
                    Player.ClearBuff(ModContent.BuffType<OverclockBuff>());
                    
                    if (transPlayer.CurrentVariant == QuirkVariant.Variant1)
                    {
                        CombatText.NewText(Player.getRect(), Color.Red, "STOP!");
                    }
                    else
                    {
                        CombatText.NewText(Player.getRect(), Color.Red, "BREATH!");
                    }
                    
                }
            }
            else
            {
                if (currentBreath < maxBreath)
                {
                    currentBreath += 1f;
                }
            }
        }
    }
}