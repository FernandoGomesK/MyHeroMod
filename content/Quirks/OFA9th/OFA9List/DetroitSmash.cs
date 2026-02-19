 private void DoDetroitSmash(TransformationPlayer mainPlayer)
        {
            int MaxDamage = 450;
            float DamageMultiplier = 1f;
            bool hurtPlayer = false;
            bool usedFaJin = false;

            switch (mainPlayer.ActiveForm)
            {
                case QuirkSkills.OneForAllFullCowling5:
                    DamageMultiplier = 0.05f;
                    hurtPlayer = false;
                    break;
                case QuirkSkills.OneForAllFullCowling8:
                    DamageMultiplier = 0.08f;
                    break;
                case QuirkSkills.OneForAllFullCowling45:
                    DamageMultiplier = 0.45f;
                    break;
                default:
                    DamageMultiplier = 1f;
                    hurtPlayer = true;
                    break;
            }
            if (FaJinStored)
            {
                DamageMultiplier += 0.55f; // Increase damage by 25% if Fa Jin is stored
                FaJinCharges = 0; // Consume all Fa Jin charges
                FaJinStored = false;
                Player.ClearBuff(ModContent.BuffType<FaJinBuff>());
                usedFaJin = true;
            }

            int FinalDamage = (int)(MaxDamage * DamageMultiplier);

            string attackName = "";

            
            if (usedFaJin)
            {
                attackName += "Faux ";
            }
            if (usedFaJin || !hurtPlayer)
            {
                attackName += (DamageMultiplier * 100).ToString("0") + "% Detroit Smash";
            }
            else
            {
                attackName += "Detroit Smash";
            }
            if (isGearshiftActive)
            {
                attackName += ": Quintuple";
            }
            else if (!usedFaJin || hurtPlayer)
            {
                attackName += "!";
            }
            
            CombatText.NewText(Player.getRect(), Color.LimeGreen, attackName);

            

            
            Vector2 Direction = Main.MouseWorld - Player.Center;
            Direction.Normalize();
            Vector2 Velocity = Direction * 15f;

            Vector2 BaseSpawnLocation = Player.Center + (Direction * 90f);

            
            
            int numberOfPunches = isGearshiftActive ? 5 : 1; // 5 hits if Gearshift is active, else 1

            for (int i = 0; i < numberOfPunches; i++)
            {
                Vector2 spacing = Direction * (25f * i);
                Vector2 currentSpawn = BaseSpawnLocation - spacing;
                
    
                Projectile.NewProjectile(
                    Player.GetSource_FromThis(), 
                    currentSpawn, 
                    Velocity, // Use the new speed with spread
                    ModContent.ProjectileType<DetroitSmashProj>(), 
                    FinalDamage, 
                    2f, 
                    Player.whoAmI
                );
                Projectile.NewProjectile(
                Player.GetSource_FromThis(), 
                BaseSpawnLocation, 
                Velocity, // Use the new speed with spread
                ModContent.ProjectileType<PunchAttackProj>(), 
                0,
                0f, 
                Player.whoAmI
            );
 
            
            }
            if (hurtPlayer)
            {
                Player.statLife -= 10;
                if (Player.statLife <= 0)
                {
                    var reason = PlayerDeathReason.ByCustomReason(
                        Terraria.Localization.NetworkText.FromKey("Mods.MyHeroMod.DeathMessage", Player.name));
                    Player.KillMe(reason, FinalDamage, 0);
                }
        }
        }