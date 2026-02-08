using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.OFA9th.Buffs;


namespace MyHeroMod.content.Quirks.FaJin;

    public partial class FaJinPlayer : ModPlayer
    {
        public int FaJinCharges = 0;
        public int MaxFaJinCharges = 3;
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
   
    if (FaJinStored) 
    {
        
        if (Player.controlJump && Player.velocity.Y == 0 && Player.releaseJump)
        {
            
            Player.velocity.Y = -25f; 

           
            FaJinCharges = 0;
            FaJinStored = false;

            
            Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.Item14, Player.position); 
            CombatText.NewText(Player.getRect(), Microsoft.Xna.Framework.Color.Red, "FA JIN!");
            
            for (int i = 0; i < 30; i++)
            {
                int d = Dust.NewDust(Player.position + new Microsoft.Xna.Framework.Vector2(0, Player.height - 5), Player.width, 10, Terraria.ID.DustID.RedTorch, 0, 0, 100, default, 2f);
                Main.dust[d].velocity *= 3f; 
                Main.dust[d].noGravity = true;
            }
        }
    }
}

        

        public override void ResetEffects()
        {
            
            var ModPlayer = Player.GetModPlayer<TransformationPlayer>();

            // Verifica se é Explosão e se o estágio é Adequation ou maior
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

        

        
    }
    

