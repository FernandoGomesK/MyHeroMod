using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;

using MyHeroMod.content.Quirks.FaJin;

public class FajinSkill : QuirkSkill
{
    public override string Name => "Toggle FaJin";
    public override string Description => "Activates Fa Jin";
    public override string IconPath => "MyHeroMod/Assets/Skills/DangerSense";

    public override int BaseCooldown => 30;

    public override QuirkType RequiredQuirk => QuirkType.FaJin;
    public override QuirkStage RequiredStage => QuirkStage.Advanced;
    public override bool IsDefaultSkill => false;
    public override bool IsBaseQuirk => true;


                    public override void OnUse(Player player)
            {
                var FajinPlayer = player.GetModPlayer<FajinPlayer>();  
                if (player.HasBuff(ModContent.BuffType<FaJinActiveBuff>()))
                {
                    player.ClearBuff(ModContent.BuffType<FaJinActiveBuff>());
                    CombatText.NewText(player.getRect(), Color.Gray, "Fa jin: OFF");
                    FajinPlayer.FaJinCharges = 0;
                    SoundEngine.PlaySound(SoundID.Item4, player.position);
                }
                else
                {
                    player.AddBuff(ModContent.BuffType<FaJinActiveBuff>(), 3600);
                    CombatText.NewText(player.getRect(), Color.Orange, "Fa jin: ON");
                    SoundEngine.PlaySound(SoundID.Item4, player.position);
                    
                }
            }
                
            }

            
            

    
    
    