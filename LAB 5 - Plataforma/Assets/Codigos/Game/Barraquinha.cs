using UnityEngine;
using UnityEngine.SceneManagement;

public class Barraquinha : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Fim");
        }
    }
}
