using Terraria;
using Terraria.ModLoader;


namespace MyHeroMod.content.System.Interfaces
{
    public interface IHeroDodgeModifier
    {
       
        bool TryDodge(Player.HurtInfo info);
    }
}