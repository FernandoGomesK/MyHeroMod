using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Frost.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.FlashFreezeHeatWave
{
    public class ChargeFlashFreezeHeatWaveProj : BaseChannelingProj 

    {

    public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";

    protected override int ChannelTime => 240;

        public override void AI()
        {
            base.AI();
            Player player = Main.player[Projectile.owner];

    
            Projectile.ai[0]++; 

            if (player.active && !player.dead)
            {
                player.velocity *= 0.6f; 
            }

            if (Projectile.ai[0] == 1) 
            {
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
                    60f
                );
            }
        }
          public override void SpawnChargingDust(Player player)
        {
            float offsetCostas = 20f; 
            Vector2 spawnPos = player.Center - new Vector2(offsetCostas * player.direction, 0f);

            spawnPos.Y += Main.rand.NextFloat(-10f, 10f);
            Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.IceTorch, 0, 0, 100, default, 4.5f);
                d.noGravity = true;
                d.velocity *= 8f;   
                

                
                int iceDust = Dust.NewDust(spawnPos, 4, 4, DustID.IceTorch, 0, 0, 100, default, 4.5f);
                Main.dust[iceDust].noGravity = true;
                Main.dust[iceDust].velocity = new Vector2(-5f * player.direction, 0f);
                player.velocity *= 0.1f; 
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

            Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<HeatwaveFireBallProj>(), maxDamage, 15f, player.whoAmI);
            // SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.8f, Pitch = +0.3f }, player.position);

            // Vector2 textPosition = player.Center + new Vector2(0, -30f);
            // Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>(), 0, 0f, player.whoAmI);

            
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 20f, 25f, 30, 1500f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}

    