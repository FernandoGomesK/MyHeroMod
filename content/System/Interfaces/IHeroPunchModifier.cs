using Microsoft.Xna.Framework;
using Terraria;

namespace MyHeroMod.content.System
{
   public interface IHeroPunchModifier
    {
        void ModifyPunch(ref float projSpeed, ref int baseDamage, ref bool isSuperPunch, ref int numberOfPunches);
    }
}