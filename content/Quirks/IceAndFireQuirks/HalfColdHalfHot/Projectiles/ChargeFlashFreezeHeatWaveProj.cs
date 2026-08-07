using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.SimpleProjectiles.FireBall;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Frost.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles
{
    public class ChargeFlashFreezeHeatWaveProj : BaseChannelingProj 

    {

    public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";

    protected override int ChannelTime => 240;

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
                // SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
                // Vector2 textPosition = player.Center + new Vector2(0, -30f);
                // Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<Deku1000000DetroitOnomatopoeia>(), 0, 0f, player.whoAmI);
            }

            if (Projectile.ai[0] == 120)
            {
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                
                
                int iceDamage = transPlayer.CurrentStage switch
                {
                    QuirkStage.Initial => 50,    
                    QuirkStage.Adequation => 100,  
                    QuirkStage.Intermediate => 180,
                    QuirkStage.Advanced => 300,    
                    QuirkStage.Final => 600,       
                    _ => 100
                };

            
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), 
                    player.Center, 
                    Vector2.Zero, 
                    ModContent.ProjectileType<FreezingIceBeamController>(), 
                    iceDamage, 
                    2f, 
                    player.whoAmI,
                    120f 
                );
            }
        }

          public override void SpawnChargingDust(Player player)
        {
            // if (Main.rand.NextBool(2))
            // {
            //     Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, Color.Green, 0.5f);
            //     d.noGravity = true;
            //     d.velocity *= 0.5f;   
            //     Dust d2 = Dust.NewDustDirect(player.position, player.width, player.height, DustID.RedTorch, 0, 0, 100, Color.Red, 0.5f);
            //     d2.noGravity = true;
            //     d2.velocity *= 1.5f;
            // }
        }

        public override void OnChargeComplete(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            int maxDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 250,    
                QuirkStage.Adequation => 600,  
                QuirkStage.Intermediate => 1250,
                QuirkStage.Advanced => 2250,    
                QuirkStage.Final => 4250,       
                _ => 1200
            };

            Vector2 Direction = Main.MouseWorld - player.Center;
            Direction.Normalize();
            Vector2 Velocity = Direction * 15f;
            Vector2 BaseSpawnLocation = player.Center + (Direction * 90f);

            Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<FireBallProj>(), maxDamage, 15f, player.whoAmI);
            // SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.8f, Pitch = +0.3f }, player.position);

            // Vector2 textPosition = player.Center + new Vector2(0, -30f);
            // Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>(), 0, 0f, player.whoAmI);

            
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 20f, 25f, 30, 1500f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}

    