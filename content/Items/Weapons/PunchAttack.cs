using MyHeroMod.content.System; // Para acessar SkillData/QuirkType
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OFA9th.Projectiles; // Para TransformationPlayer
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
            Item.damage = 14; 
            Item.DamageType = DamageClass.Melee;
            Item.width = 30;
            Item.height = 30;
            
            
            
            Item.useTime = 10;      
            Item.useAnimation = 10; 
            Item.useStyle = ItemUseStyleID.Swing; 
            
            Item.noMelee = true;   
            Item.knockBack = 4f;    
            Item.value = 0;
            Item.rare = ItemRarityID.White;
            Item.autoReuse = true;  
            
             
            Item.shoot = ModContent.ProjectileType<PunchAttackProj>(); 
                
            
            Item.shootSpeed = 25f;

            Item.useTurn = true;    
            Item.noUseGraphic = true; 
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            
                
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20)); 
                float scale = 1f - (Main.rand.NextFloat() * 0.1f);
                perturbedSpeed = perturbedSpeed * scale; 

                
                Vector2 offset = velocity.RotatedBy(MathHelper.PiOver2);
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

        
        public override bool CanUseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();

            
            bool isPunchUser = modPlayer.HasActiveQuirk(QuirkType.OneForAll9th) || 
                               modPlayer.HasActiveQuirk(QuirkType.OneForAll8th) || 
                               modPlayer.HasActiveQuirk(QuirkType.Gearshift) || 
                               modPlayer.HasActiveQuirk(QuirkType.Overclock);

            return isPunchUser; 
        }
        

        
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();
            
        
            if (modPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                damage += 0.5f; 
            
            if (modPlayer.CurrentStage >= QuirkStage.Final)
            {
                damage += 1.0f; 
            }
        }
        
    }
}
}

