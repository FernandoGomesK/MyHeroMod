using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System.Interfaces;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Projectiles;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public abstract class BaseToggleFlashfireFistSkill : QuirkBaseSkill
{

    public abstract override string Name { get; }
    public abstract override string Category { get; }
    
    public override string Description => "Toggle the Flashfire Fist state, increasing heat and empowering skills.";
    public override string IconPath => "MyHeroMod/Assets/Skills/Float/Float";
    public override int BaseCooldown => 30;
    public override bool IsDefaultSkill => false;
    
    public abstract override QuirkType RequiredQuirk { get; }
    public abstract override QuirkStage RequiredStage { get; }

    public override void OnUse(Player player)
    {
            if (player.HasBuff(ModContent.BuffType<FlashfireFistBuff>()))
            {
                player.ClearBuff(ModContent.BuffType<FlashfireFistBuff>());
            }
            else
            {
                player.AddBuff(ModContent.BuffType<FlashfireFistBuff>(), 3600);

                foreach (var modPlayer in player.ModPlayers)
                {
                    if (modPlayer is IHeroTemperature heatUser) 
                    {
                        heatUser.AddHeat(15);
                    }
                }
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/CremationSound") { Volume = 0.5f, PitchVariance = 1.0f }, player.Center);
                    Vector2 textPosition = player.Center + new Vector2(0, -60f);
                    Projectile.NewProjectile(player.GetSource_FromThis(), textPosition, Vector2.Zero, ModContent.ProjectileType<FlashfireOnomatopoeia>(), 0, 0f, player.whoAmI);

            }

            
        }
    }
}


