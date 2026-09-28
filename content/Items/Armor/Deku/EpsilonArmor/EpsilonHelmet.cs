using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Deku.DeltaArmor;

namespace MyHeroMod.content.Items.Armor.Deku.EpsilonArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class EpsilonHelmet : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 20; 
        }
        public override void UpdateEquip(Player player)
        {

            player.GetDamage(DamageClass.Melee) += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<EpsilonBreastplate>() && legs.type == ModContent.ItemType<EpsilonLeggings>();
        }


        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases defense by 5 and melee speed by 20%";
            player.statDefense += 2;
            player.GetAttackSpeed(DamageClass.Melee) += 0.20f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DeltaHelmet>(), 1)
                .AddIngredient(ItemID.BeetleHusk, 10)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}