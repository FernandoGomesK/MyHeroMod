using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Overhaul
{
    public partial class OverhaulPlayer : ModPlayer
    {

        public bool isChimeraActive = false;
        public bool ChimeraActivation = false;
        public int ActivationTimer = 0;
        public int ActivationMaxTime = 120;

        
        public override void ResetEffects()
        {
            isChimeraActive = false;
        }

        
        public override void FrameEffects()
        {
            
            if (Player.HasBuff(ModContent.BuffType<Buffs.ChimeraBuff>()))
            {
                // Forçamos o ID do braço da frente para ser o nosso Acessório Fantasma
                Player.handon = EquipLoader.GetEquipSlot(Mod, "ChimeraArms", EquipType.HandsOn);
                
                // Forçamos o ID do braço de trás
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "ChimeraArms", EquipType.HandsOff);
            }
        }

         public override void PreUpdate()
        { 
            if (ChimeraActivation)
            {
                ActivationTimer++;
                Player.velocity *= 0.8f; 

                if (Main.rand.NextBool(2))
                {
                    Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Wraith, 0, 0, 100, default, 0.5f);
                    d.noGravity = true;
                    d.velocity *= 0.5f;   
                }

                if (ActivationTimer >= ActivationMaxTime)
                {
                    ActivateChimera();
                    ChimeraActivation = false;
                    ActivationTimer = 0;
                }
            }
        }
        private void ActivateChimera()
        {
            var transformPlayer = Player.GetModPlayer<TransformationPlayer>();
            

            

            
            Player.AddBuff(ModContent.BuffType<ChimeraBuff>(), 360000000);
            Main.NewText("Chimera!", Color.Yellow);
            

            // Explosão de partículas
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(8f, 8f);
                Dust.NewDust(Player.position, Player.width, Player.height, DustID.Wraith, speed.X, speed.Y, 0, default, 2f);
            }
        }
    }

}