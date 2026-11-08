using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.System;
using KhacesCore.Content.System;
using KhacesCore.Content.System.Interfaces;
using MyHeroMod.content.Projectiles;
using MyHeroMod.content.Debuffs;
using MyHeroMod.content.System.Interfaces;

namespace MyHeroMod.content.Quirks.Gearshift
{
    // PARTE 1: DADOS E LÓGICA
    public partial class GearshiftPlayer : ModPlayer, IDashModifier, IQuirkResetter
    {
        // Variáveis de Estado
        public bool isGearshiftBuffActive = false;
        public bool wasGearshiftBuffActive = false;

        public bool GearActivation = false;
        public int ActivationTimer = 0;
        public int ActivationMaxTime = 40;

        // ---------- Tabelas por estágio ----------

        private static int GetBuffDuration(QuirkStage stage) => stage switch
        {
            QuirkStage.Initial => 380,
            QuirkStage.Adequation => 590,
            QuirkStage.Intermediate => 750,
            QuirkStage.Advanced => 1800,
            QuirkStage.Final => 3500,
            _ => 6000
        };

        private static int GetRecoilDuration(QuirkStage stage) => stage switch
        {
            QuirkStage.Initial => 187,
            QuirkStage.Adequation => 375,
            QuirkStage.Intermediate => 750,
            QuirkStage.Advanced => 1500,
            QuirkStage.Final => 3000,
            _ => 6000
        };

        // ---------- Reset ----------

        public void FullReset()
        {
            ResetGearshiftState();
        }

        public override void OnRespawn()
        {
            ResetGearshiftState();
        }

        private void ResetGearshiftState()
        {
            isGearshiftBuffActive = false;
            GearActivation = false; // evita a ativação terminar depois de morrer/resetar
            ActivationTimer = 0;
            Player.ClearBuff(ModContent.BuffType<GearshiftBuff>());
        }

        public override void ResetEffects()
        {
            wasGearshiftBuffActive = isGearshiftBuffActive;
            isGearshiftBuffActive = false;
        }

        // ---------- Velocidade de corrida ----------
        // Roda depois dos acessórios (Hermes Boots etc.), então o bônus não é sobrescrito.
        public override void PostUpdateRunSpeeds()
        {
            if (!isGearshiftBuffActive)
                return;

            Player.maxRunSpeed += 5.0f;
            Player.accRunSpeed += 5.0f;
        }

        // ---------- Ativação ----------

        public override void PreUpdate()
        {
            if (!GearActivation)
                return;

            ActivationTimer++;
            Player.velocity *= 0.8f;

            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Electric, 0, 0, 100, Color.Cyan, 0.5f);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }

            if (ActivationTimer >= ActivationMaxTime)
            {
                GearActivation = false;
                ActivationTimer = 0;
                ActivateGearshift();
            }
        }

        private void ActivateGearshift()
        {
            // Efeito visual: roda em todos os clientes
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, speed.X, speed.Y, 0, Color.Cyan, 2f);
            }

            // Buff e projétil só no dono do jogador.
            // Nos outros clientes, o buff chega pela sincronização do vanilla e o projétil
            // é replicado pelo servidor; criar aqui duplicaria o projétil N vezes.
            if (Player.whoAmI != Main.myPlayer)
                return;

            var transformPlayer = Player.GetModPlayer<TransformationPlayer>();
            Player.AddBuff(ModContent.BuffType<GearshiftBuff>(), GetBuffDuration(transformPlayer.CurrentStage));

            Vector2 textPosition = Player.Center + new Vector2(0, -60f);
            Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                textPosition,
                Vector2.Zero,
                ModContent.ProjectileType<OverdriveOnomatopoeia>(),
                0,
                0f,
                Player.whoAmI
            );
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (isGearshiftBuffActive)
            {
                velocity *= 2.5f;
                damage = (int)(damage * 1.3f);
            }
        }

        // ---------- Visual: rastro / afterimage ----------
        // O histórico de posições e o desenho ficam no AfterimagePlayer / SpeedAfterimageLayer.
        // Aqui só pedimos o efeito enquanto o buff estiver ativo (todo tick).

        public override void PostUpdateMiscEffects()
        {
            if (isGearshiftBuffActive)
                Player.GetModPlayer<AfterimagePlayer>().Request(AfterimagePalettes.Blue);
        }

        // ---------- Recoil ao fim do buff ----------

        public override void PostUpdateBuffs()
        {
            // Só o dono aplica; os outros clientes recebem o debuff sincronizado
            if (Player.whoAmI != Main.myPlayer)
                return;

            if (!wasGearshiftBuffActive || isGearshiftBuffActive)
                return;

            var transformPlayer = Player.GetModPlayer<TransformationPlayer>();
            int recoilDuration = GetRecoilDuration(transformPlayer.CurrentStage);

            if (transformPlayer.Nature == NatureType.ResistantBody)
                recoilDuration = (int)(recoilDuration * 0.75f);

            if (recoilDuration > 0)
                Player.AddBuff(ModContent.BuffType<GearshiftRecoil>(), recoilDuration);
        }

        // ---------- Netcode ----------

        public override void CopyClientState(ModPlayer targetCopy)
        {
            GearshiftPlayer clone = targetCopy as GearshiftPlayer;
            clone.GearActivation = GearActivation;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncGearshift);
            packet.Write((byte)Player.whoAmI);
            packet.Write(GearActivation);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            GearshiftPlayer clone = clientPlayer as GearshiftPlayer;
            if (GearActivation != clone.GearActivation)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncGearshift);
                packet.Write((byte)Player.whoAmI);
                packet.Write(GearActivation);
                packet.Send(-1, Player.whoAmI);
            }
        }
    }
}