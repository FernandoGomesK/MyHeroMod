using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Terraria.DataStructures;


namespace MyHeroMod.content.Quirks.SlideAndGlide
{
    public partial class SlideAndGlidePlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (greenLegsTimer > 0)
                greenLegsTimer--;

            if (Player.HasBuff(ModContent.BuffType<SlideAndGlideBuff>()))
            {
                Player.armorEffectDrawShadow = true;

                if (IsSliding)
                {
                    bool goingRight = Player.velocity.X > 0f;
                    float xOffset = goingRight ? -8f : (Player.width / 2) + 6f;

                    if (Main.rand.NextBool(2))
                    {
                        int d = Dust.NewDust(Player.position + new Vector2(xOffset, Player.height - 6), Player.width / 2, 10, DustID.IceTorch, 0, 2f, 100, default, 0.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].velocity *= 0.5f;
                    }

                    if (Main.rand.NextBool(2))
                    {
                        int d = Dust.NewDust(Player.position + new Vector2(xOffset, Player.height - 6), Player.width / 2, 10, DustID.SteampunkSteam, 0, 2f, 100, default, 1.5f);
                        Main.dust[d].noGravity = true;
                    }
                }
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            Player p = drawInfo.drawPlayer;

            if (!IsSliding || p.whoAmI != Main.myPlayer)
                return;

            p.headPosition = new Vector2(0f, -4f);

            float angle = (Main.MouseWorld - p.Center).ToRotation();
            p.headRotation = p.direction == 1
                ? angle - p.fullRotation
                : angle - p.fullRotation + MathHelper.Pi;
        }

    }
}
 