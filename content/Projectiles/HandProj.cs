using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using MyHeroMod.content.Quirks.AllForOne;
using MyHeroMod.content.System;

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

            
            if (globalNPC.HasQuirk && globalNPC.AssignedQuirk != QuirkType.Quirkless)
            {
                
                if (!afoPlayer.HasInternalQuirk(globalNPC.AssignedQuirk))
                {
                    
                    afoPlayer.InternalQuirks.Add(globalNPC.AssignedQuirk);
                    
                    
                    globalNPC.HasQuirk = false;
                    globalNPC.AssignedQuirk = QuirkType.Quirkless;

                    
                    CombatText.NewText(target.getRect(), Color.DarkRed, "QUIRK STOLEN!");
                    SoundEngine.PlaySound(SoundID.Item74, target.position); // Som meio macabro

                    
                    var transPlayer = player.GetModPlayer<TransformationPlayer>();
                    transPlayer.UpdateUnlockedSkills(); 
                }
                else
                {
                    CombatText.NewText(target.getRect(), Color.Gray, "Already Stolen!");
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

            if (transPlayer.SelectedQuirk == QuirkType.AllForOne)
            {
                if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Wraith);
            }
            }
            
            

        }

        
    }

    
}