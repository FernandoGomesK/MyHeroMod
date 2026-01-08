using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Items.Armor.Endeavor.FirstCostume
{
    [AutoloadEquip(EquipType.Head)]
    public class FirstEHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
        
            Item.defense = 5; // Defesa do capacete
        }
        public override void UpdateEquip(Player player)
        {
            // Aumenta a vida máxima em 20 quando equipado
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
            .AddIngredient(ItemID.DirtBlock, 1)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}