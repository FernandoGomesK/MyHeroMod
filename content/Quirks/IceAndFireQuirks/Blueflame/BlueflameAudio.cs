using Terraria.ModLoader;
using Terraria.Audio;
using ReLogic.Utilities;

namespace MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame
{
    public partial class BlueflamePlayer : ModPlayer
    {
        private SlotId _loopSoundSlot;

        public override void PreUpdate()
        {
        
            if (SoundEngine.TryGetActiveSound(_loopSoundSlot, out var activeSound))
            {
                activeSound.Stop();
            }
        }
        
        
    }
}