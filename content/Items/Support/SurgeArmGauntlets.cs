using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    public class SurgeArmGauntlets : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
           
            
            foreach (var modPlayer in player.ModPlayers)
            {
                
                if (modPlayer is BaseIceAndFirePlayer fireIceUser)
                {
      
                    fireIceUser.isSurgeArmGauntletsOn = true;
                    break; 
                }
            }
            
        }
        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddIngredient(ItemID.HellstoneBar, 10) 
            .AddIngredient(ItemID.Bone, 5)         
            .AddTile(TileID.Anvils)
            .Register();
            }
        
        }
        }