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
namespace MyHeroMod.content.Buffs
{
    public class FlashfireFistBuff : ModBuff
    {
        private SlotId _loopSoundSlot;
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

               
                Lighting.AddLight(player.Center, new Vector3(0.4f, 0.7f, 1f) * 1.5f);
                
            
                for (int i = 0; i < 2; i++)
                {
                  
                    int blueFire = Dust.NewDust(player.position - new Vector2(4, 4), player.width + 8, player.height + 8, DustID.BlueTorch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[blueFire].noGravity = true;
                    Main.dust[blueFire].velocity.Y -= Main.rand.NextFloat(1f, 3.5f); 
                    Main.dust[blueFire].velocity.X *= 0.3f;
                    Main.dust[blueFire].velocity += player.velocity * 0.4f; 
                    
                  
                    if (Main.rand.NextBool(2)) 
                    {
                        int whiteFire = Dust.NewDust(player.position, player.width, player.height, DustID.IceTorch, 0f, 0f, 50, default, 1.7f);
                        Main.dust[whiteFire].noGravity = true;
                        Main.dust[whiteFire].velocity.Y -= Main.rand.NextFloat(2f, 5f); 
                        Main.dust[whiteFire].velocity.X *= 0.2f;
                        Main.dust[whiteFire].velocity += player.velocity * 0.5f;
                    }

                    
                    if (Main.rand.NextBool(4)) 
                    {
                        int spark = Dust.NewDust(player.position, player.width, player.height, DustID.FireworkFountain_Blue, 0f, 0f, 0, default, 1.2f);
                        Main.dust[spark].noGravity = true;
                        
                        Main.dust[spark].velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-5f, -1f));
                    }
                }
                
                
                player.GetDamage(DamageClass.Melee) += 0.35f; 
                player.moveSpeed += 2.0f; 
                if (!SoundEngine.TryGetActiveSound(_loopSoundSlot, out var activeSound))
                {
                    
                    SoundStyle crackleStyle = new SoundStyle("MyHeroMod/Assets/Sounds/FireCrackingSound");
                    crackleStyle.IsLooped = true; 
                    crackleStyle.Volume = 0.5f; 
                    
                    _loopSoundSlot = SoundEngine.PlaySound(crackleStyle, player.Center);
                }
                else
                {
                  
                    activeSound.Position = player.Center;
                }
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