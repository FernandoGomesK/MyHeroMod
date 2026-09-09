using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.System;
using static MyHeroMod.MyHeroMod;

namespace MyHeroMod.content.Projectiles
{
    public class StealPlayerHandProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj";
        private static readonly QuirkType[] NonStealableQuirks =
        {
            QuirkType.Quirkless,
            QuirkType.OneForAll9th,
            QuirkType.AllForOne
        };

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player attacker = Main.player[Projectile.owner];

        
            if (Projectile.owner != Main.myPlayer) return;
            if (target.whoAmI == attacker.whoAmI) return;

            var afoPlayer = attacker.GetModPlayer<AllForOnePlayer>();
            var targetTrans = target.GetModPlayer<TransformationPlayer>();

            if (afoPlayer.CurrentQuirkCount >= afoPlayer.maxQuirks)
            {
                CombatText.NewText(target.getRect(), Color.Orange, "Capacity Full!");
                return;
            }

            var stealable = targetTrans.ActiveQuirks
                .Where(q => !NonStealableQuirks.Contains(q))
                .ToList();

            if (stealable.Count == 0)
            {
                CombatText.NewText(target.getRect(), Color.Gray, "Nothing to steal!");
                return;
            }

            QuirkType stolenQuirk = stealable[Main.rand.Next(stealable.Count)];

            if (afoPlayer.TryStealQuirk(stolenQuirk))
            {
                CombatText.NewText(target.getRect(), Color.DarkRed, "QUIRK STOLEN!");
                SoundEngine.PlaySound(SoundID.Item74, target.position);

                targetTrans.ActiveQuirks.Remove(stolenQuirk);

            
                var targetOfa9th = target.GetModPlayer<OneForAll9thPlayer>();
                targetOfa9th.MarkQuirkLost(stolenQuirk); 

                targetTrans.UpdateUnlockedSkills();
                targetOfa9th.UnlockQuirks();

                var attackerTrans = attacker.GetModPlayer<TransformationPlayer>();
                attackerTrans.UpdateUnlockedSkills();

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket packet = Mod.GetPacket();
                    packet.Write((byte)MessageType.StealPlayerQuirk); 
                    packet.Write((byte)Main.myPlayer); 
                    packet.Write(target.whoAmI);
                    packet.Write((int)stolenQuirk);
                    packet.Send();
                }
              
            }
            else
            {
                CombatText.NewText(target.getRect(), Color.Gray, "Already Stolen!");
            }
        }
    }
}