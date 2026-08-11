using KhacesCore.Content.System.BaseProjectiles;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Projectiles.Base;
using MyHeroMod.content.Projectiles.GreyOnomatopoeias;
using MyHeroMod.content.System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles
{ 
    public class ChargeHellJetBurnProj : BaseChannelingProj
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
                Vector2 textPosition = player.Center + new Vector2(0, -30f);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/CremationSound"), player.position);
                Projectile.NewProjectile(
                player.GetSource_FromThis(),
                textPosition,
                Vector2.Zero, 
                ModContent.ProjectileType<GreyJetOnomatopoeia>(),
                0, 
                0f, 
                player.whoAmI
                );

            }
        }

        public override void SpawnChargingDust(Player player)
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
                ModContent.ProjectileType<HellJetBurnController>(),
                Projectile.damage, 
                2f, 
                player.whoAmI,
                60f
            );

            Vector2 textPosition = player.Center + new Vector2(0, -30f);


            int projID = Projectile.NewProjectile(
            player.GetSource_FromThis(),
            textPosition,
            Vector2.Zero, 
            ModContent.ProjectileType<GreyBurnOnomatopoeia>(),
            0,  
            0f, 
            player.whoAmI
        );

        
        

            
           

            ImpactFrameSystem.Trigger(Color.Orange, false,
                "MyHeroMod/Assets/Effects/BlankImpactImage", 
                "MyHeroMod/Assets/Effects/SpeedImpactImage"
            );
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "PhosphorShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}