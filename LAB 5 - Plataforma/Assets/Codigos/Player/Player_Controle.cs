using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controle : MonoBehaviour
{
    public float acceleration = 20f;  // Aceleração do personagem
    public float maxSpeed = 10f;      // Velocidade máxima
    public float friction = 0.9f;     // Atrito quando não está se movendo
    public float jumpForce = 12f;     // Força do pulo

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Move(moveInput);

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
