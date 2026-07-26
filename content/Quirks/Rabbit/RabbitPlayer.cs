using Terraria;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Rabbit
{
    public class RabbitPlayer : ModPlayer
    {
        public bool isIronSolesOn = false;

        public override void ResetEffects()
        {
            isIronSolesOn = false;
        }


        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();
            

            if (mainPlayer.HasActiveQuirk(QuirkType.Rabbit))
            {
                Player.moveSpeed += 1.5f;
                Player.jumpSpeedBoost += 1.5f;
                Player.noFallDmg = true;
            }

        }


        
    }
}