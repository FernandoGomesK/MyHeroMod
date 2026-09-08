using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using MyHeroMod.content.Quirks.OFA8th;
using System;

namespace MyHeroMod.content.Buffs
{
    public class StockPileBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var transformPlayer = player.GetModPlayer<TransformationPlayer>();
            var ofaPlayer = player.GetModPlayer<OneForAll8thPlayer>();

           
            if (ofaPlayer.form == 1)
            {
            
                player.moveSpeed += 2.5f;   
                player.jumpSpeedBoost += 3.5f;     
                player.statDefense += 15;    
                player.GetDamage(DamageClass.Melee) += 0.15f; 
                player.GetAttackSpeed(DamageClass.Melee) += 0.15f; 
                
                
                Lighting.AddLight(player.Center, Color.Yellow.ToVector3() * 0.8f);
            }
            else if (ofaPlayer.form == 2)
            {
               
                player.moveSpeed += 4f; 
                player.jumpSpeedBoost += 5.5f;
                player.statDefense += 30;    
                player.GetDamage(DamageClass.Melee) += 0.35f; 
                player.GetAttackSpeed(DamageClass.Melee) += 0.30f;  
                
                Lighting.AddLight(player.Center, Color.Goldenrod.ToVector3() * 1.5f);

            
                if (Math.Abs(player.velocity.X) > 0.1f || Math.Abs(player.velocity.Y) > 0.1f)
                {
                    float corVelocidade = 0.5f; 
                    Color corArcoIris = Main.hslToRgb((Main.GlobalTimeWrappedHourly * corVelocidade) % 1f, 1f, 0.6f);
                    Color corTranslucida = corArcoIris * 0.5f; 

                    int dustIndex = Dust.NewDust(
                        player.position, 
                        player.width, 
                        player.height, 
                        DustID.FireworksRGB, 
                        player.velocity.X * -0.5f, 
                        player.velocity.Y * -0.5f, 
                        120,                
                        corTranslucida,   
                        1.2f
                    );
                    
                    Main.dust[dustIndex].noGravity = true;      
                }
            }
        }
    }
}