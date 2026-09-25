//using KhacesCore.Content.System;
//using MyHeroMod.content;
//using System.Collections.Generic;
//using Terraria;

//namespace MyHeroMod.Content
//{
//    public static class ProgressionSystem
//    {

//        private static readonly List<ProgressionTier<QuirkStage>> Tiers = new()
//        {
//            new ProgressionTier<QuirkStage>(() => NPC.downedMoonlord, QuirkStage.Final),
//            new ProgressionTier<QuirkStage>(() => NPC.downedPlantBoss, QuirkStage.Advanced),
//            new ProgressionTier<QuirkStage>(() => Main.hardMode, QuirkStage.Intermediate),
//            new ProgressionTier<QuirkStage>(() => NPC.downedBoss1, QuirkStage.Adequation),
//        };

//        public static void UpdateStage(TransformationPlayer player)
//        {
//            if (player.ManualStageOverride) return;

//            QuirkStage targetStage = ProgressionEvaluator.Evaluate(Tiers, QuirkStage.Initial);

//            if (player.CurrentStage != targetStage)
//            {
//                player.CurrentStage = targetStage;
//                player.UpdateUnlockedSkills();
//            }
//        }
//    }
//}