using MyHeroMod.content.Items.Armor.Deku.BetaArmor;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Armor.Deku.GammaArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class GammaHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                int equipSlotDown = EquipLoader.GetEquipSlot(Mod, "MyHeroMod/content/Items/Armor/Deku/GammaArmor/GammaHelmet_Head_Down", EquipType.Head);

                if (equipSlotDown > 0)
                {
                    ArmorIDs.Head.Sets.DrawFullHair[equipSlotDown] = true;
                }
            }
        }

        public override void EquipFrameEffects(Player player, EquipType type)
        {
            var GammaManager = player.GetModPlayer<GammaDekuManager>();

            if (!GammaManager.isCowlUp)
            {
                int equipSlotDown = EquipLoader.GetEquipSlot(Mod, "MyHeroMod/content/Items/Armor/Deku/GammaArmor/GammaHelmet_Head_Down", EquipType.Head);
                player.head = equipSlotDown;
            }
        }


        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 8; 
        }
        public override void UpdateEquip(Player player)
        {

            player.GetDamage(DamageClass.Melee) += 0.04f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GammaBreastplate>() && legs.type == ModContent.ItemType<GammaLeggings>();
        }


        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases defense by 2 and melee speed by 15%";
            player.statDefense += 2;
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BetaHelmet>(), 1) 
                .AddRecipeGroup(RecipeSystem.AdamantineGroup, 12)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}