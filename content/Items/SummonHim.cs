using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using MyHeroMod.content;
using Terraria.ModLoader.Utilities;
using MyHeroMod.content.Npcs.Bosses;
using MyHeroMod.content.Npcs.Bosses.AllForOne;
using Terraria.Audio;

namespace MyHeroMod.content.Items
{
    public class SummonHim : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;

        }

        public override void SetDefaults(){
            Item.width = 32;
            Item.height = 32;
            Item.value = 100;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation =30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override void AddRecipes()
{
    CreateRecipe()
        .AddIngredient(ModContent.ItemType<Items.QuirkGene>(), 32) 
        
        .AddTile(TileID.WorkBenches)         
        .Register();                         
}


        public override bool CanUseItem(Player player)
        {
            return!
            NPC.AnyNPCs(ModContent.NPCType<AllForOneBoss>());
        }

        public override bool? UseItem(Player player)
        {
            

            if (Main.myPlayer == player.whoAmI)
            {
                
                SoundEngine.PlaySound(SoundID.Roar, player.position);
                int type = ModContent.NPCType<AllForOneBoss>();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                    
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }
            return true;
        }    
        }
        }