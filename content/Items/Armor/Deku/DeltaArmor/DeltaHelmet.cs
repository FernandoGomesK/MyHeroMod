
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Armor.Deku.DeltaArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class DeltaHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                int equipSlotDown = EquipLoader.GetEquipSlot(Mod, "DeltaHelmet_Head_Down", EquipType.Head);

                if (equipSlotDown > 0)
                {
                    ArmorIDs.Head.Sets.DrawFullHair[equipSlotDown] = true;
                }
            }
        }

        public override void EquipFrameEffects(Player player, EquipType type)
        {
            var deltaManager = player.GetModPlayer<DeltaDekuManager>();

            if (!deltaManager.isCowlUp)
            {
                int equipSlotDown = EquipLoader.GetEquipSlot(Mod, "DeltaHelmet_Head_Down", EquipType.Head);
                player.head = equipSlotDown;
            }
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 15; 
        }
        public override void UpdateEquip(Player player)
        {

            player.GetDamage(DamageClass.Melee) += 0.08f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DeltaBreastplate>() && legs.type == ModContent.ItemType<DeltaLeggings>();
        }


        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases defense by 3 and melee speed by 15%";
            player.statDefense += 2;
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DeltaHelmet>(), 1)
                .AddIngredient(ItemID.ChlorophyteBar, 15) 
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}