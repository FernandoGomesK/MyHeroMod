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
            else // HellFlames default
            {
                buffName = "Flashfire Fist";
                tip = "Compresses internal heat to evolve your skills.";
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transformPlayer.HasActiveQuirk(QuirkType.HalfColdHalfHot))
            {
                var hchhPlayer = player.GetModPlayer<HalfColdHalfHotPlayer>();
                hchhPlayer.IsFlashFireFistActive = true;

                Lighting.AddLight(player.Center, Color.OrangeRed.ToVector3() * 0.8f);
                
                int fire = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                Main.dust[fire].noGravity = true;
                Main.dust[fire].velocity *= 3f;
                Main.dust[fire].velocity += player.velocity * 0.5f;
            }
            else if (transformPlayer.HasActiveQuirk(QuirkType.HellFlames))
            {
                var hellPlayer = player.GetModPlayer<HellFlamesPlayer>();
                hellPlayer.IsFlashFireFistActive = true;

                Lighting.AddLight(player.Center, Color.OrangeRed.ToVector3() * 0.8f);
                
                int fire = Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                Main.dust[fire].noGravity = true;
                Main.dust[fire].velocity *= 3f;
                Main.dust[fire].velocity += player.velocity * 0.5f;
                
                player.GetDamage(DamageClass.Melee) += 0.20f; 
                player.moveSpeed += 2.0f; 
            }
            else if (transformPlayer.HasActiveQuirk(QuirkType.Blueflame))
            {
                var bluePlayer = player.GetModPlayer<BlueflamePlayer>();
                bluePlayer.IsFlashFireFistActive = true;
                
                Lighting.AddLight(player.Center, Color.RoyalBlue.ToVector3() * 0.8f);
                
                int fire = Dust.NewDust(player.position, player.width, player.height, DustID.BlueTorch, 0f, 0f, 100, default, 2.5f);
                Main.dust[fire].noGravity = true;
                Main.dust[fire].velocity *= 3f;
                Main.dust[fire].velocity += player.velocity * 0.5f;
                
                player.GetDamage(DamageClass.Melee) += 0.35f; 
                player.moveSpeed += 2.0f; 
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