using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Items.Armor.Endeavor.FirstCostume
{
    [AutoloadEquip(EquipType.Legs)]
    public class FirstELeggings : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5; 
        }
        public override void UpdateEquip(Player player)
        {
        
            player.statLifeMax2 += 20;
            Lighting.AddLight(player.Center, Color.OrangeRed.ToVector3() * 0.4f);

            if (Main.rand.NextBool(10))
            {
                int fire = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                Main.dust[fire].noGravity = true;
                Main.dust[fire].velocity *= 2f;
                Main.dust[fire].velocity += player.velocity * 0.5f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .AddRecipeGroup(RecipeSystem.IronAndLeadGroup, 10)
                .AddIngredient(ItemID.Torch, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}