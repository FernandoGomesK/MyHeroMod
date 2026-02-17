using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.Quirks.DangerSense;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;

public class DangerSenseSkill : QuirkSkill
{
    public override string Name => "DangerSense";
    public override int BaseCooldown => 30;

    public override void OnUse(Player player)
    {
        var dsPlayer = player.GetModPlayer<DangerSensePlayer>();

        

            if (dsPlayer.CurrentStage >= MyHeroMod.content.QuirkStage.Adequation)
            {
                player.AddBuff(ModContent.BuffType<OvertimeBuff>(), 300);
                dsPlayer.IsOvertimeActive = true;
                dsPlayer.IsDangerSenseActive = true; // Ativa automaticamente no overtime
                CombatText.NewText(player.getRect(), Color.Yellow, "Overtime!");
            }
            else
            {
                // Alterna o estado (Toggle)
                ToggleDangerSense(player, dsPlayer);
            }
        }

        // 3. Método auxiliar (corrigido para fora do OnUse)
        private void ToggleDangerSense(Player player, DangerSensePlayer dsPlayer)
        {
            dsPlayer.IsDangerSenseActive = !dsPlayer.IsDangerSenseActive;

            if (dsPlayer.IsDangerSenseActive)
            {
                CombatText.NewText(player.getRect(), Color.Orange, "Danger Sense: ON");
                SoundEngine.PlaySound(SoundID.Item4, player.position);
            }
            else
            {
                CombatText.NewText(player.getRect(), Color.Gray, "Danger Sense: OFF");
                SoundEngine.PlaySound(SoundID.Item4, player.position);
            }
        }
    }
