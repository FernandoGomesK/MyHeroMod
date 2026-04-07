using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.System
{
    public class RecipeSystem : ModSystem
    {
        public static RecipeGroup EvilMaterialGroup;
        public static RecipeGroup IronGreavesGroup;
        
        public static RecipeGroup CobaltBarGroup; 

        public override void AddRecipeGroups()
        {
            
            EvilMaterialGroup = new RecipeGroup(() => "Any Evil Material", ItemID.ShadowScale, ItemID.TissueSample);
            RecipeGroup.RegisterGroup("MyHeroMod:EvilMaterial", EvilMaterialGroup);

            IronGreavesGroup = new RecipeGroup(() => "Any Iron/Lead Greaves", ItemID.IronGreaves, ItemID.LeadGreaves);
            RecipeGroup.RegisterGroup("MyHeroMod:IronGreaves", IronGreavesGroup);

            
            CobaltBarGroup = new RecipeGroup(() => "Any Cobalt/Palladium Bar", ItemID.CobaltBar, ItemID.PalladiumBar);
            RecipeGroup.RegisterGroup("MyHeroMod:CobaltBar", CobaltBarGroup);
        }
    }
}