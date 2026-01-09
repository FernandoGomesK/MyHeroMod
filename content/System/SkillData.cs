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
                SkillList.Add(QuirkSkills.OneForAllFullCowling45, new SkillInfo
                {
                    Name = "Full Cowling 45%",
                    Description = "Activate One For All Full Cowling 45%, greatly increasing your overall physical capabilities, but straining your body.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/FullCowling45Percent",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.BlackWhipHook, new SkillInfo
                {
                    Name = "Black Whip Hook",
                    Description = "Shoot out a black energy whip that can latch onto surfaces and pull you towards them.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/BlackWhipHook",
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
                SkillList.Add(QuirkSkills.Float, new SkillInfo
                {
                    Name = "Float",
                    Description = "Defy gravity and float in the air for a short duration.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/Float",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.SmokeScreen, new SkillInfo
                {
                    Name = "One For All 6th: Smokes Screen",
                    Description = "Create a screen of smoke to obscure vision and hide your movements.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/SmokesScreen",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.FaJinStore, new SkillInfo
                {
                    Name = "One For All 3th: Fa Jin Store",
                    Description = "Store energy in your body for later use.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/FaJinStore",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.DangerSense, new SkillInfo
                {
                    Name = "One For All 4th: Danger Sense",
                    Description = "Heighten your senses to detect nearby threats and dangers.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/DangerSense",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });
                SkillList.Add(QuirkSkills.Gearshift, new SkillInfo
                {
                    Name = "One For All 2nd: Gearshift",
                    Description = "Activate Gearshift, greatly increasing your Speed to Superhuman Levels, but straining your body.",
                    IconPath = "MyHeroMod/Assets/Skills/OFA9th/Gearshift",
                    MinStage = QuirkStage.Advanced,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.OneForAll9th, 
                }
                });

                // Hell Flames

                SkillList.Add(QuirkSkills.FlashFireFist, new SkillInfo
                {
                    Name = "Flash Fire Fist",
                    Description = "Raise your fire temperature to its highest level.",
                    IconPath = "MyHeroMod/Assets/Skills/HellFlames/FlashFireFist",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.HellFlames, 
                }
                });
                SkillList.Add(QuirkSkills.JetBurn, new SkillInfo
                {
                    Name = "Jet Burn",
                    Description = "Launch a concentrated Beam of Fire at your Cursor.",
                    IconPath = "MyHeroMod/Assets/Skills/HellFlames/JetBurn",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.HellFlames, 
                }
                });
                SkillList.Add(QuirkSkills.ProminenceBurn, new SkillInfo
                {
                    Name = "Proeminence Burn",
                    Description = "Launch a concentrated Beam of Fire at your Cursor.",
                    IconPath = "MyHeroMod/Assets/Skills/HellFlames/ProeminenceBurn",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.HellFlames, 
                }
                });
                SkillList.Add(QuirkSkills.HellSpider, new SkillInfo
                {
                    Name = "Hell Spider",
                    Description = "Shoot lines of fire from the tip of your fingers.",
                    IconPath = "MyHeroMod/Assets/Skills/HellFlames/HellSpider",
                    MinStage = QuirkStage.Advanced,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.HellFlames, 
                }
                });
                SkillList.Add(QuirkSkills.IgnitedArrow, new SkillInfo
                {
                    Name = "Ignited Arrow",
                    Description = "Shoot an arrow engulfed in flames that explodes on impact.",
                    IconPath = "MyHeroMod/Assets/Skills/HellFlames/IgnitedArrow",
                    MinStage = QuirkStage.Advanced,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.HellFlames, 
                }
                });

                // Explosion

                SkillList.Add(QuirkSkills.StunGrenade, new SkillInfo
                {
                    Name = "Stun Grenade",
                    Description = "Create a sphere blast that blinds anyone close.",
                    IconPath = "MyHeroMod/Assets/Skills/Explosion/ExplosiveShot",
                    MinStage = QuirkStage.Initial,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.Explosion, 
                }
                });

                SkillList.Add(QuirkSkills.ApShot, new SkillInfo
                {
                    Name = "AP Shot",
                    Description = "Fire an armor-piercing Explosion that can penetrate multiple enemies.",
                    IconPath = "MyHeroMod/Assets/Skills/Explosion/APShot",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.Explosion, 
                }
                });
                SkillList.Add(QuirkSkills.ApMachineGun, new SkillInfo
                {
                    Name = "AP Machine-Gun",
                    Description = "Fire a Barrage of armor-piercing Explosion .",
                    IconPath = "MyHeroMod/Assets/Skills/Explosion/APShot",
                    MinStage = QuirkStage.Intermediate,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.Explosion, 
                }
                });

                SkillList.Add(QuirkSkills.HowitzerImpact, new SkillInfo
                {
                    Name = "Howitzer Impact",
                    Description = "Jump in the Air and come down striking.",
                    IconPath = "MyHeroMod/Assets/Skills/Explosion/HowitzerImpact",
                    MinStage = QuirkStage.Adequation,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.Explosion, 
                }
                });
                SkillList.Add(QuirkSkills.Cluster, new SkillInfo
                {
                    Name = "Cluster",
                    Description = "Condensate your blasts, increasing their impact, force and your speed.",
                    IconPath = "MyHeroMod/Assets/Skills/Explosion/Cluster",
                    MinStage = QuirkStage.Advanced,
                    RelatedQuirks = new List<QuirkType> { 
                    QuirkType.Explosion, 
                }
                });

                // 
                
            }

    }
}