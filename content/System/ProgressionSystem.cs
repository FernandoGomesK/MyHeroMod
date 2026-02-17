using Terraria;
using MyHeroMod.content;

public static class ProgressionSystem {
    public static void UpdateStage(TransformationPlayer player) {
        if (player.ManualStageOverride) return;

        QuirkStage targetStage = QuirkStage.Initial;
        if (NPC.downedMoonlord) targetStage = QuirkStage.Final;
        else if (NPC.downedPlantBoss) targetStage = QuirkStage.Advanced;
        else if (Main.hardMode) targetStage = QuirkStage.Intermediate;
        else targetStage = QuirkStage.Adequation;

        if (player.CurrentStage != targetStage) {
            player.CurrentStage = targetStage;
            player.UpdateUnlockedSkills();
        }
    }
}