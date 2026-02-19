private void DoDelawareSmash(TransformationPlayer mainPlayer)
        {
            int MaxDamage = 100;
            int FinalDamage = 0;
            bool consumeFinger = false;
            bool hurtPlayer = false;

            if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling5)
            {
                FinalDamage = (int)(MaxDamage * 0.05f);
                hurtPlayer = false;
                consumeFinger = false;
            }
            else if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling8)
            {
                FinalDamage = (int)(MaxDamage * 0.08f);
                hurtPlayer = false;
                consumeFinger = false;
            }
            else if (mainPlayer.ActiveForm == QuirkSkills.OneForAllFullCowling45)
            {
                FinalDamage = (int)(MaxDamage * 0.45f);
                hurtPlayer = false;
                consumeFinger = false;
            }
            else
            {
                FinalDamage = MaxDamage;
                hurtPlayer = true;
                consumeFinger = true;
            }
            if (consumeFinger && Fingers <= 0)
            {
                CombatText.NewText(Player.getRect(), Color.Red, "No fingers left!");
                return;
            }

            if (consumeFinger) Fingers--;

            Vector2 Velocity = Main.MouseWorld - Player.Center;
            Velocity.Normalize();
            Velocity *= 15f;

            if (isFloatActive)
            {
                float recoil = 2f;

                Player.velocity = -Velocity * recoil;

                for (int i = 0; i < 10; i++)
        {
            Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, Velocity.X * 2, Velocity.Y * 2, 0, default, 1f);
        }
            }

            

            Projectile.NewProjectile(
                Player.GetSource_FromThis(), 
                Player.Center, 
                Velocity, 
                ModContent.ProjectileType<DelawareSmashProj>(), 
                FinalDamage, 2f, 
                Player.whoAmI);

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