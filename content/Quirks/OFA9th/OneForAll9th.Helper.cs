using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.OFA9th
{
    // ========================================= Helper ===============================================================================
    public partial class OneForAll9thPlayer 
    {
        public void UnlockQuirks()
        {

        var transPlayer = Player.GetModPlayer<TransformationPlayer>();
    

        InternalQuirks.Clear(); 
        


        if (transPlayer.HasActiveQuirk(QuirkType.OneForAll9th))
    {

        // if (transPlayer.ActiveQuirks >= 1);
        
        if (transPlayer.CurrentStage >= QuirkStage.Initial)
            InternalQuirks.Add(QuirkType.OneForAll9th); 

        if (transPlayer.CurrentStage >= QuirkStage.Adequation)
            

        if (transPlayer.CurrentStage >= QuirkStage.Intermediate)
            InternalQuirks.Add(QuirkType.DangerSense);
            InternalQuirks.Add(QuirkType.BlackWhip);

        if (transPlayer.CurrentStage >= QuirkStage.Advanced)
        {
            InternalQuirks.Add(QuirkType.Float);
            InternalQuirks.Add(QuirkType.SmokeScreen);
            InternalQuirks.Add(QuirkType.FaJin);
        }

        if (transPlayer.CurrentStage >= QuirkStage.Final)
        {
            
            InternalQuirks.Add(QuirkType.Gearshift);
        }
    }

    
        }

}}