using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    [AutoloadEquip(EquipType.Front)]
    public class CombatVestAlpha : ModItem
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
            
            var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
            var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
            
            hchhPlayer.IsCombatVestAlphaOn = true;
            hellPlayer.IsCombatVestAlphaOn = true;
            
            
            
        }
        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.IronBar, 20)       
            .AddTile(TileID.Anvils)
            .Register();
            }
        }
        }