using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.Quirks.OFA9th;
using MyHeroMod.content.System;
using static MyHeroMod.MyHeroMod;
using System;

namespace MyHeroMod.content.Projectiles
{
    public class StealPlayerHandProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj";

        public override void SetDefaults()
        {
            Projectile.width = 32; 
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 120; 
            Projectile.alpha = 255; 
        }

        private static readonly QuirkType[] NonStealableQuirks =
        {
            QuirkType.Quirkless,
            QuirkType.OneForAll9th,
            QuirkType.AllForOne
        };

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }       

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (transPlayer.HasActiveQuirk(QuirkType.AllForOne))
            {
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Wraith);
                }
            }
            
        
            if (Projectile.owner == Main.myPlayer) 
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player target = Main.player[i];
                    
                  
                    if (target.active && !target.dead && i != Projectile.owner)
                    {
                        
                        if (Projectile.Hitbox.Intersects(target.Hitbox))
                        {
                            ExecuteSteal(target);
                            Projectile.Kill(); 
                            break; 
                        }
                    }
                }
            }
        }

       
        private void ExecuteSteal(Player target)
        {
            try
            {
                Player attacker = Main.player[Projectile.owner];

                var afoPlayer = attacker.GetModPlayer<AllForOnePlayer>();
                var targetTrans = target.GetModPlayer<TransformationPlayer>();

                // 1. Prevent silent crashes by checking for null lists
                if (targetTrans.ActiveQuirks == null)
                {
                    Main.NewText($"[Debug] Target's ActiveQuirks is null! Sync TransformationPlayer.", Color.Red);
                    return;
                }

                if (afoPlayer.CurrentQuirkCount >= afoPlayer.maxQuirks)
                {
                    CombatText.NewText(target.Hitbox, Color.Orange, "Capacity Full!");
                    return;
                }

                var stealable = targetTrans.ActiveQuirks
                    .Where(q => !NonStealableQuirks.Contains(q))
                    .ToList();

                if (stealable.Count == 0)
                {
                    CombatText.NewText(target.Hitbox, Color.Gray, "Nothing to steal!");
                    return;
                }

                QuirkType stolenQuirk = stealable[Main.rand.Next(stealable.Count)];

                if (afoPlayer.TryStealQuirk(stolenQuirk))
                {
                    CombatText.NewText(target.Hitbox, Color.DarkRed, "QUIRK STOLEN!");
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
                        
                        // CRITICAL FIX: Cast to (byte) so it matches your HandlePacket reader!
                        packet.Write((byte)target.whoAmI); 
                        
                        packet.Write((int)stolenQuirk);
                        packet.Send();
                    }
                }
                else
                {
                    CombatText.NewText(target.Hitbox, Color.Gray, "Already Stolen!");
                }
            }
            catch (Exception e)
            {
                Main.NewText($"Error in ExecuteSteal: {e.Message}", Color.Red);
            }
        }
    }
}