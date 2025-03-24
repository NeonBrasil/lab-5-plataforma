using UnityEngine;

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
        }
    }
}
