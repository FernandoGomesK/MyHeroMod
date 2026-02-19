using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Smokescreen;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace MyHeroMod.content.Buffs // Ajuste o namespace se necessário
{
    public class SmokescreenBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/SmokescreenBuff";
        
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SmokescreenPlayer>().isSmokescreenActive = true;
            Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0f, 0f, 100, Color.MediumPurple, 6.0f);
        }
    }
}