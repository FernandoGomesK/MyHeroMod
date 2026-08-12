using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Frost.Projectiles;
using MyHeroMod.content.System;
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

           
            if (Projectile.ai[0] == 120)
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/TodorokiIce") with { Volume = 0.5f, Pitch = +0.3f }, player.position);
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
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
                    30f
                ); 


                hchhPlayer.IsFlashFireFistActive = true;
                player.AddBuff(ModContent.BuffType<FlashfireFistBuff>(), 60); 
                
            }
            
           
            if (Projectile.ai[0] >= 120)
            {
                if (Projectile.ai[0] % 10 == 0)
                {
                    PunchCameraModifier windShake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 2f, 4f, 10, 1500f, "BlizzardWind");
                    Main.instance.CameraModifiers.Add(windShake);
                }

                for (int i = 0; i < 4; i++)
                {
                    Vector2 snowPos = player.Center + new Vector2(Main.rand.NextFloat(-1000f, 1000f), Main.rand.NextFloat(-800f, 800f));
                    int snow = Dust.NewDust(snowPos, 0, 0, DustID.Snow, 0, 0, 100, default, Main.rand.NextFloat(1.5f, 3f));
                    Main.dust[snow].noGravity = true;
                    Main.dust[snow].velocity = new Vector2(player.direction * Main.rand.NextFloat(20f, 45f), Main.rand.NextFloat(-2f, 2f));
                }
            }

            
            if (Projectile.ai[0] >= 130)
            {
                if (Main.rand.NextBool(2)) 
                {
                    
                    Vector2 auraSpawn = player.position + new Vector2(player.direction * 30f, 0);
                    
                    int heatFire = Dust.NewDust(auraSpawn - new Vector2(20, 20), player.width + 40, player.height + 40, DustID.Torch, 0f, 0f, 150, default, 2.5f);
                    Main.dust[heatFire].noGravity = true;
                    
                   
                    Main.dust[heatFire].velocity.X = -player.direction * Main.rand.NextFloat(5f, 12f);
                    Main.dust[heatFire].velocity.Y = Main.rand.NextFloat(-2f, 2f); 
                    Main.dust[heatFire].velocity += player.velocity * 0.4f; 
                }

                
                if (Main.rand.NextBool(4)) 
                {
                    int coreSpark = Dust.NewDust(player.position, player.width, player.height, DustID.BlueTorch, 0f, 0f, 100, default, 1.8f);
                    Main.dust[coreSpark].noGravity = true;
                    
                
                    Main.dust[coreSpark].velocity.X = -player.direction * Main.rand.NextFloat(2f, 6f); 
                    Main.dust[coreSpark].velocity.Y -= Main.rand.NextFloat(1f, 3f); 
                }
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

            bool shouldFlipImage = player.direction == 1;


            ImpactFrameSystem.Trigger(Color.White, shouldFlipImage, 
                "MyHeroMod/Assets/Effects/BlankImpactImage",
                "MyHeroMod/Assets/Effects/FlashFreezeHeatWave/FlashFreezeImpactImage", 
                "MyHeroMod/Assets/Effects/FlashFreezeHeatWave/FlashFreezeImpactImage2", 
                "MyHeroMod/Assets/Effects/FlashFreezeHeatWave/FlashFreezeImpactImage3",
                "MyHeroMod/Assets/Effects/FlashFreezeHeatWave/FlashFreezeImpactImage4"
            );

            
            

            Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<HeatwaveFireBallProj>(), maxDamage, 15f, player.whoAmI);
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/CremationSound") with { Volume = 1.0f, Pitch = -0.2f }, player.position);

            Vector2 textPosition = player.Center + new Vector2(0, -30f);
            Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<FlashFreezeOnomatopoeia>(), 0, 0f, player.whoAmI);

            
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 20f, 25f, 30, 1500f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>()); 

            hchhPlayer.IsFlashFireFistActive = false;
        }
    }
}

    