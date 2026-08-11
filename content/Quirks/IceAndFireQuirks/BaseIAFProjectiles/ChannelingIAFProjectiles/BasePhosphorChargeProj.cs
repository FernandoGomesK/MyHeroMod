using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.ChannelingIAFProjectiles
{ 
    public class BasePhosphorChargeProj : BaseChannelingProj
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";
        protected override int ChannelTime => 60; 

        public override void AI()
        {
            base.AI(); 
            Player player = Main.player[Projectile.owner];
            
            if (player.active && !player.dead)
            {
                player.velocity *= 0.6f; 
            }

            if (Projectile.ai[0] == 1) 
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FireCrackingSound"), player.position);
            }
        }

        public override void SpawnChargingDust(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
            int dustType = DustID.Torch; 
            
            if (Projectile.ai[1] == ModContent.BuffType<PhosphorBuff>())
            {
                if (transPlayer.HasActiveQuirk(QuirkType.Blueflame))
                {
                    dustType = Main.rand.NextBool() ? DustID.PurpleTorch : DustID.PurpleTorch;
                }
                else
                {
                    dustType = Main.rand.NextBool() ? DustID.Torch : DustID.IceTorch;
                }
                
            }

            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, dustType, 0, 0, 100, default, 1.5f);
                d.noGravity = true;
                d.velocity *= 0.5f;   
            }
        }

        public override void OnChargeCancelled(Player player)
        {
            CombatText.NewText(player.getRect(), Color.Red, "Activation Cancelled!");
        }

        public override void OnChargeComplete(Player player)
        {
            ImpactFrameSystem.Trigger(Color.SkyBlue, false,"MyHeroMod/Assets/Effects/BlankImpactImage", "MyHeroMod/Assets/Effects/PhosphorImpactImage", "MyHeroMod/Assets/Effects/PhosphorImpactImage2","MyHeroMod/Assets/Effects/PhosphorImpactImage3");
            int buffToApply = (int)Projectile.ai[1];
            
          
            player.AddBuff(buffToApply, 3600000);

            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/CremationSound"), player.position);
            
            
            string activationText = "Phosphor!";
            if (buffToApply == ModContent.BuffType<PhosphorBuff>()) activationText = "Phosphor!";
            
            CombatText.NewText(player.getRect(), Color.Cyan, activationText);
            
        
            for (int i = 0; i < 30; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(10f, 10f);
                Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, speed.X, speed.Y, 0, default, 2.5f);
            }

            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "PhosphorShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}