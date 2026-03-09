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
using MyHeroMod.content.Quirks.Explosion.Projectiles;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Quirks.FaJin;
using JetBrains.Annotations;


public class ManchesterSmashSkill : QuirkSkill
{
    public override string Name => "Manchester Smash";
    public override string Description => "Jump and do a kick at your cursor";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
    public override QuirkStage RequiredStage => QuirkStage.Intermediate;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;


    public override void OnUse(Player player)
    {

        var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
        var FaJinPlayer = player.GetModPlayer<FajinPlayer>();

        

        int MaxDamage = 450;
        float DamageMultiplier = 1f;
        bool hurtPlayer = false;
        bool usedFaJin = false;

        int extraDamage = 0;

        if (ofaPlayer.isIronSolesOn == true)
        {
            extraDamage = 50;
        }
        





            if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()))
        {

            if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 45) {
                DamageMultiplier = 0.45f; 
            }
            else if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 10) {
                DamageMultiplier = 0.010f;
            }
            else if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>()) && ofaPlayer.percentage == 5) {
                DamageMultiplier = 0.05f; 
            }
            else {
                DamageMultiplier = 2.0f; 
                hurtPlayer = true;
            }
            if  (player.HasBuff(ModContent.BuffType<FaJinBuff>()))
            {
                DamageMultiplier += 0.55f; 
                FaJinPlayer.FaJinCharges = 0; 
                player.ClearBuff(ModContent.BuffType<FaJinBuff>());
                usedFaJin = true;
            }

            int FinalDamage = (int)(MaxDamage * DamageMultiplier + extraDamage);

            string attackName = "";

            
            if (usedFaJin)
            {
                attackName += "Faux ";
            }
            if (usedFaJin || !hurtPlayer)
            {
                attackName += (DamageMultiplier * 100).ToString("0") + "% Manchester Smash";
            }
            else
            {
                attackName += "Manchester Smash";
            }
            if (player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            {
                attackName += ": OVERDRIVE";
            }
            else 
            {
                attackName += "!";
            }
            
            CombatText.NewText(player.getRect(), Color.LimeGreen, attackName);

        float isFaJinactiveFloat = usedFaJin ? 1f : 0f;

        Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<ManchesterSmashController>(),
                FinalDamage, // Dano alto (Impacto)
                10f, // Knockback alto
                player.whoAmI,
                0f,
                isFaJinactiveFloat
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, player.position);
        }
        else
        {
            Main.NewText("You need to be in a Full Cowling to use this skill!", Color.Red);
        }

    }}

