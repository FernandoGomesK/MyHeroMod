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
            Item.damage = 14; // Aumentei um pouco pois socos devem doer
            Item.DamageType = DamageClass.Melee;
            Item.width = 30;
            Item.height = 30;
            
            
            // Configuração para Soco (Melee)
            Item.useTime = 10;      // Velocidade do soco (menor = mais rápido)
            Item.useAnimation = 10; // Deve ser igual ao useTime para socos normais
            Item.useStyle = ItemUseStyleID.Swing; // Movimento de balanço/soco
            
            Item.noMelee = true;   // TRUE = apenas projétil. FALSE = o item bate (queremos false)
            Item.knockBack = 4f;    // Empurrão médio
            Item.value = 0;
            Item.rare = ItemRarityID.White;
            Item.autoReuse = true;  // Segurar bate continuamente
            
             
            Item.shoot = ModContent.ProjectileType<PunchAttackProj>(); 
                
            
            Item.shootSpeed = 25f;

            Item.useTurn = true;    // Pode virar enquanto bate
            Item.noUseGraphic = true; // O item (ícone) fica invisível, parecendo que é a mão do player
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            
                // 1. SPREAD DE MIRA (Girar o vetor)
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20)); 
                float scale = 1f - (Main.rand.NextFloat() * 0.1f);
                perturbedSpeed = perturbedSpeed * scale; 

                // 2. BARRAGEM (Deslocar a origem)
                // Cria um vetor perpendicular (90 graus) à direção do tiro para mover para os lados
                Vector2 offset = velocity.RotatedBy(MathHelper.PiOver2);
                offset.Normalize();
                // Multiplica por um valor aleatório entre -16 e 16 pixels (1 bloco para cada lado)
                offset *= Main.rand.NextFloat(-16f, 16f);
                
                // Aplica o deslocamento à posição original
                Vector2 perturbedPosition = position + offset;

                // 3. VARIAÇÃO DE TEXTURA (Random Sprite)
                // Vamos gerar um número entre 0 e 2 (ex: 3 variações de soco)
                // Passaremos esse número como 'ai0' para o projétil saber qual desenho usar
                int textureStyle = Main.rand.Next(1); 

               

                Projectile.NewProjectile(
                    source, 
                    perturbedPosition, // Usa a nova posição deslocada
                    perturbedSpeed, 
                    type, 
                    damage, 
                    knockback, 
                    player.whoAmI, 
                    textureStyle // Passa o estilo no ai[0]
                );

                if (player.ownedProjectileCounts[ModContent.ProjectileType<PunchAnimProj>()] < 1)
                
    {
        Projectile.NewProjectile(
            source,
            player.Center, // Grudado no player
            velocity,      // Direção do mouse (para rotação)
            ModContent.ProjectileType<PunchAnimProj>(), // O projétil visual
            0,             // DANO ZERO
            0,             // SEM KNOCKBACK
            player.whoAmI

            
        );

                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/punchbarrage") 
        { 
            Volume = 0.50f,
            MaxInstances = 1, // Permite apenas 1 cópia desse som tocando ao mesmo tempo
            SoundLimitBehavior = SoundLimitBehavior.IgnoreNew // Ignora novos pedidos para tocar enquanto o atual não terminar
        }, player.position);
    }
            

            return false;

            
        }

        // --- SISTEMA DE RESTRIÇÃO ---
        public override bool CanUseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();

            // Verifica se é usuário do OFA
            bool isPunchUser = modPlayer.SelectedQuirk == QuirkType.OneForAll9th || 
                               modPlayer.SelectedQuirk == QuirkType.OneForAll8th || 
                               modPlayer.SelectedQuirk == QuirkType.Gearshift || 
                               modPlayer.SelectedQuirk == QuirkType.Overclock;

            return isPunchUser; 
        }
        

        // --- SISTEMA DE EVOLUÇÃO ---
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            var modPlayer = player.GetModPlayer<TransformationPlayer>();
            
            // One For All escala muito com evolução
            if (modPlayer.CurrentStage >= QuirkStage.Intermediate)
            {
                damage += 0.5f; // +50%
            
            if (modPlayer.CurrentStage >= QuirkStage.Final)
            {
                damage += 1.0f; // +100% (Dano total triplicado base)
            }
        }
        
    }
}
}

