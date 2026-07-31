using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyHeroMod.content.Npcs.Bosses.AllForOne.Projectiles
{
    public class RivetStabProj : ModProjectile
    {
        public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";

        public override void SetStaticDefaults()
        {
            
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 60; 
            
            
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; 
        }
        public override void SetDefaults()
        {
            Projectile.width = 12; 
            Projectile.height = 8; 
            Projectile.hostile = true; 
            Projectile.friendly = false;
            Projectile.penetrate = 1; 
            Projectile.tileCollide = true; 
            Projectile.timeLeft = 100; 
            Projectile.extraUpdates = 1; 
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ai[0]++; 

        
            Vector2 targetCenter = Vector2.Zero;
            bool hasTarget = false;

            
            if (Projectile.friendly)
            {
                float closestDistance = 1500f; 
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    
                    if (npc.active && !npc.friendly && npc.CanBeChasedBy())
                    {
                        float distance = Vector2.Distance(Projectile.Center, npc.Center);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            targetCenter = npc.Center;
                            hasTarget = true;
                        }
                    }
                }
            }
            
            else
            {
                Player targetPlayer = Main.player[Player.FindClosest(Projectile.Center, 1, 1)];
                if (targetPlayer.active && !targetPlayer.dead)
                {
                    targetCenter = targetPlayer.Center;
                    hasTarget = true;
                }
            }

            
            if (Projectile.ai[0] % 15 == 0 && hasTarget) 
            {
                Vector2 directionToTarget = targetCenter - Projectile.Center;
                float distanceToTarget = directionToTarget.Length();

                if (distanceToTarget > 100f) 
                {
                    directionToTarget.Normalize();
                    
                    float currentAngle = Projectile.velocity.ToRotation();
                    float targetAngle = directionToTarget.ToRotation();
                    float difference = MathHelper.WrapAngle(targetAngle - currentAngle);
                    float maxTurn = MathHelper.PiOver4; 
                    
                    if (difference > maxTurn) difference = maxTurn;
                    else if (difference < -maxTurn) difference = -maxTurn;

                    Projectile.velocity = Projectile.velocity.RotatedBy(difference);
                }
            }
        }

        public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
        {
            
            if (source is Terraria.DataStructures.EntitySource_Parent parent && parent.Entity is NPC npc)
            {
            
                if (npc.friendly)
                {
                    Projectile.hostile = false;
                    Projectile.friendly = true;
                    Projectile.tileCollide = false;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            
            Microsoft.Xna.Framework.Graphics.Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            
           
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);

            
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                
                
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

                
                Main.EntitySpriteDraw(
                    texture, 
                    drawPos, 
                    null, 
                    color, 
                    Projectile.oldRot[k], 
                    drawOrigin, 
                    Projectile.scale, 
                    Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 
                    0
                );
            }
            
            return true; 
        }
    }
}