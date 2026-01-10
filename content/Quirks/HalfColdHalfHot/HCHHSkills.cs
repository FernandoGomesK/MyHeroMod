using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles;
using MyHeroMod.content.Quirks.HalfColdHalfHot.Projectiles.HeavenPiercingWall;

using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Quirks.HalfColdHalfHot
{
    public partial class HalfColdHalfHotPlayer : ModPlayer
    {
                public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (MainPlayer.SelectedQuirk == QuirkType.HalfColdHalfHot) 
            {
                if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot1);
                if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot2);
                if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot3);
                if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.TransformSlot);
            }      
        }

        private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
        {
            if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
            {
                Main.NewText("On cooldown!", Color.White);
                // Skill is on cooldown
                return;
            }

            switch (skill)
            {
                    // case QuirkSkills.FlashFireFist:
                    // ActivateFlashFireFist(mainPlayer);

                    // SetCooldown(skill, 60);
                    // break;
                    case QuirkSkills.HeavenPiercingWall:

                    DoHeavenPiercingWall(mainPlayer);
                    break;
                    // case QuirkSkills.JetBurn:

                    // DoJetBurn(mainPlayer);
                    // break;

                    // case QuirkSkills.IgnitedArrow:

                    // DoIgnitedArrow(mainPlayer);
                    // break;

                    // case QuirkSkills.HellSpider:

                    // DoHellSpider(mainPlayer);
                    // break;
                    
                
                

            }
        }
        private void SetCooldown(QuirkSkills skill, int timeInTicks)
        {
            if (SkillCooldowns.ContainsKey(skill))
            {
                SkillCooldowns[skill] = timeInTicks;
            }
            else
            {
                SkillCooldowns.Add(skill, timeInTicks);
            }
        }

        private void DoHeavenPiercingWall(TransformationPlayer mainPlayer)
        {
        // Define a direção (Esquerda ou Direita baseado no mouse)
        float direction = Main.MouseWorld.X > Player.Center.X ? 1f : -1f;
    
        // Velocidade da onda (Rápida)
        Vector2 velocity = new Vector2(10f * direction, 0f);

        // Spawna o Controlador um pouco na frente do player
        Projectile.NewProjectile(
            Player.GetSource_FromThis(),
            Player.Center + new Vector2(20f * direction, 0), // Começa um pouco a frente
            velocity,
            ModContent.ProjectileType<IceWaveController>(),
            50, // Dano
            5f,
            Player.whoAmI
        );
}
        // private void ActivateFlashFireFist(TransformationPlayer mainPlayer)
        // {
        //     if (IsFlashFireFistActive)
        //     {
        //         IsFlashFireFistActive = false;
        //         Player.ClearBuff(ModContent.BuffType<Buffs.FlashFireFistBuff>());
        //         Main.NewText("Flash Fire Fist Deactivated", Color.OrangeRed);   
        //         SetCooldown(QuirkSkills.FlashFireFist, 120);
        //         return;
                
        //     }
        //     CurrentHeat += 20;
        //     IsFlashFireFistActive = true;

            
        // }
        // private void DoJetBurn(TransformationPlayer mainPlayer)
        // {
        //     // Verifica se já existe um controlador ativo (para não spawnar duplicado)
        //     if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.JetBurn.JetBurnController>()] > 0)
        //         return;

        //     // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
        //     // Note que a velocidade aqui define apenas a direção inicial da mira.
        //     Vector2 direction = Main.MouseWorld - Player.Center;
        //     direction.Normalize();

        //     Projectile.NewProjectile(
        //         Player.GetSource_FromThis(),
        //         Player.Center,
        //         direction,
        //         ModContent.ProjectileType<Projectiles.JetBurn.JetBurnController>(),
        //         0, // O controlador não dá dano direto
        //         0f,
        //         Player.whoAmI
            
        //     );
        //     CurrentHeat += 15;
        // }
        // private void DoProminenceBurn()
        // {
        //     // Evita duplicar se já estiver ativo
        //     if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.ProminenceBurn.ProminenceBurnController>()] > 0)
        //         return;

        //     Main.NewText("PROMINENCE BURN!!!", Color.OrangeRed);
            
        //     // Som inicial de explosão
        //     SoundEngine.PlaySound(SoundID.Item117, Player.position); 

        //     Vector2 direction = Main.MouseWorld - Player.Center;
        //     direction.Normalize();

        //     // Lança o Controlador
        //     Projectile.NewProjectile(
        //         Player.GetSource_FromThis(),
        //         Player.Center,
        //         direction,
        //         ModContent.ProjectileType<Projectiles.ProminenceBurn.ProminenceBurnController>(),
        //         0, 
        //         0f, 
        //         Player.whoAmI
        //     );
        //     CurrentHeat += 15;
        // }
        // private void DoIgnitedArrow(TransformationPlayer mainPlayer)
        // {
        //     // Implementação do Ignited Arrow

        //     Vector2 Velocity = Main.MouseWorld - Player.Center;
        //     Velocity.Normalize();
        //     Velocity *= 15f;

        //     Projectile.NewProjectile(
        //         Player.GetSource_FromThis(),
        //         Player.Center,
        //         Velocity,
        //         ModContent.ProjectileType<IgnitedArrowProj>(),
        //         40, 
        //         2f, 
        //         Player.whoAmI
        //     );
        //     CurrentHeat += 15;
        // }
        // private void DoHellSpider(TransformationPlayer mainPlayer)
        // {
        //     // Verifica se já existe um controlador ativo (para não spawnar duplicado)
        //     if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.HellSpider.HCHellSpiderController>()] > 0)
        //         return;

        //     // Apenas spawna o CONTROLADOR. Ele cuidará de atirar o fogo.
        //     // Note que a velocidade aqui define apenas a direção inicial da mira.
        //     Vector2 direction = Main.MouseWorld - Player.Center;
        //     direction.Normalize();

        //     Projectile.NewProjectile(
        //         Player.GetSource_FromThis(),
        //         Player.Center,
        //         direction,
        //         ModContent.ProjectileType<Projectiles.HCHellSpider.HellSpiderController>(),
        //         0, // O controlador não dá dano direto
        //         0f,
        //         Player.whoAmI
        //     );
        //     CurrentHeat += 15;

        // }
    }
}