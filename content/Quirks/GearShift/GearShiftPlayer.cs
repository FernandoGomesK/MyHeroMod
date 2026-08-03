using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;

using Mono.Cecil.Cil;
using MyHeroMod.content.System;
using KhacesCore.Content.System;
using KhacesCore.Content.System.Interfaces;
using MyHeroMod.content.Projectiles;






namespace MyHeroMod.content.Quirks.Gearshift
{
    // PARTE 1: DADOS E LÓGICA
    public partial class GearshiftPlayer : ModPlayer, IDashModifier, IQuirkResetter
    {
        // Variáveis de Estado
        
        
        public bool isGearshiftBuffActive = false;
        
        
        public bool GearActivation = false; 
        public int ActivationTimer = 0;     
        public int ActivationMaxTime = 40;  
        

        public void FullReset()
        {
            isGearshiftBuffActive = false;
            ActivationTimer = 0;
            Player.ClearBuff(ModContent.BuffType<GearshiftBuff>());
        }
         public override void OnRespawn()
        {
            isGearshiftBuffActive = false;
            ActivationTimer = 0;
            Player.ClearBuff(ModContent.BuffType<GearshiftBuff>());
        }

        public override void ResetEffects()
        {
            isGearshiftBuffActive = false; 
            
        }

        public override void PreUpdate()
        { 
            if (GearActivation)
            {
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
                    ActivateGearshift();
                    GearActivation = false;
                    ActivationTimer = 0;
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
            }
        }
        private void ActivateGearshift()
        {
            var transformPlayer = Player.GetModPlayer<TransformationPlayer>();
            int buffDuration = 180;

            switch(transformPlayer.CurrentStage)
            {
                case QuirkStage.Initial: buffDuration = 187; break;
                case QuirkStage.Adequation: buffDuration = 375; break;
                case QuirkStage.Intermediate: buffDuration = 750; break;
                case QuirkStage.Advanced: buffDuration = 1500; break;
                case QuirkStage.Final: buffDuration = 3000; break;
                default: buffDuration = 6000; break;
            }

            
            Player.AddBuff(ModContent.BuffType<GearshiftBuff>(), buffDuration);
            // Main.NewText("ONE FOR ALL 2ND - GEARSHIFT: TRANSMISSION!", Color.Cyan);
            // CombatText.NewText(Player.getRect(), Color.Cyan, "SECOND GEAR");
            

            // Explosão de partículas
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric, speed.X, speed.Y, 0, Color.Cyan, 2f);
            }
        }

        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (isGearshiftBuffActive)
            {
                velocity *= 2.5f; 
                damage = (int)(damage * 1.3f); 
            }
        }

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