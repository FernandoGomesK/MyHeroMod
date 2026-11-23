using KhacesCore.Content.System.BaseProjectiles;
using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs.Fajin;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.MuscleAugment
{
    public class MuscleAugmentPlayer : ModPlayer, IDashModifier, IPunchModifier
    {
        public bool isMuscleAugmentActive = false;

        public override void ResetEffects()
        {
            isMuscleAugmentActive = false;
        }

        

        public override void FrameEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (Player.HasBuff(ModContent.BuffType<content.Buffs.MuscleAugment.MuscleAugmentBuff>()))
            {
                Player.body = EquipLoader.GetEquipSlot(Mod, "MuscularBody", EquipType.Body);
            }
        }
        public void ModifyPunch(ref float projSpeed, ref int baseDamage, ref int numberOfPunches, ref int mainProjType, ref int extraProjType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            var musclePlayer = Player.GetModPlayer<MuscleAugmentPlayer>();
            if (!transPlayer.HasActiveQuirk(QuirkType.MuscleAugmentation)) 
            { 
                return;
            }

            if (musclePlayer.isMuscleAugmentActive)
            {
                projSpeed = 30;
                baseDamage = 15;
                mainProjType = ModContent.ProjectileType<PunchAttackProj>();
                extraProjType = ModContent.ProjectileType<SuperPunchProj>();
                numberOfPunches = 1;           
            }
        }
        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash,
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();


            if (!transPlayer.HasActiveQuirk(QuirkType.MuscleAugmentation)) return;

            float formModifier = 0f;
            if (isMuscleAugmentActive)
            {


                float dashSpeed = transPlayer.CurrentStage switch
                {
                    QuirkStage.Initial => 15f,
                    QuirkStage.Adequation => 20f,
                    QuirkStage.Intermediate => 25f,
                    QuirkStage.Advanced => 45f,
                    QuirkStage.Final => 50f,
                    _ => 50f
                };
                if (transPlayer.CurrentStage >= QuirkStage.Adequation)
                {
                    isEnhanced = true;
                    dustType = DustID.Cloud;
                    explosionColor = Color.White;
                }


                speed = dashSpeed + formModifier;

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
            }
        }

        public override void PostUpdateMiscEffects()
        {
        }


        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if (isMuscleAugmentActive)
            {
                //PlayerDrawLayers.Torso.Hide();
                //PlayerDrawLayers.ArmOverItem.Hide();
                //PlayerDrawLayers.ArmorLongCoat.Hide();

            }
        }

     
        
    }
}