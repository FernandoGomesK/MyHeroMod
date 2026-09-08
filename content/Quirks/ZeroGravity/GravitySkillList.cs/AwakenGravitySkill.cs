using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.Gearshift;
using Microsoft.Xna.Framework;
using Terraria.ID;
using MyHeroMod.content.Dusts;

using Terraria.Audio;
using MyHeroMod.content.Quirks.Overclock;



public class AwakenGravitySkill : QuirkBaseSkill
{
    public override string Name => "Awaken";
    public override string Description => "Power Up Zero Gravity.";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/ZeroGravity/AwakenIcon";
    public override string Category => "Zero Gravity";
    public override int BaseCooldown => 1200;
    public override QuirkType RequiredQuirk => QuirkType.ZeroGravity;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;


    public override void OnUse(Player player)
    {
       

        if (player.HasBuff(ModContent.BuffType<GravityAwakenBuff>()))
        {
            player.ClearBuff(ModContent.BuffType<GravityAwakenBuff>());
           
        }
        else
        {

            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

            var timer = transformPlayer.CurrentStage switch
            {
               
                QuirkStage.Advanced => 500,
                QuirkStage.Final => 800,
                _ => 0
            };

            
            player.AddBuff(ModContent.BuffType<GravityAwakenBuff>(),timer);
         
            
           
            
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(player.position, player.width, player.height, DustID.PinkFairy, speed.X, speed.Y, 0, default, 2f);
            }
            
             
        }
    }
}
