using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace MyHeroMod.content.Quirks.OFA8th.Projectiles.DetroitSmash
{
    public class PrimeDetroitSmashProj : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 60;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.scale = 2.0f;
        }
        
        public override bool? CanHitNPC(NPC target)
        {
            // O ai[1] contém o ID do NPC que tomou o soco inicial.
            // Se o alvo atual for o mesmo ID guardado, retornamos false (não acerta).
            // (Verificamos se ai[1] > 0 porque 0 pode ser um NPC válido, mas geralmente passamos -1 se ninguém foi acertado, 
            // porém no código anterior inicializamos com -1, mas ai[] são floats e padrão é 0. 
            // O ideal é assumir que se targetID for passado, deve ser verificado).
            
            int ignoredNPC = (int)Projectile.ai[1];

            // Se o ID bater com o alvo, IGNORA.
            if (target.whoAmI == ignoredNPC)
            {
                return false;
            }

            return null; // Retorna null para usar a lógica padrão de colisão para os outros NPCs
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, Color.White, 4.0f);
            }
        }
        public override void AI()
        {

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.scale < 3.0f)
            {
                Projectile.scale += 0.05f;
                Vector2 oldCenter = Projectile.Center;
                Projectile.width = (int)(50 * Projectile.scale);
                Projectile.height = (int)(50 * Projectile.scale);
                Projectile.Center = oldCenter;
            }

            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 3.5f);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // Pega a textura do projétil
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            // Calcula o CENTRO da imagem (Largura / 2, Altura / 2)
            Vector2 origin = texture.Size() / 2f;

            // Desenha a imagem centralizada na posição do projétil
            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition, // Posição na tela
                null,
                lightColor,
                Projectile.rotation, // Rotação correta
                origin,              // Pivô no centro!
                Projectile.scale,
                SpriteEffects.None,
                0
            );

            return false;
        }
    }
}


// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.ModLoader;
// using Terraria.ID;


// namespace MyHeroMod.content.Quirks.OFA8th.Projectiles.DetroitSmash
// {
//     public class PrimeDetroitSmashProj : ModProjectile
//     {
        
//         public override void SetDefaults()
//         {
//             Projectile.width = 80;
//             Projectile.height = 80;
//             Projectile.aiStyle = 0;
//             Projectile.friendly = true;
//             Projectile.hostile = false;
//             Projectile.DamageType = DamageClass.Generic;
//             Projectile.penetrate = 2;
//             Projectile.timeLeft = 60;
//             Projectile.light = 0.5f;
//             Projectile.ignoreWater = true;
//             Projectile.tileCollide = true;
//             Projectile.alpha = 0;
//         }
//         public override void OnKill(int timeLeft)
//         {
//             for (int i = 0; i < 10; i++)
//             {
//                 Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 2.0f);
//             }
//         }
//         public override void AI()
//         {
//             Projectile.rotation = Projectile.velocity.ToRotation();
//             if (Main.rand.NextBool(2))
//             {
//                 Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1.5f);
//             }
//         }
//     }
// }