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
using Mono.Cecil.Cil;
using MyHeroMod.content.Quirks.OFA9th.Buffs;


public class DetroitSmashSkill : QuirkSkill
{
    public override string Name => "Detroit Smash";
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

            int MaxDamage = 450;
            float DamageMultiplier = 1f;
            bool hurtPlayer = false;
            bool usedFaJin = false;



            

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
                DamageMultiplier = 2.0f; // Sem Full Cowling é o soco suicida (100%)
                hurtPlayer = true;
            }
            if  (player.HasBuff(ModContent.BuffType<FaJinBuff>()))
            {
                DamageMultiplier += 0.55f; // Increase damage by 25% if Fa Jin is stored
                ofaPlayer.FaJinCharges = 0; // Consume all Fa Jin charges
                ofaPlayer.FaJinStored = false;
                player.ClearBuff(ModContent.BuffType<FaJinBuff>());
                usedFaJin = true;
            }

            int FinalDamage = (int)(MaxDamage * DamageMultiplier);

            string attackName = "";

            
            if (usedFaJin)
            {
                attackName += "Faux ";
            }
            if (usedFaJin || !hurtPlayer)
            {
                attackName += (DamageMultiplier * 100).ToString("0") + "% Detroit Smash";
            }
            else
            {
                attackName += "Detroit Smash";
            }
            if (player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            {
                attackName += ": Quintuple";
            }
            else 
            {
                attackName += "!";
            }
            
            CombatText.NewText(player.getRect(), Color.LimeGreen, attackName);

            

            
            Vector2 Direction = Main.MouseWorld - player.Center;
            Direction.Normalize();
            Vector2 Velocity = Direction * 15f;

            Vector2 BaseSpawnLocation = player.Center + (Direction * 90f);

            
            
            int numberOfPunches = ofaPlayer.isGearshiftActive ? 5 : 1; 

            for (int i = 0; i < numberOfPunches; i++)
            {
                Vector2 spacing = Direction * (25f * i);
                Vector2 currentSpawn = BaseSpawnLocation - spacing;
                
    
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), 
                    currentSpawn, 
                    Velocity, 
                    ModContent.ProjectileType<DetroitSmashProj>(), 
                    FinalDamage, 
                    2f, 
                    player.whoAmI
                );
                Projectile.NewProjectile(
                player.GetSource_FromThis(), 
                BaseSpawnLocation, 
                Velocity, 
                ModContent.ProjectileType<PunchAttackProj>(), 
                0,
                0f, 
                player.whoAmI
            );
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
        
 
            
            }
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
        }}