using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using MyHeroMod.content.Items.Support; 
using MyHeroMod.content.Items;
using MyHeroMod.content.Npcs.Bosses.AllForOne.Projectiles;
using MyHeroMod.content.Quirks.Rivet.Projectiles;
using MyHeroMod.content.Items.QuirkItems.QuirkSyringes;
using MyHeroMod.content.Npcs.Enemies.Nomu; 

namespace MyHeroMod.content.Npcs.D_Garaki
{
    [AutoloadHead]
    public class D_Garaki : ModNPC
    {
       
        public bool awaitingNomuConfirm = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25; 
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0; 
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true; 
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 10; 
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
        
            AnimationType = NPCID.Guide; 
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() { "Doc. Garaki"};
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active) continue;
                
                var transPlayer = player.GetModPlayer<TransformationPlayer>();
                
                if (player.HasItem(ModContent.ItemType<QuirkGene>()) || 
                    player.HasItem(ModContent.ItemType<QuirkSyringe>()) || 
                    transPlayer.ActiveQuirks.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override string GetChat()
        {
            
            awaitingNomuConfirm = false; 

            int dialog = Main.rand.Next(3); 
            if (dialog == 0) return "My Master will be at full strength soon, but you may be usefull.";
            if (dialog == 1) return "Will You help me return him to his glory?";
            return "Prove your worthiness";
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            if (awaitingNomuConfirm)
            {
                button = "Pay 50 Gold"; 
                button2 = "Cancel";
            }
            else
            {
                button = "Quirk Modifications"; 
                button2 = "Call my Nomus";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            Player player = Main.LocalPlayer;

            if (awaitingNomuConfirm)
            {
                if (firstButton) 
                {
                 
                    int cost = Item.buyPrice(0, 50, 0, 0); 
                    
                 
                    if (player.BuyItem(cost)) 
                    {
                        Main.npcChatText = "Excellent! Here he comes!";
                        
                        
                        int spawnX = (int)player.Center.X + (player.direction * 300);
                        int spawnY = (int)player.Center.Y - 20;
                        NPC.NewNPC(NPC.GetSource_FromAI(), spawnX, spawnY, ModContent.NPCType<NomuNPC>());
                    }
                    else
                    {
                        Main.npcChatText = "Science needs money! You do not have 50 gold.";
                    }
                }
                else 
                {
                    Main.npcChatText = "Science needs money...";
                }

               
                awaitingNomuConfirm = false; 
            }
            else
            {
                if (firstButton)
                {
                    shopName = "QuirkShop";
                }
                else 
                {
                    awaitingNomuConfirm = true;
                    Main.npcChatText = "Deploying a Nomu requires resources. It will cost you 50 Gold Coins. Proceed?";
                }
            }
        }

        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, "QuirkShop");
            
            npcShop.Add(ModContent.ItemType<QuirkSyringe>());
            npcShop.Add(ModContent.ItemType<QuirkGene>());
            npcShop.Add(ModContent.ItemType<QuirkRemover>());
            npcShop.Add(ModContent.ItemType<GeneActivator>());
            npcShop.Add(ModContent.ItemType<SummonHim>());
            
            npcShop.Register();
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<RivetStabProj>(); 
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}