using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflameflame.Projectiles;
using MyHeroMod.content.System.BaseProjectiles;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame.Projectiles
{
public class BlueHellMinefieldController : BaseGroundWaveController
{
    public override string Texture => "MyHeroMod/Assets/Projectiles/RivetStabProj";
    protected override int ProjectileToSpawn => ModContent.ProjectileType<BlueHellMinefieldTrap>();
    protected override int PlacementCooldown => 5;
    protected override float VerticalOffset => -30f; 
}
}