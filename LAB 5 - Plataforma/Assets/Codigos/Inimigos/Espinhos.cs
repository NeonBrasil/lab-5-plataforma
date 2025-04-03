using UnityEngine;

public class Espinhos : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Controle player = collision.GetComponent<Player_Controle>();

            if (player != null)
            {
                player.Morrer(); // Chama a animação e lógica de morte
            }
        }
    }
}
