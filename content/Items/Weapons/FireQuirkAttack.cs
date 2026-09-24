using MyHeroMod.content.Buffs; 
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Projectiles;
using MyHeroMod.content.System;
using MyHeroMod.content.System.BaseProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Weapons
{
    public class FireQuirkAttack : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Magic;
            Item.width = 28;
            Item.height = 30;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0.5f;
            Item.value = 0;
            Item.rare = ItemRarityID.White;

            Item.channel = true;
            Item.noUseGraphic = true;

            Item.shoot = ModContent.ProjectileType<ContinuousFlamethrowerProj>();
            Item.shootSpeed = 0f;
        }

        public override bool CanUseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();

            bool isBlueflame = modPlayer.HasActiveQuirk(QuirkType.Blueflame);
            bool isStandardFire = modPlayer.HasActiveQuirk(QuirkType.HellFlames) || modPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot);

            if (!isBlueflame && !isStandardFire)
            {
                return false;
            }

            if (isStandardFire && !isBlueflame)
            {
                if (player.HasBuff(ModContent.BuffType<Heatstroke>()))
                {
                    return false;
                }
            }

            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();

            float stageDamage = modPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 5f,
                QuirkStage.Adequation => 15f,
                QuirkStage.Intermediate => 30f,
                QuirkStage.Advanced => 50f,
                QuirkStage.Final => 80f,
                _ => 5f,
            };

            damage.Flat += stageDamage;
        }
    }
}