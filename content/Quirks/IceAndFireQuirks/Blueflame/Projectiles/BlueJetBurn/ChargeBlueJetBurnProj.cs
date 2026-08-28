using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Projectiles.Fire;
using MyHeroMod.content.Projectiles.GreyOnomatopoeias;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles
{ 
    public class ChargeBlueJetBurnProj : BaseChannelingProj
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
                 var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
                if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
                {   
                Vector2 textPosition = player.Center + new Vector2(0, -30f);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/CremationSound"), player.position);
                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                textPosition,
                Vector2.Zero, 
                ModContent.ProjectileType<JetOnomatopoeia>(),
                0, 
                0f, 
                player.whoAmI
                );
                }

            }
        }

        public override void SpawnChargingDust(Player player)
        {
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            
            if (transPlayer.CurrentStage >= QuirkStage.Adequation)
            {   
                Lighting.AddLight(player.Center, new Vector3(0.4f, 0.7f, 1f) * 1.5f);
                for (int i = 0; i < 2; i++)
                {
                    
                    int blueFire = Dust.NewDust(player.position - new Vector2(4, 4), player.width + 8, player.height + 8, DustID.BlueTorch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[blueFire].noGravity = true;
                    Main.dust[blueFire].velocity.Y -= Main.rand.NextFloat(1f, 3.5f); 
                    Main.dust[blueFire].velocity.X *= 0.3f;
                    Main.dust[blueFire].velocity += player.velocity * 0.4f; 
                    
                    if (Main.rand.NextBool(2)) 
                    {
                        int whiteFire = Dust.NewDust(player.position, player.width, player.height, DustID.IceTorch, 0f, 0f, 50, default, 1.7f);
                        Main.dust[whiteFire].noGravity = true;
                        Main.dust[whiteFire].velocity.Y -= Main.rand.NextFloat(2f, 5f); 
                        Main.dust[whiteFire].velocity.X *= 0.2f;
                        Main.dust[whiteFire].velocity += player.velocity * 0.5f;
                    }

                    if (Main.rand.NextBool(4)) 
                    {
                        int spark = Dust.NewDust(player.position, player.width, player.height, DustID.FireworkFountain_Blue, 0f, 0f, 0, default, 1.2f);
                        Main.dust[spark].noGravity = true;
                        Main.dust[spark].velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-5f, -1f));
                    }
                }
            }
            else
            {
                 Lighting.AddLight(player.Center, new Vector3(0.4f, 0.7f, 1f) * 1.5f);
            
        
            for (int i = 0; i < 2; i++)
            {
                
                int blueFire = Dust.NewDust(player.position - new Vector2(4, 4), player.width + 8, player.height + 8, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                Main.dust[blueFire].noGravity = true;
                Main.dust[blueFire].velocity.Y -= Main.rand.NextFloat(1f, 3.5f); 
                Main.dust[blueFire].velocity.X *= 0.3f;
                Main.dust[blueFire].velocity += player.velocity * 0.4f; 
                
                
                if (Main.rand.NextBool(2)) 
                {
                    int whiteFire = Dust.NewDust(player.position, player.width, player.height, DustID.RedTorch, 0f, 0f, 50, default, 1.7f);
                    Main.dust[whiteFire].noGravity = true;
                    Main.dust[whiteFire].velocity.Y -= Main.rand.NextFloat(2f, 5f); 
                    Main.dust[whiteFire].velocity.X *= 0.2f;
                    Main.dust[whiteFire].velocity += player.velocity * 0.5f;
                }

                
                if (Main.rand.NextBool(4)) 
                {
                    int spark = Dust.NewDust(player.position, player.width, player.height, DustID.FireworkFountain_Red, 0f, 0f, 0, default, 1.2f);
                    Main.dust[spark].noGravity = true;
                    
                    Main.dust[spark].velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-5f, -1f));
                }
            }
            }
        }


        public override void OnChargeCancelled(Player player)
        {
            CombatText.NewText(player.getRect(), Color.Red, "Activation Cancelled!");
        }

        public override void OnChargeComplete(Player player)
        {
             var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.CurrentStage >= QuirkStage.Adequation)
            {   
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<BlueJetBurnController>(),
                Projectile.damage, 
                2f, 
                player.whoAmI,
                60f
            );
            }
            else
            {
                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<HellJetBurnController>(),
                Projectile.damage, 
                2f, 
                player.whoAmI,
                60f
            );
            }
            Vector2 textPosition = player.Center + new Vector2(player.direction * 65f, -30f);

                var projectile = ModContent.ProjectileType<BlueFooshOnomatopoeia>();
                if (transPlayer.CurrentStage >= QuirkStage.Advanced)
            {
                projectile = ModContent.ProjectileType<FooshOnomatopoeia>();
            }

            
                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                textPosition,
                Vector2.Zero, 
                projectile,
                0, 
                0f, 
                player.whoAmI
                );

            


            

            Color impactColor = (transPlayer.CurrentStage >= QuirkStage.Adequation) ? Color.AliceBlue : Color.Orange;
            ImpactFrameSystem.Trigger(impactColor, false, "MyHeroMod/Assets/Effects/BurnImpactImage");
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "PhosphorShake");
            Main.instance.CameraModifiers.Add(shake);


            foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroTemperature heatUser) 
                {
                    heatUser.AddHeat(25);
                }
            }
        }
    }
}