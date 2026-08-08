using MyHeroMod.content.System.BaseProjectiles;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Hellflame.Projectiles
{
public class HellMinefieldController : BaseGroundWaveController
{
    public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";
    protected override int ProjectileToSpawn => ModContent.ProjectileType<HellMinefieldTrap>();
    protected override int PlacementCooldown => 5;
    protected override float VerticalOffset => -30f; 
}
}