    using Terraria;
    using Terraria.ModLoader;
    using Terraria.ID;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Terraria.Audio;
    using MyHeroMod.content.Quirks.AllForOne;
    using MyHeroMod.content.System;
    using static MyHeroMod.MyHeroMod;

namespace MyHeroMod.content.Projectiles
    {
        public class HandProj : ModProjectile
        {
            public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj";
            public override void SetDefaults()
            {
                Projectile.width = 32; 
                Projectile.height = 32;
                Projectile.friendly = true;
                Projectile.hostile = false;
                Projectile.tileCollide = true; 
                Projectile.penetrate = 1; 
                Projectile.timeLeft = 120; 
                Projectile.alpha = 255; 
                
            }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            var afoPlayer = player.GetModPlayer<AllForOnePlayer>();
            var globalNPC = target.GetGlobalNPC<QuirkGlobalNPC>();

            
            if (Projectile.owner == Main.myPlayer)
        {
            if (globalNPC.HasQuirk && globalNPC.AssignedQuirk != QuirkType.Quirkless)
            {
                if (afoPlayer.CurrentQuirkCount >= afoPlayer.maxQuirks) 
                {
                    CombatText.NewText(target.getRect(), Color.Orange, "Capacity Full!");
                    return;
                }
                
                if (afoPlayer.TryStealQuirk(globalNPC.AssignedQuirk))
                {
                   
                    CombatText.NewText(target.getRect(), Color.DarkRed, "QUIRK STOLEN!");
                    SoundEngine.PlaySound(SoundID.Item74, target.position);

                    var transPlayer = player.GetModPlayer<TransformationPlayer>();
                    transPlayer.UpdateUnlockedSkills(); 
                    
                
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket packet = Mod.GetPacket();
                        packet.Write((byte)MessageType.StealNPCQuirk);
                        packet.Write(target.whoAmI);
                        packet.Send();
                    }
                    
                    else 
                    {
                        globalNPC.HasQuirk = false;
                        globalNPC.AssignedQuirk = QuirkType.Quirkless;

                        
                        if (target.boss)
                        {
                            target.lifeMax = (int)(target.lifeMax / 1.5f);
                            target.damage = (int)(target.damage / 1.5f);
                        }
                        else
                        {
                            target.lifeMax = (int)(target.lifeMax / 4f);
                            target.damage = (int)(target.damage / 3f);
                        }

                        
                        if (target.life > target.lifeMax)
                        {
                            target.life = target.lifeMax;
                        }
                    }
                }
                else
                {
                    CombatText.NewText(target.getRect(), Color.Gray, "Already Stolen!");
                }
            }
        }
    }

            public override Color? GetAlpha(Color lightColor)
            {
                return Color.White;
            }       

            public override void OnKill(int timeLeft)
            {
                
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
                
                

            }

            
        }

        
    }