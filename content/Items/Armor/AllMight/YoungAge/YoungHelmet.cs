using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs.OFA8;
using MyHeroMod.content.Quirks.OFA8th;
// AJUSTE: namespace real do OneForAll8thPlayer
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Items.Armor.AllMight.YoungAge
{

    [AutoloadEquip(EquipType.Head)]
    public class YoungHelmet : ModItem
    {
        public const string DownName = "YoungHelmetDown";

        public static int NormalSlot { get; private set; }
        public static int DownSlot { get; private set; }

        public override void Load()
        {
            EquipLoader.AddEquipTexture(
                Mod,
                Texture + "Down_Head",
                EquipType.Head,
                name: DownName,
                equipTexture: new YoungHelmetDownTexture()
            );
        }

        public override void SetStaticDefaults()
        {
            NormalSlot = Item.headSlot;
            DownSlot = EquipLoader.GetEquipSlot(Mod, DownName, EquipType.Head);

            foreach (int slot in new[] { NormalSlot, DownSlot })
            {
                ArmorIDs.Head.Sets.DrawHead[slot] = true;
                ArmorIDs.Head.Sets.DrawHatHair[slot] = false;
                ArmorIDs.Head.Sets.DrawFullHair[slot] = false;
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddRecipeGroup(RecipeSystem.CobaltBarGroup, 15)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            ApplyHairTint(drawPlayer, ref color);
        }

        public static void ApplyHairTint(Player drawPlayer, ref Color color)
        {
            color = new Color(color.ToVector4() * drawPlayer.hairColor.ToVector4());
        }
    }

    public class YoungHelmetDownTexture : EquipTexture
    {
        public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            YoungHelmet.ApplyHairTint(drawPlayer, ref color);
        }
    }

    public class YoungHelmetPlayer : ModPlayer
    {
        public override void FrameEffects()
        {
            if (YoungHelmet.NormalSlot <= 0 || Player.head != YoungHelmet.NormalSlot)
                return;

            if (ShouldUseDownSprite())
                Player.head = YoungHelmet.DownSlot;
        }

        private bool ShouldUseDownSprite()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (!transPlayer.HasActiveQuirk(QuirkType.OneForAll8th))
                return false;

            return !Player.HasBuff<StockPileBuff>();
        }
    }
}