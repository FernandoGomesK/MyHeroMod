using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.Items.Armor.Deku.AlphaArmor;

namespace MyHeroMod.content.Items.Armor.Deku.BetaArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class BetaHelmet : ModItem
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
            Item.defense = 5; 
        }
        public override void UpdateEquip(Player player)
        {

            player.GetDamage(DamageClass.Melee) += 0.04f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BetaBreastplate>() && legs.type == ModContent.ItemType<BetaLeggings>();
        }


        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases defense by 2 and melee speed by 5%";
            player.statDefense += 2;
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AlphaHelmet>(), 1)
                .AddIngredient(ItemID.HellstoneBar, 10) 
                .AddIngredient(ItemID.Bone, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}