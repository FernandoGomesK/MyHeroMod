using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers; 
using Terraria.DataStructures;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.FaJin;
using MyHeroMod.content.Quirks.OFA8th;
using MyHeroMod.content.Quirks.AllForOne;
using KhacesCore.Content.System.BaseProjectiles;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Items.Support.DekuArmor.Projectiles;


namespace MyHeroMod.content.items.Support.DekuArmor.Projectiles
{ 
    public class ChargearmorDetroitProj : BaseChannelingProj
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

            
            if (Projectile.ai[0] == 1) 
            {
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FullCowlingActivationSound"), player.position);
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
            // CombatText.NewText(player.getRect(), Color.Red, "Smash Cancelled!");
        }

        public override void OnChargeComplete(Player player)
        {
           
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
     
          
            
                int MaxDamage = 2500;

        
               


                var attackName = "Detroit Smash!";
                
                CombatText.NewText(player.getRect(), Color.LimeGreen, attackName);

                
                Vector2 Direction = Main.MouseWorld - player.Center;
                Direction.Normalize();
                Vector2 Velocity = Direction * 15f;
                Vector2 BaseSpawnLocation = player.Center + (Direction * 90f);

                int numberOfPunches = player.HasBuff(ModContent.BuffType<GearshiftBuff>()) ? 5 : 1; 

                for (int i = 0; i < numberOfPunches; i++)
                {
                    Vector2 spacing = Direction * (25f * i);
                    Vector2 currentSpawn = BaseSpawnLocation - spacing;
                    
                    Projectile.NewProjectile(player.GetSource_FromThis(), currentSpawn, Velocity, ModContent.ProjectileType<DetroitArmorSmashProj>(), MaxDamage, 2f, player.whoAmI);
                    Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<PunchAttackProj>(), 0, 0f, player.whoAmI);
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
                }
                
                var projectileType = player.HasBuff(ModContent.BuffType<GearshiftBuff>()) ? ModContent.ProjectileType<GearDekuDetroitSmashOnomatopoeia>() : ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>();
                Vector2 textPosition = player.Center + new Vector2(0, -30f);
                Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, projectileType, 0, 0f, player.whoAmI);

            
        
    
            
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}
    
