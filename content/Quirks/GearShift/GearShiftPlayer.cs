using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;


namespace MyHeroMod.content.Quirks.Gearshift;

    public partial class GearshiftPlayer : ModPlayer
    {
        public bool isGearshiftActive = false;
        public bool isGearshiftBuffActive = false;
        public int GearshiftTimer = 0;
        public int GearshiftMaxTime = 6000;
        // Gearshift Buff
        public bool GearActivation = false;

        public int ActivationTimer = 0;
        public int ActivationMaxTime = 40;

        
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

       
        


        public override void OnRespawn()
        {
            
            GearshiftTimer = 0;
            ActivationTimer = 0;
        }

        

        public override void PostUpdateEquips()
        {
            
        }
        
        
    


        

        public override void ResetEffects()
        {
            
            var ModPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se é Explosão e se o estágio é Adequation ou maior
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
        }
        
        public override void PreUpdate()
        {
            
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }

             if (ActivationTimer > 0)
            {
                ActivationTimer++;
                Player.velocity *= 0.6f; // Efeito de "carregar" (freia o jogador)

                // Visual durante o carregamento
                if (GearActivation)
                {
                    // Partículas Ciano para Gearshift
                    if (Main.rand.NextBool(2))
                    {
                        Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, Color.Cyan, 0.3f);
                        d.velocity *= 2f;
                        d.noGravity = true;
                    }
                }

                // Transformação Completa
                if (ActivationTimer >= ActivationMaxTime)
                {
                   int buffTime = 0;
                   var transformPlayer = Player.GetModPlayer<TransformationPlayer>();
                   var gearPlayer = Player.GetModPlayer<GearshiftPlayer>();
            
            
            
            

            switch(transformPlayer.CurrentStage){
                case QuirkStage.Initial:
                
                buffTime = 187; 
                break;
            
                case QuirkStage.Adequation:
                buffTime = 375; 
                break;
          
                case QuirkStage.Intermediate:
                buffTime = 750; 
                break;
            
                case QuirkStage.Advanced:
                buffTime = 1500; 
                break;
          
                case QuirkStage.Final:
                buffTime = 3000; 
                break;
        
                default:
                buffTime = 6000;
                break;
                    
            }

                   
                    

                    // 2. Se for Gearshift
                    if (GearActivation)
                    {
                        isGearshiftActive = true;
                        GearActivation = false;
                        GearshiftTimer = 0;

                        // EFEITOS FINAIS DA ATIVAÇÃO
                        Main.NewText("ONE FOR ALL 2ND - GEARSHIFT: TRANSMISSION !", Color.Cyan);
                        CombatText.NewText(Player.getRect(), Color.Cyan, "SECOND GEAR");
                        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/GearShiftSound"), Player.position);

                        // Explosão de partículas
                        for (int i = 0; i < 20; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2Circular(5f, 5f);
                            Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, speed.X, speed.Y, 0, Color.Cyan, 2f);
                        }



                        Player.AddBuff(ModContent.BuffType<GearshiftBuff>(), buffTime);


                    }
                    ActivationTimer = 0;

                    
                }
            }
           
        }
        public override void PostUpdate()
        {
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
{
    // Verifica se o Buff do Gearshift está ativo
    if (isGearshiftBuffActive)
    {
        // AUMENTAR VELOCIDADE
        // Multiplica por 2.5x (MUITO rápido, como o Gearshift deve ser)
        velocity *= 2.5f; 

        // AUMENTAR DANO
        // Aumenta o dano base do projétil em +30%
        damage = (int)(damage * 1.3f); 
    }
}

        
    }
    

