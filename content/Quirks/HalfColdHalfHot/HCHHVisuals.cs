using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;

namespace MyHeroMod.content.Quirks.HalfColdHalfHot
{
    public partial class HalfColdHalfHotPlayer : ModPlayer
    {
        // MODIFY DRAW INFO: Usado para mudar a cor do SPRITE (Armadura/Pele)
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (IsFlashFireFistActive)
            {
                // Deixa o personagem incandescente (Laranja)
                drawInfo.colorArmorBody = Color.OrangeRed;
                drawInfo.colorArmorHead = Color.OrangeRed;
                drawInfo.colorArmorLegs = Color.OrangeRed;
            }
        }

        // DRAW EFFECTS: O lugar perfeito para spawnar PARTÍCULAS (Dust) e LUZES
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            // --- FLASHFIRE FIST (FOGO NA MÃO) ---
            if (IsFlashFireFistActive)
            {
                // Add a fiery glow effect to the player when Flash Fire Fist is active
                drawInfo.colorArmorBody = Color.OrangeRed;
                drawInfo.colorArmorHead = Color.OrangeRed;
                drawInfo.colorArmorLegs = Color.OrangeRed;

                // Create a light effect around the player
                Lighting.AddLight(Player.Center, Color.OrangeRed.ToVector3() * 0.8f);
                
                    int fire = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
                    Main.dust[fire].noGravity = true;
                    Main.dust[fire].velocity *= 3f;
                    Main.dust[fire].velocity += Player.velocity * 0.5f;
                
                // Main.dust[Player.whoAmI].noGravity = true;
                // if (Main.rand.NextBool(3))
                // {
                //     Dust.NewDust(Player.position, Player.width, Player.height, DustID.Fire, 0f, 0f, 100, default, 1.5f);
                // }
                
            }

            // --- PHOSPHOR (X NO PEITO) ---
            if (IsPhosphorActive)
            {
                DrawPhosphorFire();
            }
        }

        // --- MÉTODOS AUXILIARES ATUALIZADOS ---

        private void DrawPhosphorFire()
        {
            // 1. Tamanho e Posição
            float tamanhoX = 10f; // Bem menor
            // Move o centro para cima, na altura do peito/coração
            Vector2 chestCenter = Player.Center + new Vector2(0, 5f); 

            // 2. Densidade do Fogo
            // Roda o loop algumas vezes por frame para gerar várias chamas e preencher o X
            int densidade = 3; 
            for (int k = 0; k < densidade; k++)
            {
                // Escolhe um ponto aleatório ao longo da linha diagonal (de -1 a 1)
                // Isso faz o fogo "tremer" ao longo do X em vez de ser uma linha reta
                float progressoRandom = Main.rand.NextFloat(-1f, 1f);

                // --- Diagonal 1 (\) ---
                Vector2 pos1 = chestCenter + new Vector2(progressoRandom * tamanhoX, progressoRandom * tamanhoX);
                SpawnFireDust(pos1, chestCenter.X);

                // --- Diagonal 2 (/) ---
                Vector2 pos2 = chestCenter + new Vector2(progressoRandom * tamanhoX, -progressoRandom * tamanhoX);
                SpawnFireDust(pos2, chestCenter.X);
            }
        }

        private void SpawnFireDust(Vector2 position, float centerX)
        {
            // Cor baseada no lado (Verde na esquerda, Vermelho na direita)
            int dustID = (position.X < centerX) ? DustID.IceTorch : DustID.Torch;

            // Cria a partícula em uma área pequena (4x4 pixels)
            // Usamos NewDust normal para ele ter um pouco de variação natural
            int d = Dust.NewDust(position - new Vector2(2,2), 4, 4, dustID, 0, 0, 100, default, 1.3f);
            
            Main.dust[d].noGravity = true; // Fogo flutua

            // --- COMPORTAMENTO DE FOGO ---
            // Velocidade para cima (Y negativo) com um pouco de variação horizontal
            Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-2f, -1f));

            // Importante: Faz o fogo acompanhar um pouco o movimento do player
            // para não ficar para trás quando você corre
            Main.dust[d].velocity += Player.velocity * 0.3f;
        }
    }
}