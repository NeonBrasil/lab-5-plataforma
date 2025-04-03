using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controle : MonoBehaviour
{
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    public float friction = 0.9f;
    public float jumpForce = 12f;
    public float deathJumpForce = 10f; // Força para subir ao morrer
    public float deathGravityScale = 0.5f; // Gravidade reduzida na queda

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float idleTime = 0f;
    private bool saiuDoIdle = false;
    private bool isDead = false; // Controle de morte
    private Collider2D colisor; // Referência ao Collider do jogador
    public GameObject filhoDoSonic; // Arraste o objeto filho do Sonic no Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colisor = GetComponent<Collider2D>(); // Pega o Collider

        if (filhoDoSonic == null)
        {
            filhoDoSonic = transform.GetChild(0).gameObject; // Pega o primeiro filho (se aplicável)
        }
    }

    void Update()
    {
        if (isDead) return; // Bloqueia o movimento após morrer

        float moveInput = Input.GetAxisRaw("Horizontal");
        Move(moveInput);
        HandleAnimations(moveInput);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Game_Manager.instance.lives = 5;
            Game_Manager.instance.RestartLevel();
        }
    }

    void Move(float direction)
    {
        if (direction != 0)
        {
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x + direction * acceleration * Time.deltaTime, -maxSpeed, maxSpeed), rb.linearVelocity.y);
            transform.localScale = new Vector3(Mathf.Sign(direction) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * friction, rb.linearVelocity.y);
        }
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
        animator.SetTrigger("Pulando");
        animator.SetBool("Parado", false);
        animator.SetBool("Andando", false);
        animator.SetBool("Correndo", false);

        if (animator.GetBool("Esperando"))
        {
            saiuDoIdle = true;
        }
        animator.SetBool("Esperando", false);
    }

    void HandleAnimations(float moveInput)
    {
        if (!isGrounded || isDead) return; // Se estiver no ar ou morto, não troca animação

        if (moveInput != 0)
        {
            idleTime = 0f;
            saiuDoIdle = false;

            if (Mathf.Abs(rb.linearVelocity.x) >= maxSpeed - 0.1f)
            {
                animator.SetBool("Correndo", true);
                animator.SetBool("Andando", false);
                animator.SetBool("Parado", false);
            }
            else
            {
                animator.SetBool("Correndo", false);
                animator.SetBool("Andando", true);
                animator.SetBool("Parado", false);
            }
        }
        else
        {
            animator.SetBool("Correndo", false);
            animator.SetBool("Andando", false);

            idleTime += Time.deltaTime;
            if (idleTime >= 5f)
            {
                animator.SetBool("Esperando", true);
                animator.SetBool("Parado", false);
            }
            else
            {
                animator.SetBool("Esperando", false);
                animator.SetBool("Parado", true);
            }
        }
    }

    public void Morrer()
    {
        if (animator.GetBool("Morrendo")) return; // Evita chamar várias vezes

        animator.SetBool("Morrendo", true); // Ativa a animação de morte
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse); // Faz o Sonic subir um pouco

        colisor.enabled = false;
        this.enabled = false; // Desativa o controle do jogador

        if (filhoDoSonic != null)
        {
            filhoDoSonic.SetActive(false); 
        }

        Invoke("FinalizarMorte", 1f); // Aguarda 1s antes de reiniciar
    }

    void FinalizarMorte()
    {
        Game_Manager.instance.PlayerDied();

        colisor.enabled = true; 

        if (filhoDoSonic != null)
        {
            filhoDoSonic.SetActive(true); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.ResetTrigger("Pulando");
            animator.SetBool("Parado", true);

            if (saiuDoIdle)
            {
                idleTime = 0f;
                saiuDoIdle = false;
            }
        }
    }
}
