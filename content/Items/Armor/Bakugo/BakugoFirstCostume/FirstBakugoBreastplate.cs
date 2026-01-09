using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace MyHeroMod.content.Items.Armor.Bakugo.BakugoFirstCostume
{
    [AutoloadEquip(EquipType.Body)]
    public class FirstBakugoBreastplate : ModItem
    {
        public static int FemaleBodySlot;

        public static int CapeSlotID { get; private set; }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server) return;

            
            
        }

                


    
    public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
        }
    public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            if (!male && FemaleBodySlot != -1)
            {
                equipSlot = FemaleBodySlot;
            }
        }
        public override void SetDefaults()
        {
            Item.width = 18; // Tamanho do item no chão/inventário
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 20; 
        }
        public override void UpdateEquip(Player player)
        {
            // Seus buffs aqui (ex: +Dano, +Velocidade)
            // player.GetDamage(DamageClass.Generic) += 0.10f; 
            Lighting.AddLight(player.Center, Color.OrangeRed.ToVector3() * 0.4f);

            if (Main.rand.NextBool(10))
            {
                int fire = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                Main.dust[fire].noGravity = true;
                Main.dust[fire].velocity *= 2f;
                Main.dust[fire].velocity += player.velocity * 0.5f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 20) // Exemplo
                .AddTile(TileID.Anvils)
                .Register();
        }
    

}
}