using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using MyHeroMod.content.System; // Para acessar o QuirkType
using Terraria.DataStructures;

namespace MyHeroMod.content.System
{
    public class QuirkGlobalNPC : GlobalNPC
    {
        
        public override bool InstancePerEntity => true;

        public bool IsQuirkErased = false;
        public bool HasQuirk = false;
        public QuirkType AssignedQuirk = QuirkType.Quirkless;

        
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            
            if (npc.friendly || npc.townNPC || npc.lifeMax < 10)
                return;

            
            // Ex: NextBool(5) = 1 em 5 = 20%
            if (Main.rand.NextBool(5)) 
            {
                HasQuirk = true;

                
                
                int random = Main.rand.Next(4); 
                switch (random)
                {
                    // case 0: AssignedQuirk = QuirkType.HellFlames; break;
                    case 0: AssignedQuirk = QuirkType.HalfColdHalfHot; break;
                    // case 2: AssignedQuirk = QuirkType.BlueFlames; break;
                    case 1: AssignedQuirk = QuirkType.OneForAll9th; break;
                    case 2: AssignedQuirk = QuirkType.OneForAll8th; break;
                    case 3: AssignedQuirk = QuirkType.Overclock; break;
                    case 4: AssignedQuirk = QuirkType.Float; break;
                    case 5: AssignedQuirk = QuirkType.DangerSense; break;
                    case 6: AssignedQuirk = QuirkType.BlackWhip; break;
                    case 7: AssignedQuirk = QuirkType.Gearshift; break;
                    case 8: AssignedQuirk = QuirkType.FaJin; break;
                    case 9: AssignedQuirk = QuirkType.SmokeScreen; break;
                    case 10: AssignedQuirk = QuirkType.Explosion; break;

                }
                
                
                npc.lifeMax = (int)(npc.lifeMax * 4f); 
                npc.life = npc.lifeMax;
                npc.damage = (int)(npc.damage * 3f);
            }
        }

        public override void ResetEffects(NPC npc)
{
    IsQuirkErased = false;
}

        // 3. Efeitos Visuais (O Brilho!)
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (HasQuirk)
            {
                
                if (Main.rand.NextBool(3)) 
                {
                    int dustType = DustID.MagicMirror; 

                    // Podemos fazer a cor da poeira mudar dependendo da Quirk que ele tirou!
                    if (AssignedQuirk == QuirkType.HellFlames) dustType = DustID.Torch; 
                    if (AssignedQuirk == QuirkType.BlueFlames) dustType = DustID.BlueTorch; 
                    if (AssignedQuirk == QuirkType.HalfColdHalfHot) dustType = DustID.IceTorch; 
                    if (AssignedQuirk == QuirkType.OneForAll9th) dustType = DustID.GreenTorch;
                    if (AssignedQuirk == QuirkType.OneForAll8th) dustType = DustID.YellowTorch; 
                    if (AssignedQuirk == QuirkType.Overclock) dustType = DustID.YellowTorch;
                    if (AssignedQuirk == QuirkType.Float) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.DangerSense) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.BlackWhip) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.Gearshift) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.FaJin) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.SmokeScreen) dustType = DustID.WhiteTorch;
                    if (AssignedQuirk == QuirkType.Explosion) dustType = DustID.OrangeTorch;

                    
                    Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, dustType);
                    d.velocity *= 0.5f;
                    d.noGravity = true; 
                    d.scale = 1.2f; 
                }
            }
        }

        public override void OnKill(NPC npc)
        {
            if (HasQuirk && Main.rand.NextBool(2))
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Items.QuirkGene>());
            }
        }

        
        // public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        // {
        //     if (HasQuirk)
        //     {
        //         
        //         if (AssignedQuirk == QuirkType.HellFlames)
        //         {
        //             target.AddBuff(BuffID.OnFire, 180); 
        //         }
        //         else if (AssignedQuirk == QuirkType.HalfColdHalfHot)
        //         {
        //             target.AddBuff(BuffID.Frostburn, 180); 
        //         }
        //     }
        // }
    }
}