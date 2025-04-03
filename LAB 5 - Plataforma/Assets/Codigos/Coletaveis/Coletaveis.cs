using UnityEngine;
using UnityEngine.SceneManagement; // Importa SceneManager para carregar fases

public class Coletaveis : MonoBehaviour
{
    public int valor = 1; // Quantidade de pontos que o colet�vel d�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o Player tocou no colet�vel
        if (collision.CompareTag("Player"))
        {
            Game_Manager.instance.AdicionarPontos(valor); // Adiciona pontos ao GameManager
            Destroy(gameObject); // Remove o colet�vel do jogo

            // Verifica se o jogador coletou 7 itens
            if (Game_Manager.instance.pontos >= 7)
            {
                SceneManager.LoadScene("Fase 2"); // Carrega a nova fase
            }
        }
    }
}
