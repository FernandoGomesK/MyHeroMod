using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.Debuffs;

namespace MyHeroMod.content.Quirks.Overhaul.Projectiles.RangeHeal
{
    public class RangeHealProj : ModProjectile
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

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }       

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (transPlayer.HasActiveQuirk(QuirkType.Overhaul))
            {
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch);
                }
            }
            
            // --- SISTEMA DE CURA DE ALIADOS ---
            // Apenas o dono do projétil faz a verificação para não curar a dobrar no Multiplayer
            if (Projectile.owner == Main.myPlayer) 
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player target = Main.player[i];

                    // Verifica se o alvo está vivo, ativo, se não é o próprio dono, e se o projétil tocou nele!
                    if (target.active && !target.dead && target.whoAmI != Projectile.owner)
                    {
                        if (Projectile.Hitbox.Intersects(target.Hitbox))
                        {
                            int healAmount = 50; // Quantidade de cura

                            // Cura o jogador e impede que passe da vida máxima
                            target.statLife += healAmount;
                            if (target.statLife > target.statLifeMax2)
                            {
                                target.statLife = target.statLifeMax2;
                            }
                            
                            // Efeito visual do textinho verde a subir
                            target.HealEffect(healAmount);

                            // Opcional: Se quiser que o Overhaul também se cure a si mesmo ao curar os outros
                            // player.statLife += 10;
                            // player.HealEffect(10);

                            Projectile.Kill(); // Destrói a mão após curar
                            break;
                        }
                    }
                }
            }
        }

        // public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        // {
        //     // Placeholder do Decay, no futuro pode fazer um "OverhaulBuff" que dá Insta-Kill a mobs fracos!
        //     target.AddBuff(ModContent.BuffType<DecayBuff>(), 300);
        // }

        public override void OnKill(int timeLeft)
        {
            // Pode colocar algum pó ou som aqui quando o projétil se desfaz!
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);
            }
        }   
    }
}