using UnityEngine;

public class Joaninha : MonoBehaviour
{
    public bool perseguir = false; // Define se o inimigo pode perseguir o player
    public float raioDeAlcance = 5f; // Raio de detec��o do player
    public float velocidade = 2f; // Velocidade do inimigo
    public Transform pontoA, pontoB; // Pontos de patrulha

    private Transform player;
    private bool seguindoPlayer = false; // Controla se o inimigo j� iniciou a persegui��o
    private Transform alvoAtual; // Define o destino atual do inimigo
    private Animator animator; // Refer�ncia ao Animator
    private bool olhandoParaDireita = true; // Controla a dire��o do sprite

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        alvoAtual = pontoA; // Come�a indo para o ponto A
        animator = GetComponent<Animator>(); // Obt�m o Animator
    }

    void Update()
    {
        if (perseguir)
        {
            float distanciaDoPlayer = Vector2.Distance(transform.position, player.position);

            if (distanciaDoPlayer <= raioDeAlcance)
            {
                seguindoPlayer = true; // Come�a a persegui��o quando o player entra no raio
                animator.SetBool("Perseguindo", true); // Ativa anima��o de persegui��o
            }
        }

        if (seguindoPlayer)
        {
            SeguirPlayer();
        }
        else
        {
            Patrulhar(); // Mant�m a patrulha at� que o player entre no raio
        }
    }

    void SeguirPlayer()
    {
        Vector2 direcao = (player.position - transform.position).normalized;
        transform.position += (Vector3)direcao * velocidade * Time.deltaTime;

        VerificarVirarSprite(direcao.x);
    }

    void Patrulhar()
    {
        Vector2 direcao = (alvoAtual.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, alvoAtual.position, velocidade * Time.deltaTime);

        VerificarVirarSprite(direcao.x);

        if (Vector2.Distance(transform.position, alvoAtual.position) < 0.1f)
        {
            alvoAtual = (alvoAtual == pontoA) ? pontoB : pontoA; // Alterna entre os pontos de patrulha
        }
    }

    void VerificarVirarSprite(float direcaoX)
    {
        if ((direcaoX < 0 && !olhandoParaDireita) || (direcaoX > 0 && olhandoParaDireita))
        {
            Flip();
        }
    }

    void Flip()
    {
        olhandoParaDireita = !olhandoParaDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1; // Inverte o eixo X para virar o sprite e os filhos
        transform.localScale = escala;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Obt�m o script do jogador e chama a anima��o de morte
            Player_Controle player = collision.GetComponent<Player_Controle>();

            if (player != null)
            {
                player.Morrer(); // Chama a anima��o de morte antes de reiniciar o jogo
            }

            // Opcional: Desativa o movimento do inimigo para evitar problemas
            this.enabled = false;
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
