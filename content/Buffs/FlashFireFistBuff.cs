using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyHeroMod.content.System;
using MyHeroMod.content.Quirks.HalfColdHalfHot;
using MyHeroMod.content.Quirks.HellFlames;
using MyHeroMod.content.Quirks.Blueflames;
using Terraria.DataStructures;

namespace MyHeroMod.content.Buffs
{
    public class FlashFireFistBuff : ModBuff
    {
        
        public override string Texture => "MyHeroMod/Assets/BuffImage/HCFireFistBuff";

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

        
            if (transformPlayer.SelectedQuirk == QuirkType.HalfColdHalfHot)
            {
                player.GetModPlayer<HalfColdHalfHotPlayer>().IsFlashFireFistActive = true;
                var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();


                if (hchhPlayer.IsFlashFireFistActive)
            {
                
                // drawInfo.colorArmorBody = Color.OrangeRed;
                // drawInfo.colorArmorHead = Color.OrangeRed;
                // drawInfo.colorArmorLegs = Color.OrangeRed;

                
                Lighting.AddLight(player.Center, Color.OrangeRed.ToVector3() * 0.8f);
                
                    int fire = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[fire].noGravity = true;
                    Main.dust[fire].velocity *= 3f;
                    Main.dust[fire].velocity += player.velocity * 0.5f;
                
                
                
            }

            }
            else if (transformPlayer.SelectedQuirk == QuirkType.HellFlames)
            {
                var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
                hellPlayer.IsFlashFireFistActive = true;

                if (hellPlayer.IsFlashFireFistActive)
            {
                
                // drawInfo.colorArmorBody = Color.OrangeRed;
                // drawInfo.colorArmorHead = Color.OrangeRed;
                // drawInfo.colorArmorLegs = Color.OrangeRed;

                
                Lighting.AddLight(player.Center, Color.OrangeRed.ToVector3() * 0.8f);
                
                    int fire = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[fire].noGravity = true;
                    Main.dust[fire].velocity *= 3f;
                    Main.dust[fire].velocity += player.velocity * 0.5f;
                
                
                
            }
                
                
                player.GetDamage(DamageClass.Melee) += 0.20f; 
                player.moveSpeed += 2.0f; 
            }
            else if (transformPlayer.SelectedQuirk == QuirkType.BlueFlames)
            {
                var bluePlayer = player.GetModPlayer<BlueFlamesPlayer>();
                bluePlayer.IsFlashFireFistActive = true;
                if (bluePlayer.IsFlashFireFistActive)
            {
                
                // drawInfo.colorArmorBody = Color.OrangeRed;
                // drawInfo.colorArmorHead = Color.OrangeRed;
                // drawInfo.colorArmorLegs = Color.OrangeRed;

                
                Lighting.AddLight(player.Center, Color.OrangeRed.ToVector3() * 0.8f);
                
                    int fire = Dust.NewDust(player.position, player.width, player.height, DustID.BlueTorch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[fire].noGravity = true;
                    Main.dust[fire].velocity *= 3f;
                    Main.dust[fire].velocity += player.velocity * 0.5f;
                
                
                
            }

                
                player.GetDamage(DamageClass.Melee) += 0.35f; 
                player.moveSpeed += 2.0f; 
            }
        }

        
        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            Player player = Main.LocalPlayer;
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

            string texturePath = "MyHeroMod/Assets/BuffImage/HCFireFistBuff"; 

            
            if (transformPlayer.SelectedQuirk == QuirkType.HellFlames)
            {
                texturePath = "MyHeroMod/Assets/BuffImage/FlashFireFistBuff";
            }
            else if (transformPlayer.SelectedQuirk == QuirkType.BlueFlames)
            {
                texturePath = "MyHeroMod/Assets/BuffImage/BlueFlashFireFistBuff"; 
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