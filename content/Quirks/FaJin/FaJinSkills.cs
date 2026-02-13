using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

using Terraria.ID;

using Terraria.Audio;

using MyHeroMod.content.GeneralSkills1;



namespace MyHeroMod.content.Quirks.FaJin
{
    public partial class FaJinPlayer : ModPlayer
    {
        public override void ProcessTriggers(Terraria.GameInput.TriggersSet triggersSet)
        {
            var MainPlayer = Player.GetModPlayer<TransformationPlayer>();


            if (MainPlayer.SelectedQuirk == QuirkType.FaJin) 
            {
                if (KeybindSystem.SkillSlot1.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot1);
                if (KeybindSystem.SkillSlot2.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot2);
                if (KeybindSystem.SkillSlot3.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.Slot3);
                if (KeybindSystem.TransformKey.JustPressed) ExecuteSkill(MainPlayer, MainPlayer.TransformSlot);
            }      
        }

        private void ExecuteSkill(TransformationPlayer mainPlayer, QuirkSkills skill)
        {
            var generalSkills = Player.GetModPlayer<GeneralSkills>();

            if (SkillCooldowns.ContainsKey(skill) && SkillCooldowns[skill] > 0)
            {
                Main.NewText("On cooldown!", Color.White);
                // Skill is on cooldown
                return;
            }

            switch (skill)
            {
                
                    case QuirkSkills.FaJinStore:
                    StoreFaJin(mainPlayer, QuirkSkills.FaJinStore);
                    SetCooldown(skill, 60);
                    break;

                    case QuirkSkills.Dash:
                    if (!FaJinStored)
                    {
                        generalSkills.Dash();
                        chargeFaJin();
                        SetCooldown(skill, 60);
                    }
                    else
                    {
                        generalSkills.Dash(25f);
                    FaJinCharges = 0;
                    FaJinStored = false;
                    SetCooldown(skill, 60);
                    dashvfx(); 
                    
                    }
                    break;
                    
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


        private void StoreFaJin(TransformationPlayer mainPlayer, QuirkSkills targetForm)
        {
            chargeFaJin();
        }
        
        private void dashvfx()
        {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1"), Player.position);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 dustPosition = Player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    Dust.NewDust(dustPosition, 0, 0, DustID.Smoke, Player.velocity.X * -0.5f, Player.velocity.Y * -0.5f);
                }
                for (int i = 0; i < 15; i++)
                {
                    Vector2 dustPosition = Player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    Dust.NewDust(dustPosition, 0, 0, DustID.RedTorch, Player.velocity.X * -0.5f, Player.velocity.Y * -0.5f, 0, default, 6f);
                }
            }
 
        
        }



        
            
            

        

             
            
        }

        
        

