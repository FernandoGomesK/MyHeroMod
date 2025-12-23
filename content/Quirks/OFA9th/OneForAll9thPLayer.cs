using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public class OneForAll9thPlayer : ModPlayer
    {

        public bool isFullCowlingBuffActive = false;

        public int Fingers = 10;



        public override void OnRespawn()
        {
            Fingers = 10;
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
            }
        }

        private void DoDetroitSmash(TransformationPlayer mainPlayer)
        {
            bool isDangerous = (mainPlayer.CurrentStage == QuirkStage.Initial) || (!mainPlayer.isTransformationActive);
            
        }
        private void DoSuperJump(TransformationPlayer mainPlayer)
        {
            bool isDangerous = (mainPlayer.CurrentStage == QuirkStage.Initial) || (!mainPlayer.isTransformationActive);

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
                if (mainPlayer.isTransformationActive)
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
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && mainPlayer.isTransformationActive)
            {
                isFullCowlingBuffActive = true;
                Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 2);
                float multiplier = (int)mainPlayer.CurrentStage +1;
                Player.GetDamage(DamageClass.Generic) += 0.05f * multiplier;
                Player.statDefense += (int)multiplier;
                Player.moveSpeed += 1.00f * multiplier;
                Player.jumpSpeed += 1.50f * multiplier;


                 Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);
            }
            else
            {
                isFullCowlingBuffActive = false;
            }
        }
public class GreenLightningLayer : PlayerDrawLayer
{
    
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
        var mp = drawInfo.drawPlayer.GetModPlayer<TransformationPlayer>();
        return mp.SelectedQuirk == QuirkType.OneForAll9th && mp.isTransformationActive;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo) {
        
        if (!ModContent.HasAsset("MyHeroMod/Assets/FullCowling")) {
            return; 
        }

        Texture2D texture = ModContent.Request<Texture2D>("MyHeroMod/Assets/FullCowling").Value;

        // Ajuste o frameCount para o número real de frames que você desenhou
        int frameCount = 6; 
        int frameSpeed = 6; 
        int currentFrame = (int)(Main.GameUpdateCount / frameSpeed) % frameCount;

        int frameHeight = texture.Height / frameCount;
        Rectangle sourceRect = new Rectangle(0, currentFrame * frameHeight, texture.Width, frameHeight);

        // Centraliza os raios no jogador
        Vector2 position = drawInfo.Center - Main.screenPosition;
        
        // Criando o dado de desenho
        DrawData drawData = new DrawData(
            texture,
            new Vector2((int)position.X, (int)position.Y), 
            sourceRect,
            Color.White, 
            drawInfo.drawPlayer.fullRotation,
            new Vector2(texture.Width / 2f, frameHeight / 2f),
            1f,
            drawInfo.playerEffect,
            0
        );

        // Adiciona à lista de desenhos do frame atual
        drawInfo.DrawDataCache.Add(drawData);
    }
}
    }}
