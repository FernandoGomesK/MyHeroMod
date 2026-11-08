using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs.Gearshift;

namespace MyHeroMod.content.Quirks.GearShift.Projectiles;

/// <summary>
/// Hand projectile that gives <see cref="ChangedGearBuff"/> to the first allied player it touches.
/// Allies are any other player, as long as the owner is not in PvP. It never interacts with NPCs.
/// </summary>
public class ChangeGearProj : ModProjectile
{
    #region Tuning

    /// <summary>Duration of the buff given to the ally, in ticks.</summary>
    public const int BuffDuration = 60 * 30;

    #endregion

    public override string Texture => "MyHeroMod/Assets/Projectiles/HandProj";

    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.friendly = false; 
        Projectile.hostile = false;
        Projectile.tileCollide = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 120;
        Projectile.alpha = 255;
    }

    public override Color? GetAlpha(Color lightColor) => Color.White;

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation();

        if (Main.netMode != NetmodeID.Server && Main.rand.NextBool(2))
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);

        // Hit detection only runs on the owner's client; the buff is then synced through vanilla buff packets
        if (Projectile.owner != Main.myPlayer)
            return;

        Player owner = Main.player[Projectile.owner];

        for (int i = 0; i < Main.maxPlayers; i++)
        {
            Player target = Main.player[i];

            if (!IsAlly(owner, target))
                continue;

            if (Projectile.Hitbox.Intersects(target.Hitbox))
            {
                GiveBuff(target);
                Projectile.Kill();
                break;
            }
        }
    }

    /// <summary>A valid target is another living player, and the owner must not be in PvP.</summary>
    private static bool IsAlly(Player owner, Player target)
    {
        return !owner.hostile
            && target.active
            && !target.dead
            && target.whoAmI != owner.whoAmI;
    }

    private void GiveBuff(Player target)
    {
        target.AddBuff(ModContent.BuffType<ChangedGearBuff>(), BuffDuration);

        CombatText.NewText(target.Hitbox, Color.Cyan, "Gear Changed!");
        SoundEngine.PlaySound(SoundID.Item4, target.position);

        for (int i = 0; i < 10; i++)
        {
            Dust d = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Electric, 0f, 0f, 0, Color.Cyan, 1f);
            d.noGravity = true;
        }
    }
}