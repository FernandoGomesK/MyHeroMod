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
using MyHeroMod.content.Quirks.FaJin;


public class StLouisSmashSkill : QuirkSkill
{
    public override string Name => "ST. Louis Smash";
    public override string Description => "Propel air forward with a flick of your fingers";
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
                DamageMultiplier += 0.55f; // Increase damage by 25% if Fa Jin is stored
                FaJinPlayer.FaJinCharges = 0; // Consume all Fa Jin charges
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
                attackName += (DamageMultiplier * 100).ToString("0") + "% St. Louis Smash";
            }
            else
            {
                attackName += "St. Louis Smash";
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

         Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<STLouisSmashController>(),
                FinalDamage, // Dano alto (Impacto)
                10f, // Knockback alto
                player.whoAmI
            );

            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);   
        }
        else
        {
            Main.NewText("You need to be in a Full Cowling to use this skill!", Color.Red);
        }
            

            
    }}
