using System.Collections.Generic;
using MyHeroMod.content.Quirks.Explosion.Projectiles;
using MyHeroMod.content.Quirks.Smokescreen; 
using MyHeroMod.content.System; // Adiciona os namespaces das tuas skills



    public static class SkillLibrary
    {
        // Dicionário que mapeia o Enum para a instância da Skill
        private static readonly Dictionary<QuirkSkills, QuirkSkill> _skills = new()
        {
            // General Skills

            // All for One

            {QuirkSkills.StealQuirk, new StealQuirkSkill() },

            // HcHh

            { QuirkSkills.HCFireFist, new FlashFireFistSkill() },
            { QuirkSkills.HCPhosphor, new TogglePhosphorSkill() },
            {QuirkSkills.JetKindling, new  iceThrowerJetKindling() },
            {QuirkSkills.HCHellSpider, new iceSpikeHellSpider() },
            {QuirkSkills.HeavenPiercingWall, new HeavenPiercingGreatGlacial()},
            {QuirkSkills.FlashFreezeHeatWave, new FlashFreezeSkill( )},

            // Explosion

            {QuirkSkills.Cluster , new Clusterkill()},
            {QuirkSkills.ApShot , new ApShotSkill()},
            {QuirkSkills.ApMachineGun , new ApMachineGunSkill()},
            {QuirkSkills.FullPowerBlast , new FullPowerBlastSkill()},
            {QuirkSkills.HowitzerImpact , new HowitzerImpactSkill()},
            {QuirkSkills.StunGrenade , new StunGrenadeSkill()},

            // Common Skills


            { QuirkSkills.Dash, new DashSkill() },

            //BlackWhip
            { QuirkSkills.BlackWhipHook, new BlackWhipHookSkill() },

            // FaJin

            {QuirkSkills.FaJinStore, new FajinSkill( )},


            // Smokescreen 

            { QuirkSkills.Smokescreen, new SmokescreenSkill() },

            // Float
            { QuirkSkills.Float, new FloatSkill() }, 

            // DangerSense
            
            { QuirkSkills.DangerSense, new DangerSenseSkill() },
            {QuirkSkills.ToggleDangerSense, new ToggleDangerSenseSkill() },

            // GearShift

            { QuirkSkills.Gearshift, new GearShiftSkill() },

            // One For All 8th

            { QuirkSkills.CaliforniaSmash, new CaliforniaSmashSkill() },
            { QuirkSkills.TexasSmash, new TexasSmashSkill() },
            { QuirkSkills.CarolinaSmash, new CarolinaSmashSkill() },
            { QuirkSkills.StockPile, new StockPile() },
            { QuirkSkills.StockPileMaximum, new StockPileMaximum() },

            // One For All 9th

            {QuirkSkills.DelawareSmash, new DelawareSmashSkill() },

            {QuirkSkills.DetroitSmash, new DetroitSmashSkill() },
            {QuirkSkills.ManchesterSmash, new ManchesterSmashSkill()  },
            {QuirkSkills.STLouisSmash, new StLouisSmashSkill() },   
            

            {QuirkSkills.OneForAllFullCowling5, new FullCowling5() },

            {QuirkSkills.OneForAllFullCowling8, new FullCowling10() },
            {QuirkSkills.OneForAllFullCowling45, new FullCowling45() },

            // Tape

            {QuirkSkills.ShootSwingingTape, new ShootTapeSkill() },
            {QuirkSkills.PullTape, new PullTapeSkill() },

            // OverClock

            {QuirkSkills.Overclock, new OverclockSkill() },




             

        
        };

        public static QuirkSkill GetSkill(QuirkSkills id)
        {
            return _skills.TryGetValue(id, out var skill) ? skill : null ;
        }

        public static List<QuirkSkills> GetAllIds() => new List<QuirkSkills>(_skills.Keys);
    }
