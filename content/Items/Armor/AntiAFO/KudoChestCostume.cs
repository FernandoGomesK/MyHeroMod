using MyHeroMod.content.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Armor.AntiAFO
{
    [AutoloadEquip(EquipType.Body)]
    public class KudoChestCostume : ModItem
    {
        public static int FemaleBodySlot;

        public static int CapeSlotID { get; private set; }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server) return;



        }





        public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            if (!male && FemaleBodySlot != -1)
            {
                equipSlot = FemaleBodySlot;
            }
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            //player.GetCritChance(DamageClass.Generic) += 10;
            //player.GetDamage(DamageClass.Generic) += 0.5f;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddRecipeGroup(RecipeSystem.EvilIronGroup, 15)
                //.AddIngredient(ItemID.Grenade, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }


    }
}