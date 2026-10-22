using KhacesCore.Content.Buffs;
using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Jet
{
    public partial class JetPlayer : ModPlayer, IDashModifier, IFlightModifier
    {
        public bool isIronSolesOn = false;

        public override void ResetEffects()
        {
            isIronSolesOn = false;
        }

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (transPlayer.HasActiveQuirk(QuirkType.Jet))
            {
                Player.wingTimeMax = 220;

                if (Player.wingsLogic == 0)
                {
                    Player.wingsLogic = 29;
                    Player.wings = -1;
                }

                Player.noFallDmg = true;

                UpdateFlyingDust();
            }
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

        public void ModifyFlight(ref float speed)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();


            if (!transPlayer.HasActiveQuirk(QuirkType.Flight)) return;



            float dashSpeed = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 8f,
                QuirkStage.Adequation => 15f,
                QuirkStage.Intermediate => 16f,
                QuirkStage.Advanced => 35f,
                QuirkStage.Final => 40f,
                _ => 8f
            };

            speed = dashSpeed;
        }

        public bool CanCruiseFlight()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            return transPlayer.HasActiveQuirk(QuirkType.Jet);
        }

        protected virtual void UpdateFlyingDust()
        {
            bool isFlying = (Player.velocity.Y != 0) && (Player.wingTime > 0 || Player.rocketDelay > 0) && !Player.mount.Active;
            if (isFlying)
            {
                if (Main.rand.NextBool(2))
                {
                    int dustFire = Dust.NewDust(Player.position + new Vector2(-5, Player.height - 10), Player.width / 2, 10, DustID.Cloud, 0, 2f, 100, default, 1.5f);
                    Main.dust[dustFire].noGravity = true;
                    Main.dust[dustFire].velocity *= 0.5f;
                }
            }
        }
    }
}