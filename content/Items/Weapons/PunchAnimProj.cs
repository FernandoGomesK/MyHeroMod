using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace MyHeroMod.content.Items.Weapons
{
    public class PunchAnimProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6; // 6 Frames de animação
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.tileCollide = false; // Não bate em parede
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 60; 
            Projectile.hide = true;
            
            // IMPORTANTE: Dano e Penetração para não atrapalhar
            Projectile.penetrate = -1; 
            // Mesmo com friendly=true, se foi criado com dano 0 no Shoot, não machuca
        }

       public override void AI()
{
    Player player = Main.player[Projectile.owner];

    if (player.dead || !player.active)
    {
        Projectile.Kill();
        return;
    }

    // --- 1. CONFIGURAÇÃO DA DIREÇÃO ---
    // Define o lado (Esquerda/Direita) baseado em onde você clicou (Velocity)
    // Isso ignora se o mouse está alto ou baixo, olha apenas o X.
    if (Projectile.velocity.X > 0)
    {
        player.ChangeDir(1); // Olha pra direita
    }
    else
    {
        player.ChangeDir(-1); // Olha pra esquerda
    }

    Projectile.direction = player.direction;
    Projectile.spriteDirection = player.direction;
    Projectile.rotation = 0f; // Trava a rotação (sempre reto)


    // --- 2. POSICIONAMENTO NO PEITO (O Segredo) ---
    
    // float distanceX = 15f; // Quão longe do corpo o soco sai (para frente)
    // float heightY = -5f;   // Altura do peito (Negativo SOBE, Positivo DESCE)
    
    // Ajuste o '-5f' se quiser mais alto ou mais baixo.
    Vector2 chestOffset = new Vector2(-15f * player.direction, -5f);
    
    // Cola o projétil no centro do player + o ajuste do peito
    Projectile.Center = player.Center + chestOffset;


    // --- 3. ANIMAÇÃO ---
    Projectile.frameCounter++;
    if (Projectile.frameCounter >= 2) 
    {
        Projectile.frameCounter = 0;
        Projectile.frame++;
        if (Projectile.frame >= 6)
        {
            Projectile.Kill();
        }
    }
}

public override bool PreDraw(ref Color lightColor)
{
    return true; 
}
public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
{
    // Adiciona este projétil à lista que é desenhada POR CIMA dos jogadores
    overPlayers.Add(index);
}

}}