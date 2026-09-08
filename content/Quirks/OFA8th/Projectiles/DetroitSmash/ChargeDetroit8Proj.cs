using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers; 
using Terraria.DataStructures;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.OFA8th;
using KhacesCore.Content.System.BaseProjectiles;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;

namespace MyHeroMod.content.Quirks.OFA8th.Projectiles
{ 
    public class ChargeDetroit8Proj : BaseChannelingProj
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
                // Note: I changed the dust color to Yellow here so the 8th has a unique visual!
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric, 0, 0, 100, Color.Yellow, 0.5f);
                d.noGravity = true;
                d.velocity *= 0.5f;   
            }
        }

        public override void OnChargeCancelled(Player player)
        {
            
        }

        public override void OnChargeComplete(Player player)
        {
            var ofa8Player = player.GetModPlayer<OneForAll8thPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            int MaxDamage = 35;
            float DamageMultiplier = 1f;
        
            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial: MaxDamage = 35; break;
                case QuirkStage.Adequation: MaxDamage = 65; break;
                case QuirkStage.Intermediate: MaxDamage = 130; break;
                case QuirkStage.Advanced: MaxDamage = 280; break;
                case QuirkStage.Final: MaxDamage = 850; break;
                default: MaxDamage = 35; break;
            }

            if (transPlayer.CurrentStage >= QuirkStage.Adequation)
                CombatText.NewText(player.getRect(), Color.Yellow, "Detroit Smash!");
            else
                CombatText.NewText(player.getRect(), Color.White, "Super Punch!");

            Vector2 Direction = Main.MouseWorld - player.Center;
            Direction.Normalize();
            Vector2 Velocity = Direction * 15f;
            Vector2 BaseSpawnLocation = player.Center + (Direction * 90f);
            Vector2 currentSpawn = BaseSpawnLocation - (Direction * 25f);

            if (player.HasBuff(ModContent.BuffType<StockPileBuff>()) || ofa8Player.form == 1) {
                DamageMultiplier = 1.5f; 
            }
            else if (player.HasBuff(ModContent.BuffType<StockPileBuff>() ) || ofa8Player.form == 2)  {
                DamageMultiplier = 2.5f;
            }

            int FinalDamage = (int)(MaxDamage * DamageMultiplier);
            
            Projectile.NewProjectile(player.GetSource_FromThis(), currentSpawn, Velocity, ModContent.ProjectileType<DetroitSmashProj>(), FinalDamage, 2f, player.whoAmI);
            Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<OFA9th.Projectiles.PunchAttackProj>(), 0, 0f, player.whoAmI);
            Vector2 textPosition = player.Center + new Vector2(0, -30f);
            Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<DetroitSmashOnomatopoeia>(), 0, 0f, player.whoAmI);
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
            
            // ====================================================== IMPACT SHAKE ======================================================
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}