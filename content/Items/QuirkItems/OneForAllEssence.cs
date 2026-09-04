using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using MyHeroMod.content.System;
using Microsoft.Xna.Framework; 

namespace MyHeroMod.content.Items.QuirkItems
{
    public abstract class OneForAllEssence : ModItem
    {
        public QuirkType ofaType;
        public string OriginPlayerName = "Unknown";

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.maxStack = 1; 
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.UseSound = SoundID.Item3;
            Item.rare = ItemRarityID.Blue;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["ofaType"] = (int)ofaType;
            tag["OriginPlayerName"] = OriginPlayerName;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("ofaType"))
                ofaType = (QuirkType)tag.GetInt("ofaType");
            
            if (tag.ContainsKey("OriginPlayerName"))
                OriginPlayerName = tag.GetString("OriginPlayerName");
        }

        public override bool CanUseItem(Player player)
        {
            if (UISystem.IsUiOpen())
            {
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                
            
                if (transPlayer.ActiveQuirks.Contains(ofaType))
                {
                    Main.NewText("You already possess this power!", Color.Red);
                    return false;
                }

                transPlayer.ActiveQuirks.Add(ofaType);
               
                Main.NewText($"You have inherited One For All from {OriginPlayerName}!", Color.LightGoldenrodYellow);
            }
            return true;
        }    
    }
}