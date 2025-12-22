using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeromod.Content.Quirks.OFA9th.Projectiles;

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
            }
        }

        private void DoSuperJump(TransformationPlayer mainPlayer)
            // You can add custom keybind processing here if needed
            if (KeybindSystem.SkillKey.JustPressed && mainPlayer.SelectedQuirk == QuirkType.OneForAll9th)
            {
                if (mainPlayer.CurrentStage == QuirkStage.Initial)
                {
                    Player.velocity.Y -= 25f;
                    int damageTaken = 25;
                    Player.statLife -= damageTaken;

                    if (Player.statLife <= 0)
                    {
                        
                        var reason = PlayerDeathReason.ByCustomReason(
                            Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));

                        Player.KillMe(reason, damageTaken, 0);
                    }

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, 0, 5, 100, default, 1.5f);
                }
            }
        }}

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (mainPlayer.SelectedQuirk == QuirkType.OneForAll9th && mainPlayer.isTransformationActive)
            {

                Player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 2);

                float multiplier = (int)mainPlayer.CurrentStage + 1;
                Player.GetDamage(DamageClass.Generic) += 0.10f * multiplier;
                Player.statDefense += (int)(5 * multiplier);
                Player.moveSpeed += 1.00f * multiplier;
                Player.jumpSpeed += 1.50f * multiplier;

                Lighting.AddLight(Player.Center, Color.LimeGreen.ToVector3() * 1.0f);

                if (mainPlayer.CurrentStage == QuirkStage.Initial && Main.rand.NextBool(600))
                {
                    Player.GetDamage(DamageClass.Generic) += 0.10f;
                    Player.statLife -= 5;
                    CombatText.NewText(Player.getRect(), Color.Red, "-5 HP: Strain!");
                }

            }
        }
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