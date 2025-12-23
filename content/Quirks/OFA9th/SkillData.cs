using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th
{
    public class SkillInfo
    {
        public string Name;
        public string Description;
        public string IconPath;
        public QuirkStage MinStage;
    }
    public static class SkillData
    {
        public static Dictionary<OfaSkills, SkillInfo>
        Skills = new Dictionary<OfaSkills, SkillInfo>();

            public static void Load()
            {
                Skills.Clear();

                Skills.Add(OfaSkills.SuperJump, new SkillInfo
                {
                    Name = "Super Jump",
                    Description = "A powerful jump that propels the user into the Sky hurting its legs in the process.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/SuperJump",
                    MinStage = QuirkStage.Initial
                });
                Skills.Add(OfaSkills.DelawareSmash, new SkillInfo
                {
                    Name = "Delaware Smash",
                    Description = "Flick your fingers and shoot a Small gust of wind forward.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/DelawareSmash",
                    MinStage = QuirkStage.Initial
                });
                Skills.Add(OfaSkills.OneForAllFullCowling, new SkillInfo
                {
                    Name = "Full Cowling",
                    Description = "Activate One For All Full Cowling 5%, increasing your overall physical capabilities.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/FullCowling",
                    MinStage = QuirkStage.Adequation
                });
            }

    }
}