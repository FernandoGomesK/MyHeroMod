using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content;
using MyHeroMod.content.System;
using Terraria.Audio;
using System.Collections.Generic;

namespace MyHeroMod.content.Quirks.Explosion
{
    public partial class ExplosionPlayer : ModPlayer
    {
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

        public bool IsClusterActive = false;

        public int MaxSweat = 0;
        public int CurrentSweat = 0;

        public override void ResetEffects()
        {
            IsClusterActive = false;
            SkillCooldowns.Clear();
            var ModPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se é Explosão e se o estágio é Adequation ou maior
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se é Explosão e se o estágio é Adequation ou maior
            if (transPlayer.SelectedQuirk == QuirkType.Explosion && transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                // "Player" é a referência correta ao jogador dono deste ModPlayer
                Player.noFallDmg = true; 
            }
        }
        
        public override void PreUpdate()
        {
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }
        }
        public override void PostUpdate()
        {
            
        }
    }
}

