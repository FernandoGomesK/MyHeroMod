using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.FaJin;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Hardening.Projectiles
{
    public class ChargeUnbreakableProj : BaseChannelingProj
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";


        protected override int ChannelTime => 30;

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
                //SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
            }
        }

        public override void SpawnChargingDust(Player player)
        {
            //if (Main.rand.NextBool(2))
            //{
            //    Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, Color.Green, 0.5f);
            //    d.noGravity = true;
            //    d.velocity *= 0.5f;
            //}
        }

        public override void OnChargeCancelled(Player player)
        {
            CombatText.NewText(player.getRect(), Color.Red, "Activation Cancelled!");
        }

        public override void OnChargeComplete(Player player)
        {

            var mainPlayer = player.GetModPlayer<TransformationPlayer>();

            //int percentage = (int)Projectile.ai[2];
            //ofaPlayer.percentage = percentage;

            int UnbreakableTimer = mainPlayer.CurrentStage switch
            {
                
                QuirkStage.Intermediate => 200,
                QuirkStage.Advanced => 650,
                QuirkStage.Final => 1200,
                _ => 60

            };


            player.AddBuff(ModContent.BuffType<UnbreakableBuff>(), UnbreakableTimer);


            CombatText.NewText(player.getRect(), Color.Red, $"Unbreakable!");


            //for (int i = 0; i < 30; i++)
            //{
            //    Vector2 speed = Main.rand.NextVector2Circular(10f, 10f);
            //    Dust.NewDust(player.position, player.width, player.height, DustID.Electric, speed.X, speed.Y, 0, Color.LightGreen, 2.5f);
            //}


            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}