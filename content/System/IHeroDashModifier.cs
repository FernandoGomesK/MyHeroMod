using Microsoft.Xna.Framework;

namespace MyHeroMod.content.System
{
    public interface IHeroDashModifier
{
    void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash, ref Color explosionColor);
}
}