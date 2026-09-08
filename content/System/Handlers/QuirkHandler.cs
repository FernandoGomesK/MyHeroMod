using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MyHeroMod.content.System;

namespace MyHeroMod.content.Handlers
{
    public class QuirkHandler : ModPlayer
    {

        public int GetQuirkCost(QuirkType quirk)
        {
            return quirk switch
            {
                QuirkType.OneForAll9th => 4,
                QuirkType.OneForAll8th => 4,
                QuirkType.AllForOne => 4,

                QuirkType.Overhaul => 3,
                QuirkType.Decay => 3,
                QuirkType.HalfColdHalfHot => 3,
                QuirkType.Explosion => 3,
                QuirkType.Blueflame => 3,
                QuirkType.HellFlames => 3,
                QuirkType.Engine => 3,
                QuirkType.OpticBlast => 3,
                QuirkType.Overclock => 3,
                QuirkType.SuperRegeneration => 3,

                QuirkType.Tape => 1,
                QuirkType.FaJin => 1,
                QuirkType.Float => 1,
                QuirkType.Quirkless => 0,

                _ => 2 
            };
        }

        public string GetQuirkDisplayName(QuirkType quirk)
        {
            return quirk switch
            {
                QuirkType.OneForAll9th => "One For All",
                QuirkType.OneForAll8th => "One For All",
                QuirkType.AllForOne => "All For One",
                QuirkType.HalfColdHalfHot => "Half-Cold Half-Hot",
                QuirkType.SuperRegeneration => "Super Regeneration",
                QuirkType.FierceWings => "Fierce Wings",
                QuirkType.SlideAndGlide => "Slide and Glide",
                QuirkType.SpringLikeLimbs => "Spring-Like Limbs",
                QuirkType.ZeroGravity => "Zero Gravity",
                QuirkType.SmokeScreen => "Smoke Screen",
                _ => quirk.ToString()
            };
        }

        public string GetQuirkDescription(QuirkType quirk)
        {
            int cost = GetQuirkCost(quirk);
            string desc = quirk switch
            {
                QuirkType.OneForAll9th => "Stockpile immense power and access the Quirks of past predecessors.",
                QuirkType.Explosion => "Secrete nitroglycerin-like sweat from your palms and ignite it on command.",
                QuirkType.SuperRegeneration => "Rapidly heal physical damage, though it places heavy strain on your body.",
                QuirkType.HalfColdHalfHot => "Generate freezing ice from your right side and scorching flames from your left.",
                QuirkType.AllForOne => "Steal Quirks from others and make them your own, or pass them on.",
                QuirkType.Quirkless => "You possess no superhuman abilities. Rely on your wits and gear.",
                QuirkType.Overhaul => "Disassemble and reassemble matter at will, allowing for rapid healing or devastating attacks.",
                QuirkType.Decay => "Touching someone with this Quirk will cause them to turn to dust",
                QuirkType.Blueflame => "Generate blue flames that burn hotter than normal fire.",
                QuirkType.HellFlames => "Generate hellfire that burns hotter than normal fire.",
                QuirkType.Engine => "Have an engine in your body that allows for high-speed movement and attacks.",
                QuirkType.OpticBlast => "Emit a powerful beam of energy from your eyes.",
                QuirkType.Overclock => "Temporarily Overclock your brain, allowing for faster thinking and reaction times.",
                QuirkType.Tape => "Generate tape from your body, which can be used for swinging and binding enemies.",
                QuirkType.Float => "Float in the air, allowing for aerial combat.",
                QuirkType.FaJin => "Accumulate kinetic energy and release powerful attacks.",
                QuirkType.SmokeScreen => "Generate a cloud of smoke that obscures vision and raises your dodge chance.",
                QuirkType.FierceWings => "Grow wings that allow for flight and enhanced mobility.",
                QuirkType.SlideAndGlide => "Slide across surfaces and glide through the air with ease.",
                QuirkType.Flight => "Surround yourself with a powerful shield that allows for flight and defense.",
                _ => "A unique superhuman ability."
            };
            
            return quirk == QuirkType.Quirkless ? desc : $"{desc}\n[c/FFFF00:Capacity Cost: {cost}]";
        }


        public override void PostUpdateMiscEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();

            int totalQuirkWeight = 0;
            foreach (var quirk in transPlayer.ActiveQuirks)
            {
                totalQuirkWeight += GetQuirkCost(quirk);
            }

            
            int currentCapacity = 4;

           
            if (transPlayer.Nature == NatureType.StrongMinded) 
                currentCapacity += 2; 

            if (transPlayer.Nature == NatureType.PerfectVessel) 
                currentCapacity += 4; 

            int overloadAmount = totalQuirkWeight - currentCapacity;
            ApplyOverloadPenalties(overloadAmount);
        }

        private void ApplyOverloadPenalties(int overloadAmount)
        {
            if (overloadAmount <= 0) return;

            if (overloadAmount == 1)
            {
                Player.moveSpeed *= 0.8f;
                Player.GetDamage(DamageClass.Generic) *= 0.9f;
            }
            else if (overloadAmount == 2)
            {
                Player.moveSpeed *= 0.5f;
                Player.statDefense -= 20;
                Player.AddBuff(BuffID.Confused, 2);
                Player.AddBuff(BuffID.Weak, 2);
            }
            else if (overloadAmount >= 3)
            {
                Player.moveSpeed *= 0.5f;
                Player.statDefense -= 20;
                Player.AddBuff(BuffID.Confused, 2);
                Player.AddBuff(BuffID.Weak, 2);
                
               
                Player.statLifeMax2 = (int)(Player.statLifeMax2 * 0.5f);
                Player.AddBuff(BuffID.Blackout, 2);
            }
        }
    }
}