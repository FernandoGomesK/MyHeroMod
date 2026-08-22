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

            int MaxDamage = ofaPlayer.CalculateStageDamage(130, 350,500,950,2200);

            float DamageMultiplier = ofaPlayer.GetFullCowlingMultiplier();  
            DamageMultiplier += ofaPlayer.ConsumeFaJin(out bool usedFaJin); 
            float ironSolesMultiplier = ofaPlayer.GetIronSolesMultiplier();

            
    
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