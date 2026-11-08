using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;


namespace MyHeroMod.content.Quirks.Gearshift
{

    public partial class GearshiftPlayer : ModPlayer
    {
        public override void CopyClientState(ModPlayer targetCopy)
        {
            GearshiftPlayer clone = targetCopy as GearshiftPlayer;
            clone.GearActivation = GearActivation;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)MyHeroMod.MessageType.SyncGearshift);
            packet.Write((byte)Player.whoAmI);
            packet.Write(GearActivation);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            GearshiftPlayer clone = clientPlayer as GearshiftPlayer;
            if (GearActivation != clone.GearActivation)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MyHeroMod.MessageType.SyncGearshift);
                packet.Write((byte)Player.whoAmI);
                packet.Write(GearActivation);
                packet.Send(-1, Player.whoAmI);
            }
        }
    }
}
