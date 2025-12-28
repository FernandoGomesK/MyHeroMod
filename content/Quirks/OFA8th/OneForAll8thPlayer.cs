using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.Quirks;
using MyHeroMod.content.Quirks.OFA8th.Projectiles;
using Terraria.Audio;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Quirks.OFA8th
{
    public class OneForAll8thPlayer : ModPlayer
    {
         public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
        

            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll8th)
            {
                if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot1);
                if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot2);
                if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot3);
                if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.TransformSlot);
            }
        }
        private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
        {
            switch (skill)
            {
                case QuirkSkills.SuperJump:
                    DoSuperJump(mainPlayer);
                    break;
                case QuirkSkills.PrimeDetroitSmash:
                    DoPrimeDetroitSmash(mainPlayer);
                    break;
                case QuirkSkills.StockPile:
                    ToggleForm(mainPlayer, QuirkSkills.StockPile);
                    break;
                case QuirkSkills.StockPileMaximum:
                    ToggleForm(mainPlayer, QuirkSkills.StockPileMaximum);
                    break;

            }
        }
        private void DoPrimeDetroitSmash(TransformationPlayer mainPlayer)
        {

            int damage = 130;    
            if (mainPlayer.CurrentStage == QuirkStage.Adequation) damage = 130;
                else if (mainPlayer.CurrentStage == QuirkStage.Intermediate) damage = 150;
                else if (mainPlayer.CurrentStage == QuirkStage.Advanced) damage = 200;
                else if (mainPlayer.CurrentStage == QuirkStage.Final) damage = 300;

                if (mainPlayer.ActiveForm == QuirkSkills.StockPile) damage += 50;
                if (mainPlayer.ActiveForm == QuirkSkills.StockPileMaximum) damage += 150;
            
            
           
            Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Velocity, ModContent.ProjectileType<PrimeDetroitSmashProj>(), damage, 2f, Player.whoAmI);

            
        }
        private void DoSuperJump(TransformationPlayer mainPlayer)
        {
            
            Player.velocity.Y = -15f;
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, 0, 5, 100, default, 1.5f);
            }   
        }
        private void ToggleForm(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {
            if (mainPlayer.ActiveForm == targetForm)
            {
                mainPlayer.ActiveForm = QuirkSkills.None;
                Main.NewText("Reverted to normal form.", Color.White);
            }
            else
            {
                if (targetForm == QuirkSkills.StockPile && mainPlayer.CurrentStage < QuirkStage.Intermediate)
                {
                    Main.NewText("You don't quite get how to use all of your power yet.", Color.Red);
                    return;
                }
                if (targetForm == QuirkSkills.StockPileMaximum && mainPlayer.CurrentStage < QuirkStage.Advanced)
                {
                    Main.NewText("You haven't mastered all of your power yet.", Color.Red);
                    return;
                }
                mainPlayer.ActiveForm = targetForm;
                Main.NewText($"Transformed into {SkillData.SkillList[targetForm].Name} form!", Color.Yellow);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), Player.position);

                
                
                // Efeito de fumaça na transformação
                for(int i=0; i<30; i++) Dust.NewDust(Player.position, Player.width, Player.height, DustID.Smoke, 0,0, 100, default, 2f);
            }
        }
            public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Só roda se for o All Might e estiver transformado
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll8th && mainPlayer.ActiveForm != QuirkSkills.None)
            {
                // 1. Aplica o Buff de Status (Defesa/Dano)
                Player.AddBuff(ModContent.BuffType<StockPileBuff>(), 2);

                
                
            }
        }
    }
}


        