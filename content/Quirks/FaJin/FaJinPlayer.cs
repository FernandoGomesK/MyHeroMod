using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.OFA9th.Buffs;
using Terraria.Audio;


namespace MyHeroMod.content.Quirks.FaJin;

    public partial class FaJinPlayer : ModPlayer
    {
        public int FaJinCharges = 0;
        public int MaxFaJinCharges = 5;
        public bool FaJinStored = false;
        public Dictionary<QuirkSkills, int> SkillCooldowns = new Dictionary<QuirkSkills, int>();

       
        


        public override void OnRespawn()
        {
            
        }

        

        public override void PostUpdateEquips()
        {
            var mainPlayer = Player.GetModPlayer<TransformationPlayer>();

            if (FaJinCharges >= MaxFaJinCharges)
            {
                FaJinStored = true;
                Player.AddBuff(ModContent.BuffType<FaJinBuff>(), 2);
            }
            else
            {
                FaJinStored = false;
            }
        }
        public override void PostUpdateMiscEffects()
{
   
    if (!FaJinStored) 
    {
        
        if (Player.controlJump && Player.velocity.Y == 0 && Player.releaseJump)
        {
            chargeFaJin();
        }
        }
    }


        

        public override void ResetEffects()
        {
            
            var ModPlayer = Player.GetModPlayer<TransformationPlayer>();

            
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            
        }
        
        public override void PreUpdate()
        {
            List<QuirkSkills> keys = new List<QuirkSkills>(SkillCooldowns.Keys);
            foreach (var skill in keys)
            {
                if (SkillCooldowns[skill] > 0) SkillCooldowns[skill]--;
            }
           
        }
        public override void PostUpdate()
    {
        
        
    }
    public void fullChargeFaJin()
        {
                FaJinCharges = MaxFaJinCharges;
                Main.NewText("Fa Jin storage is full!", Color.Red);
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinSound"), Player.position);
                return;
        }

    public void chargeFaJin()
        {
            FaJinCharges++;
            SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/FaJinStoringSound"), Player.position);
            Main.NewText($"Stored Fa Jin energy! Current charges: {FaJinCharges}", Color.LimeGreen);
            CombatText.NewText(Player.getRect(), Color.Orange, $"Fa Jin Charges: {FaJinCharges}");
            if (FaJinCharges >= MaxFaJinCharges)
            {
                fullChargeFaJin();
            }
        }

        

        
    }
    

