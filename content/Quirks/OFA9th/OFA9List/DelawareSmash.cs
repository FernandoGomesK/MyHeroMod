using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using Terraria.DataStructures;
using MyHeroMod.content.Projectiles;
using Terraria.Graphics.CameraModifiers;

namespace MyHeroMod.content.Quirks.OFA9th.Skills 
{
    public class DelawareSmashSkill : QuirkBaseSkill
    {
        public override string Name => "Delaware Smash";
        public override string Description => "Propel air forward with a flick of your fingers";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/DelawareSmashIcon";
        public override int BaseCooldown => 200;
        public override string Category => "OneForAll9th";

        public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;

        public override void OnUse(Player player)
        {
            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();

           
            int MaxDamage = ofaPlayer.CalculateStageDamage(50, 150, 300, 550, 1200);

           
            bool hasFullCowling = player.HasBuff(ModContent.BuffType<FullCowlingBuff>());
            bool hurtPlayer = !hasFullCowling;
            bool consumeFinger = !hasFullCowling;
            
            
            float DamageMultiplier = 1f;
            if (hasFullCowling)
            {
                DamageMultiplier = ofaPlayer.GetAirForceDamageMultiplier();
            }

            
            if (consumeFinger)
            {
                if (ofaPlayer.currentFingers <= 0)
                {
                    CombatText.NewText(player.getRect(), Color.Red, "No fingers left!");
                    return; 
                }
                ofaPlayer.currentFingers--;
            }

      
            int FinalDamage = (int)(MaxDamage * DamageMultiplier);
            Vector2 Velocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero);
            
            if (hurtPlayer)
            {   
                Velocity *= 30f;
            }
            else
            {
            
                float airForceSpeedMod = ofaPlayer.isAirForceOn ? 10f : 0f;
                Velocity *= 15f + airForceSpeedMod;
            }

         
            if (player.HasBuff(ModContent.BuffType<FloatBuff>()))
            {
                player.velocity = -Velocity * 2f;
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Cloud, Velocity.X * 2, Velocity.Y * 2, 0, default, 1f);
                }
            }
            
        
            Vector2 textPosition = player.Center + new Vector2(0, -30f);
            Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<DekuDetroitSmashOnomatopoeia>(), 0, 0f, player.whoAmI);
          
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Velocity, ModContent.ProjectileType<DelawareSmashProj>(), FinalDamage, 2f, player.whoAmI);
            
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, player.position);
            PunchCameraModifier shake = new PunchCameraModifier(player.Center, Main.rand.NextVector2CircularEdge(1f, 1f), 10f, 15f, 20, 1000f, "FullCowlingShake");
            Main.instance.CameraModifiers.Add(shake);

            if (hurtPlayer)
            {    
                ofaPlayer.ApplyRecoilDamage(0.05f); 
            }
        }
    }
}