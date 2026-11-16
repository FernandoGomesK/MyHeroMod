using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Hairs.BruceLeeHair
{
    public class BruceLeeHair : ModHair
    {

        public override void AutoStaticDefaults()
        {
            TextureAssets.PlayerHair[Type] = ModContent.Request<Texture2D>("MyHeroMod/content/Hairs/BruceLeeHair/BruceLeeHair");
            TextureAssets.PlayerHairAlt[Type] = ModContent.Request<Texture2D>("MyHeroMod/content/Hairs/BruceLeeHair/BruceLeeHair_Alt");
        }

        public override Gender RandomizedCharacterCreationGender => Gender.Male;

        public override bool AvailableDuringCharacterCreation => true;

        public override void SetStaticDefaults()
        {
            HairID.Sets.DrawBackHair[Type] = false;
            //Mod.Logger.Info($"[BruceLeeHair] Registered with Hair Type ID: {Type} / Total Hairs: {HairLoader.Count}");
        }
        //protected sealed override void Register()
        //{
        //    ModTypeLookup<ModHair>.Register(this);
        //    Type = HairLoader.Register(this);
        //    HairID.Search.Add(FullName, Type);
        //}

        
    }
}