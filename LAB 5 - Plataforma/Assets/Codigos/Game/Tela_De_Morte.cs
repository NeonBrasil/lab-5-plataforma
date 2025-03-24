using UnityEngine;
using UnityEngine.SceneManagement;

public class Tela_De_Morte : MonoBehaviour
{
    public void RestartGame()
    {
        Game_Manager.instance.lives = 5;
        SceneManager.LoadScene("Fase 1");  // Nome da cena do jogo
    }

    public void QuitGame()
    {
        Application.Quit(); // Fecha o jogo
        Debug.Log("Jogo Fechou");
    }
}
