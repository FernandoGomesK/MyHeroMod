using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MyHeroMod.content.System
{
    public static class AfterimagePalettes
    {
        public static readonly Color[] Rainbow = { Color.Lime, Color.DeepSkyBlue, Color.Yellow, Color.Red, Color.Magenta, Color.Cyan };
        public static readonly Color[] Blue = { Color.RoyalBlue };
    }

    public class AfterimagePlayer : ModPlayer
    {
        public const int TrailLength = 20;

        public Vector2[] oldPos = new Vector2[TrailLength];
        public bool Active;
        public Color[] Palette = AfterimagePalettes.Rainbow;
        public int TicksPerColor = 30;
        public float Opacity = 0.45f;

        /// <summary>Chamado por qualquer quirk de super velocidade, todo tick em que estiver ativo.</summary>
        public void Request(Color[] palette, int ticksPerColor = 30, float opacity = 0.45f)
        {
            Active = true;
            Palette = palette;
            TicksPerColor = Math.Max(1, ticksPerColor);
            Opacity = opacity;
        }

        public override void ResetEffects() => Active = false;

        public override void PostUpdate()
        {
            if (!Active)
            {
                Array.Clear(oldPos, 0, oldPos.Length);
                return;
            }

            for (int i = oldPos.Length - 1; i > 0; i--)
                oldPos[i] = oldPos[i - 1];
            oldPos[0] = Player.position;
        }
    }

    public class SpeedAfterimageLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.HeldItem);

        private static Color GetCycleColor(Color[] palette, int ticksPerColor, int tick)
        {
            if (palette.Length == 1)
                return palette[0];

            int step = tick / ticksPerColor;
            float t = (tick % ticksPerColor) / (float)ticksPerColor;
            return Color.Lerp(palette[step % palette.Length], palette[(step + 1) % palette.Length], t);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            var trail = drawPlayer.GetModPlayer<AfterimagePlayer>();

            if (!trail.Active || drawInfo.DrawDataCache.Count == 0)
                return;

            List<DrawData> currentData = new(drawInfo.DrawDataCache);
            int insertIndex = 0;
            int now = (int)Main.GameUpdateCount;

            for (int i = trail.oldPos.Length - 1; i >= 0; i -= 2)
            {
                Vector2 oldPos = trail.oldPos[i];
                if (oldPos == Vector2.Zero || oldPos == drawPlayer.position)
                    continue;

                Vector2 offset = oldPos - drawPlayer.position;
                float alpha = (1f - (float)i / trail.oldPos.Length) * trail.Opacity;
                Color tint = GetCycleColor(trail.Palette, trail.TicksPerColor, now - i) * alpha;

                foreach (var drawData in currentData)
                {
                    DrawData trailData = drawData;
                    trailData.position += offset;
                    trailData.color = tint;
                    drawInfo.DrawDataCache.Insert(insertIndex++, trailData);
                }
            }
        }
    }
}