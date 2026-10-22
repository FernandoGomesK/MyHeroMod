using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Jet
{
    public partial class JetPlayer : ModPlayer, IDashModifier
    {
        public bool isIronSolesOn = false;

        public override void ResetEffects()
        {
            isIronSolesOn = false;
        }
        public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash,
        ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();


            if (!transPlayer.HasActiveQuirk(QuirkType.Jet)) return;


            float dashSpeed = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 25f,
                QuirkStage.Adequation => 35f,
                QuirkStage.Intermediate => 50f,
                QuirkStage.Advanced => 55f,
                QuirkStage.Final => 65f,
                _ => 80f
            };

            isEnhanced = true;
            dustType = DustID.Cloud;
            explosionColor = Color.White;


            speed = dashSpeed;

            // SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
        }

    }
}