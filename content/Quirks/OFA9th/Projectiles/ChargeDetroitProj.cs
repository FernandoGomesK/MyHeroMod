using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Graphics.CameraModifiers; 
using Terraria.DataStructures;
using MyHeroMod.content.System;
using MyHeroMod.content.Projectiles.Base;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Buffs;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.FaJin;
using MyHeroMod.content.Quirks.OFA8th;
using MyHeroMod.content.Quirks.AllForOne;

namespace MyHeroMod.content.Quirks.OFA9th.Projectiles
{ 
    public class ChargeDetroitProj : BaseChannelingProj
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
            CombatText.NewText(player.getRect(), Color.Red, "Smash Cancelled!");
        }

        public override void OnChargeComplete(Player player)
        {
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            var afoPlayer = player.GetModPlayer<AllForOnePlayer>();
            var ofa8Player = player.GetModPlayer<OneForAll8thPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();
            var FaJinPlayer = player.GetModPlayer<FajinPlayer>();

            bool isOFA9th = transPlayer.HasActiveQuirk(QuirkType.OneForAll9th)  || (transPlayer.HasActiveQuirk(QuirkType.AllForOne) && afoPlayer.HasInternalQuirk(QuirkType.OneForAll9th));
            bool isOFA8th = transPlayer.HasActiveQuirk(QuirkType.OneForAll8th) || (transPlayer.HasActiveQuirk(QuirkType.AllForOne) && afoPlayer.HasInternalQuirk(QuirkType.OneForAll8th) && !isOFA9th);

            int MaxDamage = 150;
            float DamageMultiplier = 1f;
            bool hurtPlayer = false;
            bool usedFaJin = false;

            // --- 9TH GEN LOGIC ---
            if (isOFA9th)
            {
                switch(transPlayer.CurrentStage){
                    case QuirkStage.Initial: MaxDamage = 150; break;
                    case QuirkStage.Adequation: MaxDamage = 400; break;
                    case QuirkStage.Intermediate: MaxDamage = 600; break;
                    case QuirkStage.Advanced: MaxDamage = 1100; break;
                    case QuirkStage.Final: MaxDamage = 2500; break;
                    default: MaxDamage = 150; break;
                }

                if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 45) {
                    DamageMultiplier = 0.45f; 
                }
                else if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 10) {
                    DamageMultiplier = 0.10f; // Fixed a tiny bug here! You had 0.010f which is 1%, changed to 0.10f
                }
                else if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 5) {
                    DamageMultiplier = 0.05f; 
                }
                else {
                    DamageMultiplier = 1.0f; 
                    hurtPlayer = true;
                }

                if (player.HasBuff(ModContent.BuffType<FaJinBuff>()))
                {
                    DamageMultiplier += 0.55f; 
                    FaJinPlayer.FaJinCharges = 0; 
                    player.ClearBuff(ModContent.BuffType<FaJinBuff>());
                    usedFaJin = true;
                }

                int FinalDamage = (int)(MaxDamage * DamageMultiplier);
                string attackName = usedFaJin ? "Faux " : "";

                if (usedFaJin || !hurtPlayer)
                    attackName += (DamageMultiplier * 100).ToString("0") + "% Detroit Smash";
                else
                    attackName += "Detroit Smash";

                attackName += player.HasBuff(ModContent.BuffType<GearshiftBuff>()) ? ": Quintuple" : "!";
                
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
                    
                    Projectile.NewProjectile(player.GetSource_FromThis(), currentSpawn, Velocity, ModContent.ProjectileType<DetroitSmashProj>(), FinalDamage, 2f, player.whoAmI);
                    Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<PunchAttackProj>(), 0, 0f, player.whoAmI);
                    SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
                }

                if (hurtPlayer)
                {
                    player.statLife -= (int)(0.25f * player.statLifeMax2);
                    if (player.statLife <= 0)
                    {
                        var reason = PlayerDeathReason.ByCustomReason(Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", player.name));
                        player.KillMe(reason, FinalDamage, 0);
                    }
                }
            }
            
            else if (isOFA8th)
            {
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
                Projectile.NewProjectile(player.GetSource_FromThis(), BaseSpawnLocation, Velocity, ModContent.ProjectileType<PunchAttackProj>(), 0, 0f, player.whoAmI);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
            }

            // --- IMPACT SHAKE ---
            
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);
        }
    }
}