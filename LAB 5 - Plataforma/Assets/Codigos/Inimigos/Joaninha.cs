using UnityEngine;

public class Joaninha : MonoBehaviour
{
    public bool perseguir = false; // Define se o inimigo pode perseguir o player
    public float raioDeAlcance = 5f; // Raio de detecção do player
    public float velocidade = 2f; // Velocidade do inimigo
    public Transform pontoA, pontoB; // Pontos de patrulha

    private Transform player;
    private bool seguindoPlayer = false; // Controla se o inimigo já iniciou a perseguição
    private Transform alvoAtual; // Define o destino atual do inimigo

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        alvoAtual = pontoA; // Começa indo para o ponto A
    }

    void Update()
    {
        if (perseguir)
        {
            float distanciaDoPlayer = Vector2.Distance(transform.position, player.position);

            if (distanciaDoPlayer <= raioDeAlcance)
            {
                seguindoPlayer = true; // Começa a perseguição quando o player entra no raio
            }
        }

        if (seguindoPlayer)
        {
            SeguirPlayer();
        }
        else
        {
            Patrulhar(); // Mantém a patrulha até que o player entre no raio
        }
    }

    void SeguirPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, velocidade * Time.deltaTime);
    }

    void Patrulhar()
    {
        transform.position = Vector2.MoveTowards(transform.position, alvoAtual.position, velocidade * Time.deltaTime);

        if (Vector2.Distance(transform.position, alvoAtual.position) < 0.1f)
        {
            alvoAtual = (alvoAtual == pontoA) ? pontoB : pontoA; // Alterna entre os pontos de patrulha
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Game_Manager.instance.PlayerDied();
        }
    }

    void OnDrawGizmos()
    {
        if (perseguir)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, raioDeAlcance);
        }

        if (pontoA != null && pontoB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pontoA.position, pontoB.position);
        }
    }
}
