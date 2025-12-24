using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public class OneForAll9thPlayer : ModPlayer
    {

        public bool isFullCowlingBuffActive = false;

        public int Fingers = 10;



        public override void OnRespawn()
        {
            Fingers = 10;
            Player.GetModPlayer<TransformationPlayer>().ActiveForm = OfaSkills.None;
        }

        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (KeybindSystem.SkillMenu.JustPressed)
        {
        UISystem.ToggleSkillMenu();
        }

            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th)
            {
                if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot1);
                if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot2);
                if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.Slot3);
                if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(mainPlayer, mainPlayer.TransformSlot);
            }
        }

        private void ExecuteSkill(TransformationPlayer mainPlayer, OfaSkills skill)
        {
            switch (skill)
            {
                case OfaSkills.SuperJump:
                    DoSuperJump(mainPlayer);
                    break;
                case OfaSkills.DelawareSmash:
                    DoDelawareSmash(mainPlayer);
                    break;
                case OfaSkills.DetroitSmash:
                    DoDetroitSmash(mainPlayer);
                    break;
                case OfaSkills.OneForAllFullCowling5:
                    ToggleForm(mainPlayer, OfaSkills.OneForAllFullCowling5);
                    break;
                case OfaSkills.OneForAllFullCowling8:
                    ToggleForm(mainPlayer, OfaSkills.OneForAllFullCowling8);
                    break;
            }
        }
        private void ToggleForm(TransformationPlayer mainPlayer, OfaSkills targetForm)
        {
            if (mainPlayer.ActiveForm == targetForm)
            {
                mainPlayer.ActiveForm = OfaSkills.None;
                Main.NewText("Reverted to normal form.", Color.White);
            }
            else
            {
                if (targetForm == OfaSkills.OneForAllFullCowling5 && mainPlayer.CurrentStage < QuirkStage.Adequation)
                {
                    Main.NewText("You don't quite get how to use Full Cowling yet.", Color.Red);
                    return;
                }
                if (targetForm == OfaSkills.OneForAllFullCowling8 && mainPlayer.CurrentStage < QuirkStage.Intermediate)
                {
                    Main.NewText("You haven't mastered Full Cowling 8% yet.", Color.Red);
                    return;
                }
                mainPlayer.ActiveForm = targetForm;
            }
        }

        private void DoDetroitSmash(TransformationPlayer mainPlayer)
        {
            bool isProtected = mainPlayer.ActiveForm != OfaSkills.None;
            bool isDangerous = (mainPlayer.CurrentStage == QuirkStage.Initial) || (!isProtected);
            
        }
        
        private void DoSuperJump(TransformationPlayer mainPlayer)
        {
            bool isDangerous = (mainPlayer.CurrentStage == QuirkStage.Initial) || (mainPlayer.ActiveForm != OfaSkills.None);

            if (isDangerous)
            {
                Player.velocity.Y = -15f;
                int damageTaken = 25;
                Player.statLife -= damageTaken;

                if (Player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
                    Player.KillMe(reason, damageTaken, 0);
                }
                CombatText.NewText(Player.getRect(), Color.Red, "Leg Broken!");

                for (int i = 0; i < 15; i++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, 0, 5, 100, default, 1.5f);
                }   
            }
            else
            {
                Main.NewText("Cannot use Super Jump in current state.", Color.Red);
            }
        }
        private void DoDelawareSmash(TransformationPlayer mainPlayer)
        {
            int damage = 60;
            bool consumeFinger = false;
            bool hurtPlayer = false;

            if (mainPlayer.CurrentStage == QuirkStage.Initial)
            {
                damage = 60; consumeFinger = true; hurtPlayer = true;
            }
            else if (mainPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                if (mainPlayer.ActiveForm != OfaSkills.None)
                {
                    damage = 10; consumeFinger = false; hurtPlayer = false;
                } else
                {
                    damage = 60; consumeFinger = true; hurtPlayer = true;
                }
            }
            if (consumeFinger && Fingers <= 0)
            {
                CombatText.NewText(Player.getRect(), Color.Red, "No fingers left!");
                return;
            }

            if (consumeFinger) Fingers--;

            Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Velocity, ModContent.ProjectileType<DelawareSmashProj>(), damage, 2f, Player.whoAmI);

            if (hurtPlayer)
            {
                Player.statLife -= 10;
                if (Player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
                    Player.KillMe(reason, damage, 0);
                }
            }
        }

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && mainPlayer.ActiveForm != OfaSkills.None)
            {
                Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 2);


                 Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
            }
            else
            {
                isFullCowlingBuffActive = false;
            }
        }

    }}
