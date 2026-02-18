using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.GeneralSkills1;
using MyHeroMod.content.System.BasePlayer;


namespace MyHeroMod.content.Quirks.Gearshift
{
    // PARTE 2: INPUTS E SKILLS
    public partial class GearshiftPlayer : BasePlayer
    {
        // public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        // {
        //     var MainPlayer = Player.GetModPlayer<TransformationPlayer>();

        //     if (MainPlayer.SelectedQuirk == QuirkType.Gearshift) 
        //     {
        //         if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot1);
        //         if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot2);
        //         if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot3);
        //         if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.TransformSlot);
        //     }      
        // }

        // private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
        // {
        //     var skillData = SkillLibrary.GetSkill(skill);
        //     if (skillData != null && skillData.CanUse(Player)) {
        //     skillData.OnUse(Player);
        //     SetCooldown(skill, skillData.BaseCooldown);
        //     }
        //     var generalSkills = Player.GetModPlayer<GeneralSkills1.GeneralSkills>(); // Caminho completo para evitar confusão

        //     if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
        //     {
        //         Main.NewText("On cooldown!", Color.White);
        //         return;
        //     }

            
        // }

        // private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
        // {
        //     var generalSkills = Player.GetModPlayer<GeneralSkills>();

        //     if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
        //     {
        //         Main.NewText("On cooldown!", Color.White);
        //         return;
        //     }

        //     switch (skill)
        //     {
        //         case QuirkSkills.Gearshift:
        //             ToggleGearshift(mainPlayer);
        //             SetCooldown(skill, 30);
        //             break;

        //         case QuirkSkills.Dash:
        //             if (!isGearshiftBuffActive)
        //             {
        //                 generalSkills.Dash();
        //                 SetCooldown(skill, 60);
        //             }
        //             else
        //     {

        //         Vector2 targetPos = Main.MouseWorld;
        //         Vector2 dir = targetPos - Player.Center;
        //         float distance = dir.Length();
                
        //         float maxDist = 600f;
        //         if (distance > maxDist)
        //         {
        //             dir.Normalize();
        //             dir *= maxDist;
        //             distance = maxDist;
        //         }

            
        //         Vector2 safePos = Player.Center;
        //         float stepSize = 16f; 
        //         bool hitWall = false;

        //         for (float i = 0; i < distance; i += stepSize)
        //         {
        //             Vector2 checkPos = Player.Center + Vector2.Normalize(dir) * i;
                    
                    
        //             if (Collision.SolidCollision(checkPos - new Vector2(Player.width/2, Player.height/2), Player.width, Player.height))
        //             {
        //                 hitWall = true;
        //                 break; 
        //             }
        //             safePos = checkPos; 
        //         }

        //         Vector2 startPos = Player.Center;
        //         int dustCount = (int)(Vector2.Distance(startPos, safePos) / 5); // 1 partícula a cada 5 pixels
        //         for (int i = 0; i < dustCount; i++)
        //         {
        //             Vector2 dustPos = Vector2.Lerp(startPos, safePos, (float)i / dustCount);
        //             int d = Dust.NewDust(dustPos, 0, 0, DustID.Electric, 0, 0, 100, Color.Cyan, 1.5f);
        //             Main.dust[d].noGravity = true;
        //             Main.dust[d].velocity *= 0.5f;
        //         }

                
        //         Player.Center = safePos;
        //         Player.velocity = Vector2.Zero; 
        //         if (hitWall) 
        //         {
        //             Player.velocity = -Vector2.Normalize(dir) * 2f; 
        //         }

        //         dashvfx(); 
        //         SetCooldown(skill, 40); 
        //     }
        //     break;
        //     }
        // }

        // private void SetCooldown(QuirkSkills skill, int timeInTicks)
        // {
        //     if (SkillCooldowns.ContainsKey(skill)) SkillCooldowns[skill] = timeInTicks;
        //     else SkillCooldowns.Add(skill, timeInTicks);
        // }

        // private void dashvfx()
        // {
        //     SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.15f }, Player.position);
        //         for (int i = 0; i < 4; i++)
        //         {
        //             Vector2 dustPosition = Player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
        //             Dust.NewDust(dustPosition, 0, 0, DustID.Smoke, Player.velocity.X * -0.5f, Player.velocity.Y * -0.5f);
        //         }
        //         for (int i = 0; i < 15; i++)
        //         {
        //             Vector2 dustPosition = Player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
        //             Dust.NewDust(dustPosition, 0, 0, DustID.BlueTorch, Player.velocity.X * -1f, Player.velocity.Y * -1f, 0, default, 6f);
        //         }
        // }

        // private void ToggleGearshift(TransformationPlayer mainPlayer)
        // {
        //     // 1. DESLIGA se tiver o Buff ativo
        //     if (Player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
        //     {
        //         Player.ClearBuff(ModContent.BuffType<GearshiftBuff>());
                
        //         Main.NewText("Gearshift Deactivated!", Color.White);
        //         SetCooldown(QuirkSkills.Gearshift, 600); // 10s cooldown
                
        //         GearActivation = false;
        //         ActivationTimer = 0;
        //         return;
        //     }
        //     // 2. CANCELA se estiver carregando
        //     else if (GearActivation)
        //     {
        //         GearActivation = false;
        //         ActivationTimer = 0;
        //         Main.NewText("Cancelled.", Color.Gray);
        //     }
        //     // 3. LIGA (Começa a carregar)
        //     else
        //     {
        //         ActivationTimer = 0;
        //         GearActivation = true;
        //         SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/GearShiftSound") with { Volume = 0.20f }, Player.position);
        //     }
        // }
    }
}