// using Terraria;
// using Terraria.ModLoader;
// using Terraria.ID;
// using Terraria.DataStructures;
// using Microsoft.Xna.Framework;
// using Humanizer;
// using MyHeroMod.content.Dusts;

// namespace MyHeroMod.content.Quirks.Explosion
// {
//     public partial class ExplosionPlayer : ModPlayer
//     {
//         public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
//         {
            

//             if (IsClusterActive)
// {
//     // Efeitos visuais no corpo (Armadura laranja)
    

//     // Luz ao redor
//     Lighting.AddLight(Player.Center, Color.Orange.ToVector3() * 0.8f);

//     // CRIAÇÃO DA PARTÍCULA (DUST)
//     // NextBool(5) significa 20% de chance por frame (para não ficar pesado)
//     Vector2 randomPos = Player.Center + Main.rand.NextVector2Circular(20f, 20f);
//     if (Main.rand.NextBool(25)) 
//     {
//         // Dust.NewDust retorna o índice da partícula criada na lista Main.dust
//         int dust = Dust.NewDust(
//         randomPos, 
//         0, 0, // Largura/Altura 0 porque já calculamos a posição exata acima
//         ModContent.DustType<ClusterDust>(),
//         0f, 0f, 
//         0, default, 1.5f
//     );
//         Main.dust[dust].noGravity = true;
//         Main.dust[dust].velocity = Player.velocity;        
                
//             }
                
//         }
//     }
//     }}