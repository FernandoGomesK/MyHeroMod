using Microsoft.Xna.Framework;
using MyHeroMod.content.Items.Placeable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MyHeroMod.content.Tiles.CraftingStations
{
    public class NomuVat : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

           
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            
          
            TileObjectData.newTile.Height = 3; 
            
           
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(50, 200, 100), name); 

            DustType = DustID.Glass; 
        }

    }
}