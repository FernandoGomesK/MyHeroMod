using Terraria;
using Terraria.ModLoader;
using MyHeroMod.content.System;
using MyHeroMod.content;
using MyHeroMod.content.Buffs;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using MyHeroMod.content.Quirks.Engine.Projectiles;
using MyHeroMod.content.Quirks.Engine;
using MyHeroMod.content.Quirks.Rabbit;
using MyHeroMod.content.Quirks.Rabbit.RabbitProjectiles;
using MyHeroMod.content.Quirks.Grasshoper;

public class GrasshoperKickSkill : QuirkBaseSkill
{
    public override string Name => "Grasshoper Kick";
    public override string Description => "Jump into the air while doing a diving kick";
    public override string IconPath
    {
        get
        {
            Player player = Main.LocalPlayer;
            var transPlayer = player.GetModPlayer<TransformationPlayer>();

            if (transPlayer.CurrentVariant == QuirkVariant.Variant1)
            {
                return "MyHeroMod/Assets/SkillIcons/Grasshoper/ShinKickIcon";
            }
            if (transPlayer.CurrentVariant == QuirkVariant.Variant2)
            {
                return "MyHeroMod/Assets/SkillIcons/Grasshoper/IchigoKickIcon";
            }
            else
            {
                return "MyHeroMod/Assets/SkillIcons/Grasshoper/GrasshoperKickIcon";
            }

        }
    }
   
        
    public override string Category => "Grasshoper";

    public override int BaseCooldown => 120;

    public override QuirkType RequiredQuirk => QuirkType.Grasshoper;
    public override QuirkStage RequiredStage => QuirkStage.Initial;
    public override bool IsDefaultSkill => false;


    public override void OnUse(Player player)
    {
        var transPlayer = player.GetModPlayer<TransformationPlayer>();
        var grassPlayer = player.GetModPlayer<GrasshoperPlayer>();

        if (grassPlayer.IsGrasshoper == false)
        {
            CombatText.NewText(player.getRect(), Color.Red, "Needs to be transformed!");
            return;
        }


        int MaxDamage = transPlayer.CurrentStage switch
        {
            QuirkStage.Initial => 130,
            QuirkStage.Adequation => 350,
            QuirkStage.Intermediate => 500,
            QuirkStage.Advanced => 950,
            QuirkStage.Final => 2200,
            _ => 130
        };





        var extraDamage = 0;
        if (grassPlayer.IsIronSolesOn)
        {
            extraDamage = 50;
        }



        var DamageMultiplier = 1f;

        var attackName = "Grasshoper Kick!";





        int FinalDamage = (int)(MaxDamage * DamageMultiplier) + extraDamage;


        CombatText.NewText(player.getRect(), Color.Green, attackName);


        Projectile.NewProjectile(
            player.GetSource_FromThis(),
            player.Center,
            Vector2.Zero,
            ModContent.ProjectileType<LunaFallController>(),
            FinalDamage,
            10f,
            player.whoAmI
        );

        SoundEngine.PlaySound(new SoundStyle("MyHeroMod/Assets/Sounds/smash2") with { Volume = 0.5f }, player.position);
    }
}