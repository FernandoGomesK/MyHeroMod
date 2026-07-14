using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.DarkShadow.Projectiles; 

namespace MyHeroMod.content.Quirks.DarkShadow
{
    public partial class DarkShadowPlayer : ModPlayer
    {
        public bool isDarkShadowOn = false;
        public bool isBlackAbyssOn = false;
        public bool isMediumDarkShadowOn = false;
        public bool isCBOArmsOn = false;

        // Variável nova para ativar o modo descontrolado
        public bool isUncontrolledMode = false; 

        public int DarkShadowRange => isMediumDarkShadowOn ? 800 : 600; 

        public int AutomaticAttackTimer = 0;
        public int AutomaticAttackCooldown = 60; // 60 frames = 1 segundo
        
        public bool isFrontHandAttacking => Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowLongFrontHandProj>()] > 0;
        public bool isBackHandAttacking => Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowLongBackHandProj>()] > 0;
        
        public override void ResetEffects()
        {
            isDarkShadowOn = false;      
            isBlackAbyssOn = false;
            isCBOArmsOn = false;
            isMediumDarkShadowOn = false;
            isUncontrolledMode = false; // Reseta todo frame
        }

        public override void FrameEffects()
        {
            if (Player.HasBuff(ModContent.BuffType<Buffs.BlackAbyssBuff>()))
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "AbyssHead", EquipType.Head);
                Player.handon = EquipLoader.GetEquipSlot(Mod, "AbyssArms", EquipType.HandsOn);
                Player.handoff = EquipLoader.GetEquipSlot(Mod, "AbyssArms", EquipType.HandsOff);
            }
        }

        public override void PostUpdate()
        {
            var transPlayer = Player.GetModPlayer<TransformationPlayer>();
            if (!Main.dayTime)
            {
                isMediumDarkShadowOn = true;
                
                if (transPlayer.CurrentStage == QuirkStage.Initial || transPlayer.CurrentStage == QuirkStage.Intermediate)
                {
                    isDarkShadowOn = true;
                }
                {
                    isUncontrolledMode = true;
                }
            }

            if (isDarkShadowOn && !isBlackAbyssOn)
            {
                // LÓGICA DE SPAWN DOS CORPOS BASE (Inalterada)
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowBodyProj>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<DarkShadowBodyProj>(), 0, 0f, Player.whoAmI);
                }  
                
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowFrontHandProj>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<DarkShadowFrontHandProj>(), 10, 0f, Player.whoAmI);
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<DarkShadowBackHandProj>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<DarkShadowBackHandProj>(), 10, 0f, Player.whoAmI);
                }

                // =======================================================
                // LÓGICA DO ATAQUE AUTOMÁTICO (RAMPAGE)
                // =======================================================
                if (isUncontrolledMode) 
                {
                    HandleAutomaticAttacks();
                }
            }

            // SPAWN DOS BRAÇOS GRANDES (Inalterado)
            if (isCBOArmsOn)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<BigDarkShadowBackHandProj>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<BigDarkShadowBackHandProj>(), 10, 0f, Player.whoAmI);
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<BigDarkShadowFrontHandProj>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<BigDarkShadowFrontHandProj>(), 10, 0f, Player.whoAmI);
                }
            }
        }

        // Método exclusivo para gerenciar a mira e os disparos automáticos
        private void HandleAutomaticAttacks()
        {
            // Se as duas mãos já estão no meio de um ataque, não faz nada
            if (isFrontHandAttacking && isBackHandAttacking) return;

            NPC closestNPC = null;
            float minDistance = DarkShadowRange;

            // 1. Encontra o inimigo válido mais próximo
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.lifeMax > 5 && !npc.dontTakeDamage)
                {
                    float distanceToNpc = Player.Distance(npc.Center);
                    
                    if (distanceToNpc < minDistance)
                    {
                        // Verifica colisão: garante que o Dark Shadow não tente atacar através de paredes sólidas
                        if (Collision.CanHitLine(Player.Center, 1, 1, npc.Center, 1, 1))
                        {
                            minDistance = distanceToNpc;
                            closestNPC = npc;
                        }
                    }
                }
            }

            // 2. Se encontrou um alvo, inicia o cronômetro
            if (closestNPC != null)
            {
                AutomaticAttackTimer++;

                if (AutomaticAttackTimer >= AutomaticAttackCooldown)
                {
                    // Calcula a direção e a velocidade (Ex: 15f pixels por frame de velocidade inicial)
                    Vector2 attackDirection = (closestNPC.Center - Player.Center).SafeNormalize(Vector2.Zero);
                    float shootSpeed = 15f; 
                    Vector2 shootVelocity = attackDirection * shootSpeed;

                    int damage = isMediumDarkShadowOn ? 80 : 40; // Dá mais dano se estiver médio/descontrolado
                    float knockback = 5f;

                    // 3. Atira! Prioriza a mão da frente. Se ela estiver ocupada, usa a de trás.
                    if (!isFrontHandAttacking)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, shootVelocity, ModContent.ProjectileType<DarkShadowLongFrontHandProj>(), damage, knockback, Player.whoAmI);
                    }
                    else if (!isBackHandAttacking)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, shootVelocity, ModContent.ProjectileType<DarkShadowLongBackHandProj>(), damage, knockback, Player.whoAmI);
                    }

                    // Reseta o timer para o próximo ataque
                    AutomaticAttackTimer = 0;
                }
            }
            else
            {
                
                if (AutomaticAttackTimer > 0) AutomaticAttackTimer--;
            }
        }
    }
}