using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot.Projectiles.JetKindlingProjs
{ 
    public abstract class BaseTodorokiCharge : BaseChannelingProj
    {
        public override string Texture => "MyHeroMod/content/Quirks/Explosion/Projectiles/HowitzerImpact/HowitzerImpactProj";
        protected override int ChannelTime => 30; 

        protected abstract int OuterDustType { get; }
        protected abstract int CoreDustType { get; }
        protected abstract int SparkDustType { get; }
        protected abstract int BeamProjectileType { get; }
        protected abstract int OnomatopoeiaType { get; }
        protected abstract string SoundStylePath { get; }
        protected abstract Vector3 LightColor { get; }
        protected abstract Color ImpactColor { get; }

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
              
                SoundEngine.PlaySound(new SoundStyle(SoundStylePath), player.position);
            }
        }

        public override void SpawnChargingDust(Player player)
        {
           
            Lighting.AddLight(player.Center, LightColor * 1.5f);
            
            for (int i = 0; i < 2; i++)
            {
                
                int outerFire = Dust.NewDust(player.position - new Vector2(4, 4), player.width + 8, player.height + 8, OuterDustType, 0f, 0f, 100, default, 2.5f);
                Main.dust[outerFire].noGravity = true;
                Main.dust[outerFire].velocity.Y -= Main.rand.NextFloat(1f, 3.5f); 
                Main.dust[outerFire].velocity.X *= 0.3f;
                Main.dust[outerFire].velocity += player.velocity * 0.4f; 
                
                if (Main.rand.NextBool(2)) 
                {
                    int coreFire = Dust.NewDust(player.position, player.width, player.height, CoreDustType, 0f, 0f, 50, default, 1.7f);
                    Main.dust[coreFire].noGravity = true;
                    Main.dust[coreFire].velocity.Y -= Main.rand.NextFloat(2f, 5f); 
                    Main.dust[coreFire].velocity.X *= 0.2f;
                    Main.dust[coreFire].velocity += player.velocity * 0.5f;
                }
                
                if (Main.rand.NextBool(4)) 
                {
                    int spark = Dust.NewDust(player.position, player.width, player.height, SparkDustType, 0f, 0f, 0, default, 1.2f);
                    Main.dust[spark].noGravity = true;
                    Main.dust[spark].velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-5f, -1f));
                }
            }
        }

        public override void OnChargeCancelled(Player player)
        {
            CombatText.NewText(player.getRect(), Color.Red, "Activation Cancelled!");
        }

        public override void OnChargeComplete(Player player)
        {
          
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                BeamProjectileType,
                Projectile.damage, 
                2f, 
                player.whoAmI,
                60f
            );

            
            Vector2 textPosition = player.Center + new Vector2(0, -30f);
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                textPosition,
                Vector2.Zero, 
                OnomatopoeiaType,
                0,  
                0f, 
                player.whoAmI
            );

            ImpactFrameSystem.Trigger(ImpactColor, false,
                "MyHeroMod/Assets/Effects/BlankImpactImage", 
                "MyHeroMod/Assets/Effects/SpeedImpactImage"
            );
            
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "PhosphorShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}