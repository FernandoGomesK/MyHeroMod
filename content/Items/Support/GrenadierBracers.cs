// using MyHeroMod.content.Quirks.Explosion;
// using MyHeroMod.content.Quirks.HalfColdHalfHot;
// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;


// namespace MyHeroMod.content.Items.Support
// {
//     public class GrenadierBracers : ModItem
//     {
//         public override void SetDefaults()
//         {
//             Item.width = 22;
//             Item.height = 18;
//             Item.accessory = true;
//             Item.rare = ItemRarityID.Green;
//             Item.value = Item.sellPrice(gold: 1);
            
//         }

//         public override void UpdateAccessory(Player player, bool hideVisual)
//         {
//             // Pega o Player do Todoroki e liga o colete
//             var quirkPlayer = player.GetModPlayer<ExplosionPlayer>();
//             quirkPlayer.IsGrenadierBracersOn = true;
            
//             // Opcional: Bônus genéricos
            
//         }
        
//         }
//         }