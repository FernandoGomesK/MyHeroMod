using Terraria.ModLoader;
using Terraria;

using MyHeroMod.content.Quirks.SlideAndGlide;
using MyHeroMod.content.Quirks.Engine;

namespace MyHeroMod.content.Buffs 
{
    public class ReciproBuff : ModBuff
    {
        public override string Texture => "MyHeroMod/Assets/BuffImage/EngineBuff";
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; 
            Main.buffNoTimeDisplay[Type] = true; 
            Main.debuff[Type] = false; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            }
        }
    }