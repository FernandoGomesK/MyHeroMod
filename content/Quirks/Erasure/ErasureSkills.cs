using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Buffs;

using Terraria.Audio;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.OFA9th;
using Humanizer;
using MyHeroMod.content.Quirks.Erasure.Projectiles;


namespace MyHeroMod.content.Quirks.Erasure;

    public partial class ErasurePlayer : ModPlayer, IQuirkResetter
{
    

    public void quirkErasing()
    {
        var erasePlayer = Player.GetModPlayer<ErasurePlayer>();


        
    }
}