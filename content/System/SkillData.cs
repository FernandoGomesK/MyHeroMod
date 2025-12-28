using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using MyHeroMod.content;

namespace MyHeroMod.content.System
{
    public class SkillInfo
    {
        public string Name;
        public string Description;
        public string IconPath;
        public QuirkStage MinStage;
        public List<QuirkType> RelatedQuirks;
    }
    public static class SkillData
    {



        public static Dictionary<QuirkSkills, SkillInfo> SkillList = new Dictionary<QuirkSkills, SkillInfo>();

            public static void Load()
            {
                SkillList.Clear();

                //Ofa Generic

                SkillList.Add(QuirkSkills.SuperJump, new SkillInfo
                {
                    Name = "Super Jump",
                    Description = "A powerful jump that propels the user into the Sky hurting its legs in the process.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/SuperJump",
                    MinStage = QuirkStage.Initial,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                    QuirkType.OneForAll8th 
                }
                });

                //One For All 8th

                SkillList.Add(QuirkSkills.PrimeDetroitSmash, new SkillInfo
                {
                    Name = "Detroit Smash",
                    Description = "A Powerfull Punch that releases a shockwave forward in its most powerfull Version",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/DetroitSmash",
                    MinStage = QuirkStage.Adequation,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll8th
                }
                });
                SkillList.Add(QuirkSkills.StockPile, new SkillInfo
                {
                    Name = "Stock Pile",
                    Description = "Conjure all of your quirk's Strenght honed over the years", 
                    IconPath = "MyHeroMod/Assets/Skills/OFA8th/StockPile",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll8th
                }
                });
                SkillList.Add(QuirkSkills.StockPileMaximum, new SkillInfo
                {
                    Name = "Stock Pile Maximum",
                    Description = "Conjure the Maximum of your quirk's Strenght honed over the years",
                    IconPath = "MyHeroMod/Assets/Skills/OFA8th/StockPileMaximum",
                    MinStage = QuirkStage.Advanced,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll8th
                }
                });
                
            
                // One For All 9th
                SkillList.Add(QuirkSkills.DetroitSmash, new SkillInfo
                {
                    Name = "Detroit Smash",
                    Description = "A powerful punch that releases a shockwave forward.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/DetroitSmash",
                    MinStage = QuirkStage.Initial,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.DelawareSmash, new SkillInfo
                {
                    Name = "Delaware Smash",
                    Description = "Flick your fingers and shoot a Small gust of wind forward.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/DelawareSmash",
                    MinStage = QuirkStage.Initial,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.OneForAllFullCowling5, new SkillInfo
                {
                    Name = "Full Cowling 5%",
                    Description = "Activate One For All Full Cowling 5%, increasing your overall physical capabilities.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/FullCowling5Percent",
                    MinStage = QuirkStage.Adequation,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.OneForAllFullCowling8, new SkillInfo
                {
                    Name = "Full Cowling 8%",
                    Description = "Activate One For All Full Cowling 8%, greatly increasing your overall physical capabilities, but straining your body.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/FullCowling8Percent",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.BlackWhipSurge, new SkillInfo
                {
                    Name = "Black Whip Surge",
                    Description = "Shoot out a Mass of Black Energy all around you.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/BlackWhip",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });

                // 
                
            }

    }
}