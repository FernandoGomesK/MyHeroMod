using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using MyHeroMod.content.System.BasePlayer;
using MyHeroMod.content.Quirks.OFA9th.Projectiles;
using MyHeroMod.content.Buffs;

namespace MyHeroMod.content.System
{
    public class PunchSkill : QuirkSkill
    {
        public override string Name => "Punch";
        public override string Description => "Throw a stronger punch. Various quirks can upgrade it!";
        public override string IconPath => "MyHeroMod/Assets/Skills/Dash";
        public override int BaseCooldown => 120;

        
       
        public override QuirkType RequiredQuirk => QuirkType.Quirkless;
        public override QuirkStage RequiredStage => QuirkStage.Initial;
        
        public override bool IsDefaultSkill => true;
        public override bool IsBaseQuirk => false;
        

        public override bool CheckUnlock(TransformationPlayer player)
        {
            return true; 
        }

        public override void OnUse(Player player)
        {
            int baseDamage = 20;
            float projSpeed = 15f;
            bool isSuperPunch = false;
            int numberOfPunches = 1;

            foreach (var modPlayer in player.ModPlayers)
            {
                if (modPlayer is IHeroPunchModifier punchModifier) 
                {
                    punchModifier.ModifyPunch(ref projSpeed, ref baseDamage, ref isSuperPunch, ref numberOfPunches);
        }
            }

            


            Vector2 Direction = Main.MouseWorld - player.Center;
            Direction.Normalize();
            Vector2 Velocity = Direction * projSpeed;

            Vector2 BaseSpawnLocation = player.Center + (Direction * 90f);

        
        
            
        for (int i = 0; i < numberOfPunches; i++)
            {

                Vector2 spacing = Direction * (25f * i);
                Vector2 currentSpawn = BaseSpawnLocation - spacing;

            Projectile.NewProjectile(
                player.GetSource_FromThis(),
                currentSpawn,
                Velocity,
                ModContent.ProjectileType<PunchAttackProj>(),
                baseDamage, 
                2f, 
                player.whoAmI);

                if (isSuperPunch == true)
            { 
                Projectile.NewProjectile(
                    player.GetSource_FromThis(), 
                    player.Center, 
                    Velocity, 
                    ModContent.ProjectileType<DetroitSmashProj>(), 
                    20, 
                    2f, 
                    player.whoAmI
                );
            }
            }
        }
}
}