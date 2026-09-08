using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass;
using MyHeroMod.content.System;


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
            

            foreach (var modPlayer in player.ModPlayers)
            {
               
                if (modPlayer is BaseIceAndFirePlayer fireIceUser)
                {
                    fireIceUser.isCombatVestAlphaOn = true;
                }
            }
        }
             
        public override void AddRecipes()
        {   
            CreateRecipe()
            .AddRecipeGroup(RecipeSystem.IronAndLeadGroup, 20) 
            .AddTile(TileID.Anvils)
            .Register();
            }
        }
}