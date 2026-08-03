// using Terraria;
// using Microsoft.Xna.Framework;
// using Terraria.Audio;
// using Terraria.ID;
// using Terraria.ModLoader;
// using MyHeroMod.content.System.BasePlayer;
// using MyHeroMod.content.Quirks.OFA9th.Projectiles;
// using MyHeroMod.content.Buffs;

// namespace MyHeroMod.content.System
// {
//     public class PunchSkill : QuirkSkill
//     {
//         public override string Name => "Punch";
//         public override string Description => "Throw a stronger punch. Various quirks can upgrade it!";
//         public override string IconPath => "MyHeroMod/Assets/Skills/Dash";
//         public override int BaseCooldown => 120;

        
       
//         public override QuirkType RequiredQuirk => QuirkType.Quirkless;
//         public override QuirkStage RequiredStage => QuirkStage.Initial;
        
//         public override bool IsDefaultSkill => true;
//         public override bool IsBaseQuirk => false;
        

//         public override bool CheckUnlock(TransformationPlayer player)
//         {
//             return true; 
//         }

//        