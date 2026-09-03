using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers; 
using Terraria.DataStructures;
using MyHeroMod.content.Buffs;
using KhacesCore.Content.System.BaseProjectiles;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.System;
using System;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{ 
    public class ChargeFinalSmashProj : BaseChannelingProj
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";

        protected override int ChannelTime => 300; 

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
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
                Vector2 textPosition = player.Center + new Vector2(0, -30f);
                Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<Deku1000000DetroitOnomatopoeia>(), 0, 0f, player.whoAmI);
            }
            
            if (Projectile.ai[0] == 60)
            {
                if (Main.rand.NextBool(2))
                {
                    
                    Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, Color.Green, 1.5f);
                    d.noGravity = true;
                    d.velocity *= 1.2f;   
                    
                    Dust d2 = Dust.NewDustDirect(player.position, player.width, player.height, DustID.RedTorch, 0, 0, 100, Color.Red, 1.5f);
                    d2.noGravity = true;
                    d2.velocity *= 2.5f;
                }
            }
            
            if (Projectile.ai[0] >= 120) 
            {
                float corVelocidade = 0.5f; 
                Color corArcoIris = Main.hslToRgb((Main.GlobalTimeWrappedHourly * corVelocidade) % 1f, 1f, 0.6f);
                Color corTranslucida = corArcoIris * 0.5f; 

                Vector2 center = player.Center;
                float raioCirculo = 64f;
                
            
                int numParticulasCirculo = 36; 
                for (int i = 0; i < numParticulasCirculo; i++)
                {
                    float angulo = i * (MathHelper.TwoPi / numParticulasCirculo);
                    Vector2 offset = new Vector2((float)Math.Cos(angulo), (float)Math.Sin(angulo)) * raioCirculo;
                    
                   
                    Dust d = Dust.NewDustPerfect(center + offset, DustID.FireworksRGB, offset * 0.02f, 100, corTranslucida, 1.8f);
                    d.noGravity = true;
                }
                
              
                int numVertices = 8;
                Vector2[] verticesEstrela = new Vector2[numVertices];
                float raioInterno = raioCirculo * 0.3f;

                for (int i = 0; i < numVertices; i++)
                {
                
                    float angulo = i * (MathHelper.TwoPi / numVertices) - MathHelper.PiOver2; 
                    
                   
                    float raioAtual = (i % 2 == 0) ? raioCirculo : raioInterno;
                    
                    verticesEstrela[i] = center + new Vector2((float)Math.Cos(angulo), (float)Math.Sin(angulo)) * raioAtual;
                }

              
                for (int i = 0; i < numVertices; i++)
                {
                    Vector2 inicio = verticesEstrela[i];
                    Vector2 fin = verticesEstrela[(i + 1) % numVertices]; 
                    
                    int passos = 15; 
                    for (int p = 0; p <= passos; p++)
                    {
                        float t = p / (float)passos;
                        Vector2 posicaoPonto = Vector2.Lerp(inicio, fin, t);

                       
                        Dust d = Dust.NewDustPerfect(posicaoPonto, DustID.FireworksRGB, Vector2.Zero, 100, corTranslucida, 1.5f);
                        d.noGravity = true;
                    }
                }
            }
            
           if (Projectile.ai[0] >= 200) 
            {
                float corVelocidade = 0.5f; 
                Color corArcoIris = Main.hslToRgb((Main.GlobalTimeWrappedHourly * corVelocidade) % 1f, 1f, 0.6f);
                Color corTranslucida = corArcoIris * 0.5f; 

                Vector2 center = player.Center;
                float raioCirculo = 64f;
                
            
                int numParticulasCirculo = 36; 
                for (int i = 0; i < numParticulasCirculo; i++)
                {
                    float angulo = i * (MathHelper.TwoPi / numParticulasCirculo);
                    Vector2 offset = new Vector2((float)Math.Cos(angulo), (float)Math.Sin(angulo)) * raioCirculo;
                    
                   
                    Dust d = Dust.NewDustPerfect(center + offset, DustID.FireworksRGB, offset * 0.02f, 100, corTranslucida, 1.8f);
                    d.noGravity = true;
                }
                
              
                int numVertices = 8;
                Vector2[] verticesEstrela = new Vector2[numVertices];
                float raioInterno = raioCirculo * 0.3f;

                for (int i = 0; i < numVertices; i++)
                {
                
                    float angulo = i * (MathHelper.TwoPi / numVertices) - MathHelper.PiOver2; 
                    
                   
                    float raioAtual = (i % 2 == 0) ? raioCirculo : raioInterno;
                    
                    verticesEstrela[i] = center + new Vector2((float)Math.Cos(angulo), (float)Math.Sin(angulo)) * raioAtual;
                }

              
                for (int i = 0; i < numVertices; i++)
                {
                    Vector2 inicio = verticesEstrela[i];
                    Vector2 fin = verticesEstrela[(i + 1) % numVertices]; 
                    
                    int passos = 15; 
                    for (int p = 0; p <= passos; p++)
                    {
                        float t = p / (float)passos;
                        Vector2 posicaoPonto = Vector2.Lerp(inicio, fin, t);

                       
                        Dust d = Dust.NewDustPerfect(posicaoPonto, DustID.FireworksRGB, Vector2.Zero, 100, corTranslucida, 1.5f);
                        d.noGravity = true;
                    }
                }
            }
        }

        public override void SpawnChargingDust(Player player)
        {
            if (Main.rand.NextBool(2))
            {
            
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, Color.Green, 1.5f);
                d.noGravity = true;
                d.velocity *= 1.2f;   
                
                Dust d2 = Dust.NewDustDirect(player.position, player.width, player.height, DustID.RedTorch, 0, 0, 100, Color.Red, 1.5f);
                d2.noGravity = true;
                d2.velocity *= 2.5f;
            }
        }

        public override void OnChargeComplete(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            var OfaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

            OfaPlayer.isQuirkless = true;

            int maxDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Final => 50000,       
                _ => 5000
            };

            Vector2 Direction = Main.MouseWorld - player.Center;
            Direction.Normalize();
            Vector2 Velocity = Direction * 15f;
            Vector2 BaseSpawnLocation = player.Center + (Direction * 90f);

            Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<DetroitSmashProj>(), maxDamage, 15f, player.whoAmI);
            Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<PunchAttackProj>(), maxDamage / 2, 0f, player.whoAmI);
            
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.8f, Pitch = +0.3f }, player.position);

            Vector2 textPosition = player.Center + new Vector2(0, -30f);
            Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>(), 0, 0f, player.whoAmI);

            player.AddBuff(BuffID.Weak, 3600); 
            player.AddBuff(BuffID.BrokenArmor, 3600); 

            ImpactFrameSystem.ImpactTimer = 4; 
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 20f, 25f, 30, 1500f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}