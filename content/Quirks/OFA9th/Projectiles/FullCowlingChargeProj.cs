using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers; 
using MyHeroMod.content.System;
using MyHeroMod.content.Projectiles.Base;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{ 
    public class FullCowlingChargeProj : BaseChannelingProj
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";

        
        protected override int ChannelTime => 40; 

        public override void AI()
        {
            base.AI(); 

            Player player = Main.player[Projectile.owner];
            
            
            if (player.active && !player.dead)
            {
                player.velocity *= 0.6f; 
            }
        }

        public override void SpawnChargingDust(Player player)
        {
            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, Color.Green, 0.5f);
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
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            int percentage = (int)Projectile.ai[2];
            ofaPlayer.percentage = percentage;

            player.AddBuff(ModContent.BuffType<FullCowlingBuff>(), 3600000);
            
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
            CombatText.NewText(player.getRect(), Color.Cyan, $"Full Cowling {percentage}%!");
            
            
            for (int i = 0; i < 30; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(10f, 10f);
                Dust.NewDust(player.position, player.width, player.height, DustID.Electric, speed.X, speed.Y, 0, Color.Green, 2.5f);
            }

            
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}