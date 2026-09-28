using KhacesCore.Content.Buffs;
using MyHeroMod.content.Buffs;
using MyHeroMod.content.Quirks.Flight;
using MyHeroMod.content.System;
using MyHeroMod.content.System.Interfaces;
using System;
using Terraria;
using Terraria.ModLoader;


namespace MyHeroMod.content.Quirks.Hardening
{
     public partial class HardeningPlayer : ModPlayer, IQuirkResetter
    {

        public bool isHardeningOn = false;
        public bool isUnbreakableOn = false;

        public int hardeningMaxHealth = 100;
        public float hardeningHealth = 0;
        public int timeSinceLastHit = 0;    
        public void FullReset()
        {
            isHardeningOn = false;
            isUnbreakableOn = false;

            
            hardeningHealth = 0f; 
            Player.ClearBuff(ModContent.BuffType<HardenBuff>());
            Player.ClearBuff(ModContent.BuffType<UnbreakableBuff>());
        }

        public override void PreUpdate()
        {
            isHardeningOn = false;
            isUnbreakableOn = false;
        }

        public override void FrameEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<Buffs.UnbreakableBuff>()))
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "UHardeningHead", EquipType.Head);
                Player.front = EquipLoader.GetEquipSlot(Mod, "UHardeningBody", EquipType.Front);
                Player.handon = EquipLoader.GetEquipSlot(Mod, "UHardeningArms", EquipType.HandsOn);
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "UHardeningArms", EquipType.HandsOff);
            }
            else if (Player.HasBuff(ModContent.BuffType<Buffs.HardenBuff>()))
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "HardeningHead", EquipType.Head);
                Player.front = EquipLoader.GetEquipSlot(Mod, "HardeningBody", EquipType.Front);
                Player.handon = EquipLoader.GetEquipSlot(Mod, "HardeningArms", EquipType.HandsOn);
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "HardeningArms", EquipType.HandsOff);
            }
        }

        public override void PostUpdateEquips()
        {

            if (Player.HasBuff<HardenBuff>())
            {
                 
                var transPlayer = Player.GetModPlayer<TransformationPlayer>();
                var hardeningBonusHealth = 0;


                var hardeningBaseHealth = transPlayer.CurrentStage switch
                {
                    QuirkStage.Initial => 300,
                    QuirkStage.Adequation => 450,
                    QuirkStage.Intermediate => 600,
                    QuirkStage.Advanced => 750,
                    QuirkStage.Final => 850,
                    _ => 20
                };


                if (transPlayer.Nature == NatureType.KinecticAbsorber)
                {
                    hardeningBonusHealth = 250;
                }

                hardeningMaxHealth = hardeningBaseHealth + hardeningBonusHealth;
            }
            else
            {

                hardeningMaxHealth = 0;
                hardeningHealth = 0f;
            }
        }

        public override void PostUpdate()
        {


            timeSinceLastHit++;
            if (timeSinceLastHit > 350)
            {
                timeSinceLastHit = 350;
            }

            if (isHardeningOn && hardeningHealth < hardeningMaxHealth)
            {

                if (timeSinceLastHit > 300)
                {

                    hardeningHealth += 0.5f;

                    if (hardeningHealth > hardeningMaxHealth)
                    {
                        hardeningHealth = hardeningMaxHealth;
                    }
                }
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (isUnbreakableOn && hardeningHealth > 0)
            {
                modifiers.ModifyHurtInfo += (ref Player.HurtInfo info) =>
                {
                    int effectiveShield = (int)(hardeningHealth * 2);

                    int damageBlocked = Math.Min(effectiveShield, info.Damage);

                    info.Damage -= damageBlocked;
                    hardeningHealth -= (damageBlocked / 2f);

                    timeSinceLastHit = 0;

                    if (info.Damage <= 0)
                    {
                        info.Damage = 0;
                    }
                };
            }
            else if (isHardeningOn && hardeningHealth > 0)
            {
                modifiers.ModifyHurtInfo += (ref Player.HurtInfo info) =>
                {
                    int damageToAbsorb = Math.Min((int)hardeningHealth, info.Damage);

                    info.Damage -= damageToAbsorb;
                    hardeningHealth -= damageToAbsorb;

                    timeSinceLastHit = 0;

                    if (info.Damage <= 0)
                    {
                        info.Damage = 0;
                    }
                };
            }
        }


        public override void OnHurt(Player.HurtInfo info)
        {

            timeSinceLastHit = 0;
        }
    }
    
}
