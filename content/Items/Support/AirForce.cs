using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.OFA9th;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace MyHeroMod.content.Items.Support
{
    public class AirForce : ModItem
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
            // Pega o Player do Todoroki e liga o colete
            var quirkPlayer = player.GetModPlayer<OneForAll9thPlayer>();
            quirkPlayer.isAirForceOn = true;
            
            // Opcional: Bônus genéricos
            
        }
        
        }
        }