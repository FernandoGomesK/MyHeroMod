using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.ZeroGravity;
using MyHeroMod.content.Debuffs; 
using MyHeroMod.content.Buffs;
using MyHeroMod.content;   

public class GravityReleaseSkill : QuirkBaseSkill
{
    public override string Name => "Release";
    public override string Description => "Negate the gravitational pull of objects at a distance";
    public override string IconPath => "MyHeroMod/Assets/SkillIcons/ZeroGravity/ReleaseIcon";
    public override string Category => "ZeroGravity";

    public override int BaseCooldown => 120;
    public override QuirkType RequiredQuirk => QuirkType.ZeroGravity;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;
    

    public override void OnUse(Player player)
    {
        var zPlayer = player.GetModPlayer<ZeroGravityPlayer>();

        
        player.ClearBuff(ModContent.BuffType<ZeroGravityBuff>());
        zPlayer.isZeroGravityActive = false;

        
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];

            if (npc.active && npc.HasBuff(ModContent.BuffType<ZeroGravityEnemyBuff>()))
            {
                int buffIndex = npc.FindBuffIndex(ModContent.BuffType<ZeroGravityEnemyBuff>());
                if (buffIndex != -1)
                {
                    npc.DelBuff(buffIndex);
                }

                npc.GetGlobalNPC<ZeroGravityGlobalNPC>().hasZeroGravity = false;
            }
            
            if (npc.active && npc.HasBuff(ModContent.BuffType<ZeroGravityBuff>()))
            {
                int buffIndex = npc.FindBuffIndex(ModContent.BuffType<ZeroGravityBuff>());
                if (buffIndex != -1)
                {
                    npc.DelBuff(buffIndex); 
                }

            
                
                
                npc.GetGlobalNPC<ZeroGravityGlobalNPC>().hasZeroGravity = false;
            }
        }

        
        Terraria.Audio.SoundEngine.PlaySound(SoundID.MaxMana, player.position);
        for (int i = 0; i < 15; i++)
        {
            Dust.NewDust(player.position, player.width, player.height, DustID.PinkFairy, 0f, -2f, 150, default, 1.5f);
        }
    }
}