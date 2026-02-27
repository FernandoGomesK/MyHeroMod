// using MyHeroMod.content.Projectiles;
// using MyHeroMod.content.System; // Para acessar SkillData/QuirkType
// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using MyHeroMod.content; // Para TransformationPlayer

// namespace MyHeroMod.content.Items.Weapons
// {
//     public class BlueQuirkAttack : ModItem
//     {
        
//         public override void SetDefaults()

        
//         {
//             // Nome e Tooltip são definidos no arquivo .hjson (Localization)
//             Item.damage = 8; // Dano inicial fraco
//             Item.DamageType = DamageClass.Magic; // Tipo de dano
//             Item.width = 28;
//             Item.height = 30;
//             Item.useTime = 6; // Muito rápido (lança-chamas)
//             Item.useAnimation = 6;
//             Item.useStyle = ItemUseStyleID.Shoot;
//             Item.noMelee = true; // Não bate com o item, só atira
//             Item.knockBack = 0.5f;
//             Item.value = 0; // Sem valor de venda (é uma skill)
//             Item.rare = ItemRarityID.White;
//             Item.autoReuse = true; // Segurar o clique atira contínuo
//             Item.shoot = ModContent.ProjectileType<BlueFireProj>();
//             Item.shootSpeed = 6f; // Velocidade do fogo
//             Item.useTurn = true; // Pode virar enquanto atira

//             Item.noUseGraphic = true; // Não mostra o item ao usar
//         }

//         // --- SISTEMA DE RESTRIÇÃO ---
//         // Impede o uso se não tiver a Quirk certa
//         public override bool CanUseItem(Player player)
//         {
//             var modPlayer = player.GetModPlayer<TransformationPlayer>();

//             // Lista de Quirks que podem usar esse ataque
//             bool DabiUser = modPlayer.SelectedQuirk == QuirkType.BlueFlames;

//             return DabiUser; 
//         }

//         // --- SISTEMA DE EVOLUÇÃO (Opcional) ---
//         // Aumenta o dano conforme o jogador evolui a Quirk
//         public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
//         {
//             var modPlayer = player.GetModPlayer<TransformationPlayer>();
            
//             // Exemplo: Se estiver no estágio 'Intermediário', +50% de dano
//             if (modPlayer.CurrentStage >= QuirkStage.Intermediate)
//             {
//                 damage += 0.5f; 
//             }
//             // Se estiver no estágio 'Dominado', +100% de dano
//             if (modPlayer.CurrentStage >= QuirkStage.Final)
//             {
//                 damage += 1.0f;
//             }
//         }
//         }
//         }
        