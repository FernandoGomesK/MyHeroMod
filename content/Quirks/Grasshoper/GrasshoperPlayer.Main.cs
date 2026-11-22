using KhacesCore.Content.System.Interfaces;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System.Interfaces;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyHeroMod.content.Quirks.Grasshoper
{
    public class GrasshoperPlayer : ModPlayer, IQuirkResetter, IDashModifier
    {

        public bool IsGrasshoper = false;
        public bool IsIronSolesOn = false;

        public void FullReset()
        {
             IsGrasshoper = false;
            IsIronSolesOn = false;

        }
        public override void ResetEffects()
        {

            IsGrasshoper = false;
            IsIronSolesOn = false;
        }

        public override void FrameEffects()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (Player.HasBuff(ModContent.BuffType<content.Buffs.Grasshoper.GrasshoperBuff>()))
            {
                if (transPlayer.CurrentVariant == QuirkVariant.Default)
                {
                    Player.head = EquipLoader.GetEquipSlot(Mod, "GrasshoperHelmet", EquipType.Head);
                    Player.body = EquipLoader.GetEquipSlot(Mod, "GrasshoperBody", EquipType.Body);
                    Player.legs = EquipLoader.GetEquipSlot(Mod, "GrasshoperLegs", EquipType.Legs);
                }
                else if (transPlayer.CurrentVariant == QuirkVariant.Variant1)
                {
                    Player.head = EquipLoader.GetEquipSlot(Mod, "ShinHelmet", EquipType.Head);
                    Player.body = EquipLoader.GetEquipSlot(Mod, "ShinBody", EquipType.Body);
                    Player.legs = EquipLoader.GetEquipSlot(Mod, "ShinLeggings", EquipType.Legs);
                }
                else if (transPlayer.CurrentVariant == QuirkVariant.Variant2)
                {
                    Player.head = EquipLoader.GetEquipSlot(Mod, "IchigoHelmet", EquipType.Head);
                    Player.body = EquipLoader.GetEquipSlot(Mod, "IchigoBody", EquipType.Body);
                    Player.legs = EquipLoader.GetEquipSlot(Mod, "IchigoLeggings", EquipType.Legs);
                }

                


            }
        }

        public override void PostUpdate()
        {


        }

       public void ModifyDash(ref float speed, ref bool isEnhanced, ref bool hideNormalDash,
       ref Color explosionColor, ref int dustType, ref int onomatopoeiaType)
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();


            if (!transPlayer.HasActiveQuirk(QuirkType.Grasshoper)) return;


            float dashSpeed = transPlayer.CurrentStage switch
            {
                QuirkStage.Initial => 20f,
                QuirkStage.Adequation => 25f,
                QuirkStage.Intermediate => 35f,
                QuirkStage.Advanced => 40f,
                QuirkStage.Final => 60f,
                _ => 80f
            };

            isEnhanced = true;
            dustType = DustID.Cloud;
            explosionColor = Color.White;


            speed = dashSpeed;

            // SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash1") with { Volume = 0.8f }, Player.position);
        }
    }
}