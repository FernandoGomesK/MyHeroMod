using Microsoft.Xna.Framework;
using Terraria;

namespace MyHeroMod.content.System
{
   public interface IHeroDashModifier
    {
        
        void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor, ref int dustType);
    }
}