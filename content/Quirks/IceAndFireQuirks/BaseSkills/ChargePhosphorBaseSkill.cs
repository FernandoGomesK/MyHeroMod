using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseIAFProjectiles.ChannelingIAFProjectiles;
using MyHeroMod.content.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.BaseSkills
{
    public abstract class PhosphorBase : QuirkBaseSkill
    {
        public override string Name => "Flashfire Fist: Phosphor";
        public override string Description => "Activate your ultimate state";
        public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense"; 
        public override int BaseCooldown => 60; 
        
       
        protected abstract int BuffType { get; }
        protected virtual int CowlingPercentage => 0;
        protected virtual int ChargeDustType => DustID.FlameBurst; 

        public override void OnUse(Player player)
        {
            if (player.HasBuff(BuffType))
            {
                player.ClearBuff(BuffType); 
                CombatText.NewText(player.getRect(), Color.Red, "Deactivated");
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.Red, Name + " Charging!");
                
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), 
                    player.Center, 
                    Vector2.Zero, 
                    ModContent.ProjectileType<BasePhosphorChargeProj>(), 
                    0, 
                    0f, 
                    player.whoAmI, 
                    ai0: 0f, 
                    ai1: BuffType, 
                    ai2: CowlingPercentage 
                );
            }
        }
    }

    public class BluePhosphor : PhosphorBase
    {
        public override string Name => "Blue Phosphor";

        public override string GetDisplayName(Player player) => "Phosphor";
        
        public override string IconPath => "MyHeroMod/Assets/Skills/FullCowling5";
        public override string Category => "Blueflame";
        public override QuirkType RequiredQuirk => QuirkType.Blueflame; 
        public override QuirkStage RequiredStage => QuirkStage.Final;
        
        protected override int BuffType => ModContent.BuffType<PhosphorBuff>(); 
        protected override int ChargeDustType => DustID.DungeonWater; 
    }

    public class HCHHPhosphor : PhosphorBase
    {
        public override string Name => "Phosphor";

        public override string GetDisplayName(Player player) => "Phosphor";
        public override string IconPath => "MyHeroMod/Assets/Skills/FullCowling10";
        public override string Category => "HalfColdHalfHot";
        public override QuirkType RequiredQuirk => QuirkType.HalfColdHalfHot;
        public override QuirkStage RequiredStage => QuirkStage.Advanced;
        
        protected override int BuffType => ModContent.BuffType<PhosphorBuff>(); 
        
    }
}