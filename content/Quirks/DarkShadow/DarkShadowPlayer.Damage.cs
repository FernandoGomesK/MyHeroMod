using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow.Projectiles;

namespace MyHeroMod.content.Quirks.DarkShadow
{
    public partial class DarkShadowPlayer : ModPlayer
    {

        public int GetProjectileDamage()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();


            float CboBuff = 1.0f;

            if (this.isCBOArmsOn)
            {
                CboBuff = 1.25f;
            }

            int baseProjectileDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 45,
                QuirkStage.Adequation => 65,
                QuirkStage.Intermediate => 80,
                QuirkStage.Advanced => 120,
                QuirkStage.Final => 350,
                _ => 45
            };

            return (int)(baseProjectileDamage * CboBuff);
        }
    }
}