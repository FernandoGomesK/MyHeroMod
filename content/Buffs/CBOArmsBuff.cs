using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow;
using Terraria.ID;

namespace MyHeroMod.content.Buffs
{
    public class CBOArmsBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var darkShadow = player.GetModPlayer<DarkShadowPlayer>();



            darkShadow.isCBOArmsOn = true;
            
                
            
        }
    }}