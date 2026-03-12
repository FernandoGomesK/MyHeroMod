using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.OFA9th;
using Terraria.DataStructures;


public class DelawareSmashSkill : QuirkSkill
{
    public override string Name => "Delaware Smash";
    public override string Description => "Propel air forward with a flick of your fingers";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {

        var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();


        
            

            int MaxDamage = 50;

            switch(transPlayer.CurrentStage){
                case QuirkStage.Initial:
                MaxDamage = 50;
                break;
            
                case QuirkStage.Adequation:
                MaxDamage = 150;
                break;
          
                case QuirkStage.Intermediate:
                MaxDamage = 300;
                break;
            
                case QuirkStage.Advanced:
                MaxDamage = 550;
                break;
          
                case QuirkStage.Final:
                MaxDamage = 1200;
                break;
        
                default:
                MaxDamage =150;
                break;
                    
            }
            int FinalDamage = 0;
            float airForceMod = 10f;
            bool consumeFinger = false;
            bool hurtPlayer = false;
            float DamageMultiplier = 1f;

            

            if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()))
            {
                if (ofaPlayer.percentage == 45) DamageMultiplier = ofaPlayer.isAirForceOn ? 0.60f : 0.45f;
                else if (ofaPlayer.percentage == 10) DamageMultiplier = ofaPlayer.isAirForceOn ? 0.25f : 0.10f;
                else if (ofaPlayer.percentage == 5) DamageMultiplier = ofaPlayer.isAirForceOn ? 0.10f : 0.05f;
            }
            else
            {
                hurtPlayer = true;
                consumeFinger = true;
            }

            FinalDamage = (int)(MaxDamage * DamageMultiplier);

            if (consumeFinger && ofaPlayer.currentFingers <= 0)
            {
                CombatText.NewText(player.getRect(), Color.Red, "No fingers left!");
                return; 
            }

            if (consumeFinger) ofaPlayer.currentFingers--;

            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();

            if (hurtPlayer)
            {   
                Velocity *= 30f;
            }
            else if (ofaPlayer.isAirForceOn)
            {
             Velocity *= 15f + airForceMod;
            }
            else
            {
                Velocity *= 15f;
            }
            

            if (player.HasBuff(ModContent.BuffType<FloatBuff>()))
            {
                float recoil = 2f;

                player.velocity = -Velocity * recoil;

                for (int i = 0; i < 10; i++)
        {
            Dust.NewDust(player.position, player.width, player.height, DustID.Cloud, Velocity.X * 2, Velocity.Y * 2, 0, default, 1f);
        }
            }

            

            Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                player.Center, 
                Velocity, 
                ModContent.ProjectileType<DelawareSmashProj>(), 
                FinalDamage, 2f, 
                player.whoAmI);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, player.position);

            if (hurtPlayer)
            {
                player.statLife -= (int)(player.statLifeMax2 * 0.05f);
                if (player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", player.name));
                        player.KillMe(reason, FinalDamage, 0);        
                }
            }
        }
        }