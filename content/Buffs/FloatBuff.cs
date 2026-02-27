using Terraria.ModLoader;
using MyHeroMod.content.Quirks.Float;
using Terraria;

namespace MyHeroMod.content.Buffs // Ajuste o namespace se necessário
{
    public class FloatBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/FloatBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FloatPlayer>().isFloatActive = true; 


            if (player.GetModPlayer<FloatPlayer>().isFloatActive && !player.mount.Active && player.velocity.Y != 0 )
            {
                    
                if (player.controlJump) 
                {
                    player.velocity.Y = -0.5f; 
                    player.fallStart = (int)(player.position.Y / 16f); 
                }
                
                else if (player.velocity.Y > 0)
                {
                    player.velocity.Y *= 0.25f; 
                }
            }
        }
    }
}