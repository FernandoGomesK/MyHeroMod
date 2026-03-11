using MyHeroMod.content.Quirks.HalfColdHalfHot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.Front)]
    public class CombatVestBeta : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);

            Item.frontSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Front);
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Pega o Player do Todoroki e liga o colete
            var quirkPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            quirkPlayer.IsCombatVestBetaOn = true;
            
            // Opcional: Bônus genéricos
            
        }
        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.TissueSample, 10) 
            .AddIngredient(ItemID.ShadowScale, 10) 
            .AddTile(TileID.Anvils)
            .Register();
            }
        }
        }