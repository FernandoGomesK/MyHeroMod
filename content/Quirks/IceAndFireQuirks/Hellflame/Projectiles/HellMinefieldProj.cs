using Microsoft.Xna.Framework;
using MyHeroMod.content.System.BaseProjectiles;
using Terraria;
using Terraria.ID;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles
{
public class HellMinefieldTrap : BaseTrapProj
{
    public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";
    protected override int TrapWidth => 40;
    protected override int TrapHeight => 20; 
    protected override int TrapDuration => 300; 

    protected override void SpawnTrapVisuals()
    {
    
        if (Main.rand.NextBool(3))
        {
            Vector2 dustPos = new Vector2(Projectile.position.X + Main.rand.NextFloat(Projectile.width), Projectile.position.Y + Projectile.height - 5);
            Dust fire = Dust.NewDustPerfect(dustPos, DustID.Torch, new Vector2(0, -Main.rand.NextFloat(1f, 3f)));
            fire.noGravity = true;
            fire.scale = 1.5f;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 180);
    }
}
}