using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;
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



        
            int MaxDamage = 100;
            int FinalDamage = 0;
            bool consumeFinger = false;
            bool hurtPlayer = false;

            if  (player.HasBuff(ModContent.BuffType<FullCowlingBuff5>()))
            {
                FinalDamage = (int)(MaxDamage * 0.05f);
                hurtPlayer = false;
                consumeFinger = false;
            }
            else if (player.HasBuff(ModContent.BuffType<FullCowlingBuff10>()))
            {
                FinalDamage = (int)(MaxDamage * 0.10f);
                hurtPlayer = false;
                consumeFinger = false;
            }
            else if (player.HasBuff(ModContent.BuffType<FullCowlingBuff45>()))
            {
                FinalDamage = (int)(MaxDamage * 0.45f);
                hurtPlayer = false;
                consumeFinger = false;
            }
            else
            {
                FinalDamage = MaxDamage;
                hurtPlayer = true;
                consumeFinger = true;
            }
            if (consumeFinger && ofaPlayer.Fingers <= 0)
            {
                CombatText.NewText(player.getRect(), Color.Red, "No fingers left!");
                return;
            }

            if (consumeFinger) ofaPlayer.Fingers--;

            Vector2 Velocity = Main.MouseWorld - player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

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

            if (hurtPlayer)
            {
                player.statLife -= 10;
                if (player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", player.name));
                        player.KillMe(reason, FinalDamage, 0);        
                }
            }
        }
        }