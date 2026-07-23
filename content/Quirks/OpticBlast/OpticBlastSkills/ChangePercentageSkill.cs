using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Quirks.OpticBlast; 
using Terraria.Audio;


    public class ChangePercentageSkill : QuirkBaseSkill
    {
        public override string Name => "Change Percentage";
        public override string Description => "Change the percentage of your Optic Blast";
        public override string IconPath => "MyHeroMod/Assets/Skills/DelawareSmash"; 
        public override string Category => "OpticBlast";

        public override int BaseCooldown => 60;

        public override QuirkType RequiredQuirk => QuirkType.OpticBlast;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        public override bool IsDefaultSkill => false;
        public override bool IsBaseQuirk => false;

        public override void OnUse(Player player)
        {
            
            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();
            string text = "Optic Blast: ";

            
            if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.Zero)
            {
                opticPlayer.CurrentPercentage = OpticBlastPlayer.Percentage.TwentyFive;
                text += "25%!";
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/OpticBlast25percent"), player.position);
            }
            else if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.TwentyFive)
            {
                opticPlayer.CurrentPercentage = OpticBlastPlayer.Percentage.Fifty;
                text += "50%!";
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/OpticBlast50percent"), player.position);
            }
            else if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.Fifty)
            {
                opticPlayer.CurrentPercentage = OpticBlastPlayer.Percentage.SeventyFive;
                text += "75%!";
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/OpticBlast75percent"), player.position);
            }
            else if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.SeventyFive)
            {
                opticPlayer.CurrentPercentage = OpticBlastPlayer.Percentage.Full;
                text += "100%!";
                SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/OpticBlast100percent"), player.position);
            }
            else
            {
                opticPlayer.CurrentPercentage = OpticBlastPlayer.Percentage.Zero;
                text += "0%!";
            }

            
            CombatText.NewText(player.getRect(), Color.Blue, text);
        }
    }
