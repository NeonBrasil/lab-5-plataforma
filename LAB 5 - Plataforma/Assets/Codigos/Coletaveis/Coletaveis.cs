using UnityEngine;

public class Coletaveis : MonoBehaviour
{
    public int valor = 1; // Quantidade de pontos que o coletável dá

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o Player tocou no coletável
        if (collision.CompareTag("Player"))
        {
            Game_Manager.instance.AdicionarPontos(valor); // Adiciona pontos ao GameManager
            Destroy(gameObject); // Remove o coletável do jogo
        }
    }
}
