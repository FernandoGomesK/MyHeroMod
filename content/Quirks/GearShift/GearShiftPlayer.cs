using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;


namespace MyHeroMod.content.Quirks.Gearshift
{
    // PARTE 1: DADOS E LÓGICA
    public partial class GearshiftPlayer : ModPlayer
    {
        // Variáveis de Estado
        
        public bool isGearshiftActive = false;
        public bool isGearshiftBuffActive = false; // Controlado pelo Buff
        
        // Variáveis de Carregamento (Nomes compatíveis com OverdriveLayer)
        public bool GearActivation = false; // Substitui isCharging
        public int ActivationTimer = 0;     // Substitui chargeTimer
        public int ActivationMaxTime = 40;  

        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

        public override void OnRespawn() => ResetAll();

        public void ResetAll()
        {
            isGearshiftActive = false;
            isGearshiftBuffActive = false;
            GearActivation = false;
            ActivationTimer = 0;
            SkillCooldowns.Clear();
        }

        public override void ResetEffects()
        {
            // Reseta todo frame. O GearshiftBuff.cs vai colocar como true se estiver ativo.
            isGearshiftBuffActive = false; 
        }

        public override void PreUpdate()
        { 
            // 1. Gerencia Cooldowns
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }
            // 2. Lógica de Carregamento
            if (GearActivation)
            {
                ActivationTimer++;
                Player.velocity *= 0.8f; // Freia o jogador
                
                // Partículas durante o carregamento
                if (Main.rand.NextBool(2))
                {
                    Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, Color.Cyan, 0.5f);
                    d.noGravity = true;
                    d.velocity *= 0.5f;   
                }

                // Terminou de carregar?
                if (ActivationTimer >= ActivationMaxTime)
                {
                    ActivateGearshift();
                    GearActivation = false;
                    ActivationTimer = 0;
                }
            }
        }
        private void ActivateGearshift()
        {
            var transformPlayer = Player.GetModPlayer<TransformationPlayer>();
            int buffDuration = 180;

            switch(transformPlayer.CurrentStage)
            {
                case QuirkStage.Initial: buffDuration = 187; break;
                case QuirkStage.Adequation: buffDuration = 375; break;
                case QuirkStage.Intermediate: buffDuration = 750; break;
                case QuirkStage.Advanced: buffDuration = 1500; break;
                case QuirkStage.Final: buffDuration = 3000; break;
                default: buffDuration = 6000; break;
            }

            // Adiciona o Buff e Toca os Efeitos
            Player.AddBuff(ModContent.BuffType<GearshiftBuff>(), buffDuration);
            Main.NewText("ONE FOR ALL 2ND - GEARSHIFT: TRANSMISSION!", Color.Cyan);
            CombatText.NewText(Player.getRect(), Color.Cyan, "SECOND GEAR");
            

            // Explosão de partículas
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, speed.X, speed.Y, 0, Color.Cyan, 2f);
            }
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (isGearshiftBuffActive)
            {
                velocity *= 2.5f; 
                damage = (int)(damage * 1.3f); 
            }
        }
    }
}