using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    public int lives = 5; // Número inicial de vidas
    public int pontos = 0; // Armazena os pontos do jogador
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o GameManager entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied()
    {
        lives--;
        pontos = 0; // Reseta os pontos ao reiniciar a fase

        if (lives < 0)
        {
            GameOver();
        }
        else
        {
            RestartLevel();
        }
    }
    public void AdicionarPontos(int quantidade)
    {
        pontos += quantidade;
    }

    public void RestartLevel()
    {
        pontos = 0; // Reseta os pontos ao reiniciar a fase
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Tela De Morte");  // Nome da cena de game over
    }
}
