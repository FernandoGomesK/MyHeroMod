using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Float;
using Terraria;
using MyHeroMod.content.Quirks.Flight;
using MyHeroMod.content.Quirks.SlideAndGlide;

namespace MyHeroMod.content.Buffs // Ajuste o namespace se necessário
{
    public class SlideAndGlideBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/SlideAndGlideBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SlideAndGlidePlayer>().isSlideOn = true;
            var slidePlayer = player.GetModPlayer<SlideAndGlidePlayer>();

             var mainPlayer = player.GetModPlayer<TransformationPlayer>();

            
            if (!mainPlayer.HasActiveQuirk(QuirkType.SlideAndGlide))  
                return;

            
            if (slidePlayer.isSlideOn)
            {
                if (mainPlayer.CurrentStage == QuirkStage.Initial || mainPlayer.CurrentStage == QuirkStage.Adequation)
                {
                    // Sem pulos extras. O jogo faz isso naturalmente se você não der nenhum pulo a ele.
                }
                else if (mainPlayer.CurrentStage == QuirkStage.Intermediate)
                {
                    
                    player.GetJumpState(ExtraJump.CloudInABottle).Enable();
                    player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();
                    player.GetJumpState(ExtraJump.SandstormInABottle).Enable();
                    player.GetJumpState(ExtraJump.FartInAJar).Enable();
                    player.GetJumpState(ExtraJump.TsunamiInABottle).Enable();
                    player.GetJumpState(ExtraJump.UnicornMount).Enable();
                }
                else if (mainPlayer.CurrentStage == QuirkStage.Advanced)
                {
                    player.wingTimeMax =360000;

                if (player.wingsLogic == 0)
                {
                    player.wingsLogic = 29; 
                    player.wings = -1; 
                }

                player.noFallDmg = true;
                }
                    

                
                player.spikedBoots = 2; 
                
                
                bool isGrounded = player.velocity.Y == 0f;

                if (isGrounded)
                {
                    
                    bool tryingToTurn = (player.controlLeft && player.velocity.X > 0.5f) || 
                                        (player.controlRight && player.velocity.X < -0.5f);

                    
                    float turnPenalty = 1f;
                    if (tryingToTurn)
                    {
                        turnPenalty = mainPlayer.CurrentStage switch
                        {
                            QuirkStage.Initial => 0.05f,       
                            QuirkStage.Adequation => 0.2f,     
                            QuirkStage.Intermediate => 0.6f,   
                            QuirkStage.Advanced => 1.0f,      
                            QuirkStage.Final => 1.0f,
                            _ => 0.05f
                        };
                    }

                    
                    float baseAcceleration = mainPlayer.CurrentStage switch
                    {
                        QuirkStage.Initial => 2f, 
                        QuirkStage.Adequation => 2.5f, 
                        QuirkStage.Intermediate => 4.5f, 
                        QuirkStage.Advanced => 5.5f, 
                        QuirkStage.Final => 8.0f, 
                        _ => 2.0f
                    };
                    player.runAcceleration *= baseAcceleration * turnPenalty; 

                    
                    player.maxRunSpeed += mainPlayer.CurrentStage switch
                    {
                        QuirkStage.Initial => 2f, 
                        QuirkStage.Adequation => 2.5f, 
                        QuirkStage.Intermediate => 4.5f, 
                        QuirkStage.Advanced => 5.5f, 
                        QuirkStage.Final => 8.0f, 
                        _ => 2.0f
                    }; 

                
                    float slowdownMult = mainPlayer.CurrentStage switch
                    {
                        QuirkStage.Initial => 0.02f,       
                        QuirkStage.Adequation => 0.08f,    
                        QuirkStage.Intermediate => 0.3f,   
                        QuirkStage.Advanced => 0.8f,       
                        QuirkStage.Final => 1.0f,          
                        _ => 0.02f
                    };
                    player.runSlowdown *= slowdownMult;  
                }
                else
                {
                    
                }
            }
        }
    }}