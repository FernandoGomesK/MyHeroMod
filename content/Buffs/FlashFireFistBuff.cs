using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.IceAndFireQuirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.IceAndFireQuirks.Blueflame;
using Terraria.DataStructures;
using Terraria.Audio;
using ReLogic.Utilities;
using MyHeroMod.content.Quirks.IceAndFireQuirks.BaseClass;
namespace MyHeroMod.content.Buffs
{
    public class FlashfireFistBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                if (transPlayer.CurrentStage < QuirkStage.Intermediate)
                {
                    buffName = "Ignite";
                    tip = "Unlocks the left side, enabling Fire skills.";
                }
                else
                {
                    buffName = "Flashfire Fist";
                    tip = "Compresses internal heat to evolve your skills.";
                }
            }
            else if (transPlayer.HasActiveQuirk(QuirkType.Blueflame))
            {
                if (transPlayer.CurrentStage < QuirkStage.Intermediate)
                {
                    buffName = "Crazy Torch";
                    tip = "Unleashes raw blue heat, draining life for power.";
                }
                else
                {
                    buffName = "Flashfire Fist";
                    tip = "Refines your blue flames for devastating attacks.";
                }
            }
            else 
            {
                buffName = "Flashfire Fist";
                tip = "Compresses internal heat to evolve your skills.";
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

            
            int mainDust = DustID.Torch;
            int secondaryDust = DustID.RedTorch;
            int sparkDust = DustID.FireworkFountain_Red;
            Vector3 lightColor = Color.OrangeRed.ToVector3();

            BaseIceAndFirePlayer activePlayer = null;

           
            if (transformPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                activePlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
                activePlayer.IsFlashFireFistActive = true;
            }
            else if (transformPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                activePlayer = player.GetModPlayer<HellFlamesPlayer>();
                activePlayer.IsFlashFireFistActive = true;
            }
            else if (transformPlayer.HasActiveQuirk(QuirkType.Blueflame))
            {
                activePlayer = player.GetModPlayer<BlueflamePlayer>();
                activePlayer.IsFlashFireFistActive = true;

                mainDust = DustID.BlueTorch;
                secondaryDust = DustID.IceTorch;
                sparkDust = DustID.FireworkFountain_Blue;
                lightColor = new Vector3(0.4f, 0.7f, 1f) * 1.5f;
            }

            
            player.GetDamage(DamageClass.Melee) += 0.35f;
            player.moveSpeed += 2.0f;
            Lighting.AddLight(player.Center, lightColor * 0.8f);

           
            for (int i = 0; i < 2; i++)
            {
                int fire = Dust.NewDust(player.position - new Vector2(4, 4), player.width + 8, player.height + 8, mainDust, 0f, 0f, 100, default, 2.5f);
                Main.dust[fire].noGravity = true;
                Main.dust[fire].velocity.Y -= Main.rand.NextFloat(1f, 3.5f); 
                Main.dust[fire].velocity.X *= 0.3f;
                Main.dust[fire].velocity += player.velocity * 0.4f; 
                
                if (Main.rand.NextBool(2)) 
                {
                    int hotFire = Dust.NewDust(player.position, player.width, player.height, secondaryDust, 0f, 0f, 50, default, 1.7f);
                    Main.dust[hotFire].noGravity = true;
                    Main.dust[hotFire].velocity.Y -= Main.rand.NextFloat(2f, 5f); 
                    Main.dust[hotFire].velocity.X *= 0.2f;
                    Main.dust[hotFire].velocity += player.velocity * 0.5f;
                }
                
                if (Main.rand.NextBool(4)) 
                {
                    int spark = Dust.NewDust(player.position, player.width, player.height, sparkDust, 0f, 0f, 0, default, 1.2f);
                    Main.dust[spark].noGravity = true;
                    Main.dust[spark].velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-5f, -1f));
                }
            }

            
           

            if (activePlayer != null)
            {
                SoundStyle crackleStyle = new SoundStyle("MyHeroMod/Assets/Sounds/FireCrackingSound")
                {
                    IsLooped = true,
                    Volume = 0.5f
                };
                activePlayer.PlayLoopSound(crackleStyle, player.Center);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            Player player = Main.LocalPlayer;
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

            string texturePath = "MyHeroMod/Content/Buffs/HCFireFistBuff"; 

            if (transformPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                texturePath = "MyHeroMod/Content/Buffs/FlashFireFistBuff";
            }
            else if (transformPlayer.HasActiveQuirk(QuirkType.Blueflame))
            {
                texturePath = "MyHeroMod/Content/Buffs/BlueFlashFireFistBuff"; 
            }

            if (ModContent.HasAsset(texturePath))
            {
                Texture2D customTexture = ModContent.Request<Texture2D>(texturePath).Value;
                drawParams.Texture = customTexture;
                drawParams.SourceRectangle = customTexture.Frame(); 
            }

            return true;
        }
    }
}