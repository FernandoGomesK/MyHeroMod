using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.Float
{
    public partial class FloatPlayer : ModPlayer
    {
        public bool isFloatActive = false;

        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

        public override void OnRespawn() => ResetAll();

        public void ResetAll()
        {
            isFloatActive = false;
            SkillCooldowns.Clear();
        }

        public override void ResetEffects()
        {
            // Reseta todo frame. O FloatBuff.cs vai colocar como true se estiver ativo.
            isFloatActive = false;
        }

        public override void PostUpdate()
        {
            // Só funciona se o buff estiver ativo, não estiver em montaria e não estiver no chão
            if (isFloatActive && !Player.mount.Active && Player.velocity.Y != 0)
            {
                // Se segurar PULO: Para no ar (Hover)
                if (Player.controlJump) 
                {
                    Player.velocity.Y = -2f; 
                    Player.fallStart = (int)(Player.position.Y / 16f); // Zera dano de queda
                }
                // Se NÃO segurar pulo mas estiver caindo: Cai devagar (Pena)
                else if (Player.velocity.Y > 0)
                {
                    Player.velocity.Y *= 0.5f; // Cai devagarinho
                }
            }
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
            
        }
        

        
        }
    }