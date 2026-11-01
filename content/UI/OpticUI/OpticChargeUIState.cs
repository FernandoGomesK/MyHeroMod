//using Microsoft.Xna.Framework;
//using MyHeroMod.content.Quirks.OFA9th;
//using MyHeroMod.content.Quirks.OpticBlast;
//using Terraria;
//using Terraria.UI;
//namespace MyHeroMod.content.UI.OpticUI
//{

//    public class OpticChargeUIState : UIState

//    {

//        public DraggableOpticChargeBar OpticBar;


//        public override void OnInitialize()

//        {

//            OpticBar = new DraggableOpticChargeBar();



//            Player player = Main.LocalPlayer;

//            var opticPlayer = player.GetModPlayer<OpticBlastPlayer>();



//            if (opticPlayer.CurrentPercentage == OpticBlastPlayer.Percentage.Full)

//            {

//                OpticBar.Width.Set(110f, 0f);

//                OpticBar.Height.Set(28f, 0f);

//            }

//            else

//            { 
//                OpticBar.Width.Set(100f, 0f);

//                OpticBar.Height.Set(28f, 0f);
//            }


//            OpticBar.Left.Set(Main.screenWidth - 355f, 0f);

//            OpticBar.Top.Set(10f, 0f);


//            Append(OpticBar);

//        }

//    }
//}