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

            
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            
            
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);

            
            LocalizedText name = CreateMapEntryName();
            
            AddMapEntry(new Color(100, 150, 200), name); 

            
            DustType = DustID.Iron;
        }
        
        // This makes sure the station actually drops its item when broken
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<NomuVatItem>());
        }
    }
}