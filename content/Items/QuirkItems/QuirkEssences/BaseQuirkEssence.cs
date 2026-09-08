using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using MyHeroMod.content.System;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.OFA9th;

namespace MyHeroMod.content.Items.QuirkItems.QuirkEssences
{
    public abstract class BaseQuirkEssence : ModItem
    {
        
        public QuirkType EssenceQuirkType; 
        
        public string OriginPlayerName = "Unknown";
        public int timeUsed = 0;

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
            tag["EssenceQuirkType"] = (int)EssenceQuirkType;
            tag["OriginPlayerName"] = OriginPlayerName;
            tag["TimeUsed"] = timeUsed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("EssenceQuirkType"))
                EssenceQuirkType = (QuirkType)tag.GetInt("EssenceQuirkType");
            
            if (tag.ContainsKey("OriginPlayerName"))
                OriginPlayerName = tag.GetString("OriginPlayerName");
                
            if (tag.ContainsKey("TimeUsed"))
                timeUsed = tag.GetInt("TimeUsed");
        }

        public override bool CanUseItem(Player player)
        {
            return !UISystem.IsUiOpen();
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                
            
                if (transPlayer.ActiveQuirks.Contains(EssenceQuirkType))
                {
                    Main.NewText("You already possess this power!", Color.Red);
                    return false;
                }
                
            
                if (transPlayer.ActiveQuirks.Contains(QuirkType.AllForOne))
                {
                    var afoPlayer = player.GetModPlayer<AllForOnePlayer>();
                    afoPlayer.InternalQuirks.Add(EssenceQuirkType);

                    Main.NewText($"You Consumed {OriginPlayerName}'s power!", Color.LightGoldenrodYellow);
                }
             
                else
                {
                    transPlayer.ActiveQuirks.Add(EssenceQuirkType);
               
                    if (EssenceQuirkType == QuirkType.OneForAll9th || EssenceQuirkType == QuirkType.OneForAll8th)
                    {
                        Main.NewText($"You have inherited One For All from {OriginPlayerName}!", Color.LightGoldenrodYellow);
                        
                        
                        if (EssenceQuirkType == QuirkType.OneForAll9th)
                        {
                            var ofa9thPlayer = player.GetModPlayer<OneForAll9thPlayer>();
                            ofa9thPlayer.timeUsed += this.timeUsed; 
                        }
                    }
                    else
                    {
                        Main.NewText($"You have inherited a quirk from {OriginPlayerName}!", Color.LightGoldenrodYellow);
                    }
                }
            }
            return true;
        }    
    }
}
