using Terraria;
using Terraria.ModLoader;


namespace MyHeroMod.content.System
{
    public interface IHeroDodgeModifier
    {
       
        bool TryDodge(Player.HurtInfo info);
    }
}