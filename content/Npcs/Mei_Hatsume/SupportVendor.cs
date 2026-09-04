using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using MyHeroMod.content.Items.Support;
using MyHeroMod.content.Items.Support.UrarakaSupport.ZeroBetaBoots;

namespace MyHeroMod.content.Npcs.Mei_Hatsume
{
    
    [AutoloadHead]
    public class SupportVendor : ModNPC
    {
        public override void SetStaticDefaults()
        {
            
            
            Main.npcFrameCount[NPC.type] = 23; 
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

            AnimationType = NPCID.BestiaryGirl; 
        }

        
        public override List<string> SetNPCNameList()
        {
            return new List<string>() { "Mei Hatsume"};
        }

        
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active) continue;
      
                return true; 
            }
            return false;
        }

        
        public override string GetChat()
        {
            int dialog = Main.rand.Next(3); 
            if (dialog == 0) return "If my idea is good, then you will use it, right?";
            if (dialog == 1) return "Failure is the mother of invention!";
            return "I've forgotten all of your names!";
        }

        
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Support Shop"; 
        }

        // O que acontece quando clica no botão
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "SupportShop";
            }
        }

        
        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, "SupportShop");
            
            
            npcShop.Add(ModContent.ItemType<AirForce>());
            npcShop.Add(ModContent.ItemType<IronSoles>());
            npcShop.Add(ModContent.ItemType<MidGauntlets>());
            npcShop.Add(ModContent.ItemType<ZeroBetaBoots>());
            npcShop.Add(ModContent.ItemType<MidGauntlets>());

            npcShop.Add(ItemID.IronBar); 
            
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
            projType = ProjectileID.SpikyBall;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}