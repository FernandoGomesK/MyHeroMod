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
using MyHeroMod.content.Quirks.FaJin;
using MyHeroMod.content.Quirks.OFA8th;
using MyHeroMod.content.Quirks.AllForOne;


public class DetroitSmashSkill : QuirkSkill
{
    public override string Name => "Detroit Smash";
    public override string Description => "Propel air forward with a flick of your fingers";
    public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.Quirkless;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => false;

    public override bool CheckUnlock(TransformationPlayer player)
    {
        var afoPlayer = player.Player.GetModPlayer<AllForOnePlayer>();

        if (player.SelectedQuirk == QuirkType.OneForAll8th ||player.SelectedQuirk ==  QuirkType.OneForAll9th ) 
            return player.CurrentStage >= QuirkStage.Initial;

        if (player.SelectedQuirk == QuirkType.AllForOne && (afoPlayer.HasInternalQuirk(QuirkType.OneForAll8th) || afoPlayer.HasInternalQuirk(QuirkType.OneForAll9th)))
        {
            return true;
        }




        return false;
    }


    public override void OnUse(Player player)
    {
 
        var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
        var afoPlayer = player.GetModPlayer<AllForOnePlayer>();
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var FaJinPlayer = player.GetModPlayer<FajinPlayer>();

        bool isOFA9th = transPlayer.SelectedQuirk == QuirkType.OneForAll9th  || (transPlayer.SelectedQuirk == QuirkType.AllForOne && afoPlayer.HasInternalQuirk(QuirkType.OneForAll9th));
        bool isOFA8th = transPlayer.SelectedQuirk == QuirkType.OneForAll8th || (transPlayer.SelectedQuirk == QuirkType.AllForOne && afoPlayer.HasInternalQuirk(QuirkType.OneForAll8th) && !isOFA9th);

            int MaxDamage = 450;
            float DamageMultiplier = 1f;
            bool hurtPlayer = false;
            bool usedFaJin = false;

            if (isOFA9th)
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
                DamageMultiplier = 1.0f; 
                hurtPlayer = true;
            }
            if  (player.HasBuff(ModContent.BuffType<FaJinBuff>()))
            {
                DamageMultiplier += 0.55f; 
                FaJinPlayer.FaJinCharges = 0; 
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

            
            
            int numberOfPunches = player.HasBuff(ModContent.BuffType<GearshiftBuff>()) ? 5 : 1; 

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
                player.statLife -= 40;
                if (player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", player.name));
                    player.KillMe(reason, FinalDamage, 0);
                }
        }
        }
        
        else if (isOFA8th)
        {
            if (transPlayer.CurrentStage >= QuirkStage.Adequation)
            {
                CombatText.NewText(player.getRect(), Color.Yellow, "Detroit Smash!");
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.White, "Super Punch!");
            }

            

            Vector2 Direction = Main.MouseWorld - player.Center;
            Direction.Normalize();
            Vector2 Velocity = Direction * 15f;

            Vector2 BaseSpawnLocation = player.Center + (Direction * 90f);
            
            Vector2 spacing = Direction * 25f;
            Vector2 currentSpawn = BaseSpawnLocation - spacing;

            if (player.HasBuff(ModContent.BuffType<StockPileBuff>())) {
                DamageMultiplier = 1.5f; 
            }
            else if (player.HasBuff(ModContent.BuffType<FullCowlingBuff>())) {
                DamageMultiplier = 2.0f;
            }

             int FinalDamage = (int)(MaxDamage * DamageMultiplier);
                
    
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

            
//         }
        } 
        
        }
        
        }