using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.Quirks.FaJin;

namespace MyHeroMod.content.Quirks.OFA9th.Skills 
{
    public class StLouisSmashSkill : QuirkBaseSkill
    {
        public override string Name => "ST. Louis Smash";
        public override string Description => "Jump and do a diving Kick at your Cursor";
        public override string IconPath => "MyHeroMod/Assets/SkillIcons/OFA9th/stLouisIcon";

        public override int BaseCooldown => 120;
        public override string Category => "OneForAll9th";

        public override QuirkType RequiredQuirk => QuirkType.OneForAll9th;
        public override QuirkStage RequiredStage => QuirkStage.Intermediate;
        public override bool IsDefaultSkill => false;
        

        public override void OnUse(Player player)
        {
      
            if (!player.HasBuff(ModContent.BuffType<FullCowlingBuff>()))
            {
                CombatText.NewText(player.getRect(), Color.Red, "Requires Full Cowling!");
                return;
            }

            var ofaPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            
            int MaxDamage = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 130,
                QuirkStage.Adequation => 350,
                QuirkStage.Intermediate => 500,
                QuirkStage.Advanced => 950,
                QuirkStage.Final => 2200,
                _ => 130
            };

            
            float DamageMultiplier = ofaPlayer.percentage switch
            {
                45 => 0.45f,
                20 => 0.20f,
                10 => 0.10f,
                5 => 0.05f,
                _ => 1f
            };

            bool usedFaJin = false;

            
            if (player.HasBuff(ModContent.BuffType<FaJinBuff>()))
            {
                var faJinPlayer = player.GetModPlayer<FajinPlayer>();
                DamageMultiplier += 0.55f;
                faJinPlayer.FaJinCharges = 0; 
                player.ClearBuff(ModContent.BuffType<FaJinBuff>());
                usedFaJin = true;
            }

           
            float ironSolesMultiplier = ofaPlayer.isIronSolesOn ? 1.30f : 1f;
            int FinalDamage = (int)(MaxDamage * DamageMultiplier * ironSolesMultiplier);
            
            string attackName = usedFaJin ? "Faux " : "";
            attackName += $"{(DamageMultiplier * 100):0}% St. Louis Smash";
            
            if (player.HasBuff(ModContent.BuffType<GearshiftBuff>()))
            {
                attackName += ": OVERDRIVE";
            }
            else 
            {
                attackName += "!";
            }
            
            CombatText.NewText(player.getRect(), Color.LimeGreen, attackName);

            // 7. Fire Projectile
            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero, 
                ModContent.ProjectileType<STLouisSmashController>(),
                FinalDamage, 
                10f, 
                player.whoAmI
            );

            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
            
        }
    }
}