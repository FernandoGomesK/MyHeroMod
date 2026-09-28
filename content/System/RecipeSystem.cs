using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.System
{
    public class RecipeSystem : ModSystem
    {
        public static RecipeGroup EvilMaterialGroup { get; set; }
        public static RecipeGroup IronGreavesGroup { get; set; }

        public static RecipeGroup CobaltBarGroup { get; set; }

        public static RecipeGroup AdamantineGroup { get; set; }

        public static RecipeGroup EvilIronGroup { get; set; }

        public static RecipeGroup IronAndLeadGroup { get; set; }

        public override void AddRecipeGroups()
        {
            
            EvilMaterialGroup = new RecipeGroup(() => "Any Evil Material", ItemID.ShadowScale, ItemID.TissueSample);
            RecipeGroup.RegisterGroup("MyHeroMod:EvilMaterial", EvilMaterialGroup);

            IronGreavesGroup = new RecipeGroup(() => "Any Iron/Lead Greaves", ItemID.IronGreaves, ItemID.LeadGreaves);
            RecipeGroup.RegisterGroup("MyHeroMod:IronGreaves", IronGreavesGroup);

            AdamantineGroup = new RecipeGroup(() => "Any Adamantite/Titanium Bar", ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("MyHeroMod:AdamantiteBar", AdamantineGroup);

            EvilIronGroup = new RecipeGroup(() => "Any Evil Iron Bar", ItemID.CrimtaneBar, ItemID.DemoniteBar);
            RecipeGroup.RegisterGroup("MyHeroMod:EvilIronBar", EvilIronGroup);

            
            CobaltBarGroup = new RecipeGroup(() => "Any Cobalt/Palladium Bar", ItemID.CobaltBar, ItemID.PalladiumBar);
            RecipeGroup.RegisterGroup("MyHeroMod:CobaltBar", CobaltBarGroup);

            IronAndLeadGroup = new RecipeGroup(() =>"Any Iron/Lead Bar", ItemID.LeadBar, ItemID.IronBar);
            RecipeGroup.RegisterGroup("MyHero:Iron Bar", IronAndLeadGroup);
        }
    }
}