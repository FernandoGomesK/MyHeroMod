// using Terraria;
// using Terraria.ModLoader;

// using MyHeroMod.content.Buffs;
// using MyHeroMod.content.System;

// public class FajinSkill : QuirkSkill
// {
//     public override string Name => "Fa Jin";
//     public override int BaseCooldown => 30;

//     public override void OnUse(Player player)
//     {
//         if (player.HasBuff(ModContent.BuffType<SmokescreenBuff>()))
//         {
//             player.ClearBuff(ModContent.BuffType<SmokescreenBuff>());
//         }
//         else
//         {
//             player.AddBuff(ModContent.BuffType<SmokescreenBuff>(), 3600);
//         }
//     }
// }