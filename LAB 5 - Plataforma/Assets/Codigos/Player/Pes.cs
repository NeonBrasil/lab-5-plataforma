using UnityEngine;

public class Pes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o FeetCollider tocar na cabeça do inimigo, ele morre
        if (collision.CompareTag("EnemyHead"))
        {
            Destroy(collision.transform.parent.gameObject); // Destroi o inimigo inteiro

            // Faz o Sonic pular novamente
            GetComponentInParent<Player_Controle>().Jump();
        }
    }
}
