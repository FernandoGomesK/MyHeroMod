using MyHeroMod.content.System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th.Projectiles; 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.Audio;

namespace MyHeroMod.content.Items.Weapons
{
    public class PunchAttack : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15; 
            Item.DamageType = DamageClass.Melee;
            Item.width = 30;
            Item.height = 30;
            
            Item.useTime = 10;      
            Item.useAnimation = 10; 
            Item.useStyle = ItemUseStyleID.Swing; 
            
            Item.noMelee = false;   
            Item.knockBack = 4f;    
            Item.value = 0;
            Item.rare = ItemRarityID.White;
            Item.autoReuse = true;  
            
            Item.shoot = ModContent.ProjectileType<PunchAttackProj>(); 
            Item.shootSpeed = 20f;

            Item.useTurn = true;    
            Item.noUseGraphic = true; 
        }

        public override void HoldItem(Player player)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();

            
            switch (modPlayer.CurrentStage)
            {
                case QuirkStage.Initial:
                    Item.scale = 1.0f;
                    Item.shootSpeed = 20f;
                    break;
                case QuirkStage.Adequation:
                    Item.scale = 1.2f;
                    Item.shootSpeed = 23f;
                    break;
                case QuirkStage.Intermediate:
                    Item.scale = 1.4f;
                    Item.shootSpeed = 26f;
                    break;
                case QuirkStage.Advanced:
                    Item.scale = 1.6f;
                    Item.shootSpeed = 30f;
                    break;
                case QuirkStage.Final:
                    Item.scale = 2.0f;
                    Item.shootSpeed = 35f;
                    break;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();

           
            float rangeMultiplier = modPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 0.4f,       
                QuirkStage.Adequation => 0.6f,
                QuirkStage.Intermediate => 0.8f,
                QuirkStage.Advanced => 1.0f,
                QuirkStage.Final => 1.0f,         
                _ => 0.6f
            };

            
            Vector2 adjustedVelocity = velocity * rangeMultiplier;

            Vector2 perturbedSpeed = adjustedVelocity.RotatedByRandom(MathHelper.ToRadians(20)); 
            float scale = 1f - (Main.rand.NextFloat() * 0.1f);
            perturbedSpeed = perturbedSpeed * scale; 

            Vector2 offset = adjustedVelocity.RotatedBy(MathHelper.PiOver2);
            offset.Normalize();
            offset *= Main.rand.NextFloat(-16f, 16f);
            
            Vector2 perturbedPosition = position + offset;
            int textureStyle = Main.rand.Next(1); 

            Projectile.NewProjectile(
                source, 
                perturbedPosition, 
                perturbedSpeed, 
                type, 
                damage, 
                knockback, 
                player.whoAmI, 
                textureStyle 
            );

            if (player.ownedProjectileCounts[ModContent.ProjectileType<PunchAnimProj>()] < 1)
            {
                Projectile.NewProjectile(
                    source,
                    player.Center, 
                    velocity,       
                    ModContent.ProjectileType<PunchAnimProj>(),
                    0,             
                    0,             
                    player.whoAmI
                );

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/punchbarrage") 
                { 
                    Volume = 0.50f,
                    MaxInstances = 1, 
                    SoundLimitBehavior = SoundLimitBehavior.IgnoreNew 
                }, player.position);
            }

            return false;
        }
        
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();
       
            float stageDamage = modPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 14f,      
                QuirkStage.Adequation => 28f,    
                QuirkStage.Intermediate => 45f, 
                QuirkStage.Advanced => 75f,     
                QuirkStage.Final => 130f,        
                _ => 10f, 
            };

            damage.Flat += stageDamage;
        }

        public override void AddRecipes()
        { 
            CreateRecipe()
                .Register();
        }
    }
}