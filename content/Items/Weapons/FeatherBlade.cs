using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Weapons
{
    public class FeatherBlade : ModItem
    {
        public override void SetDefaults()
        {
            
            Item.damage = 50;                   
            Item.DamageType = DamageClass.Ranged; 
            Item.knockBack = 6f;                 
            Item.crit = 4;                        

            
            Item.width = 40;                     
            Item.height = 40;                     

            
            Item.useTime = 20;                   
            Item.useAnimation = 20;             
            Item.useStyle = ItemUseStyleID.Swing; 
            Item.autoReuse = true;                

            
            Item.UseSound = SoundID.Item1;        

            
         
               
        }

        public override void AddRecipes()
        {
            //CreateRecipe()
            //    .AddIngredient(ItemID.Wood, 10)
            //    .AddTile(TileID.WorkBenches)
            //    .Register();
        }
    }
}