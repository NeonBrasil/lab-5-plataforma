using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private TextMeshProUGUI livesText;
    private TextMeshProUGUI pontosText;

    void Start()
    {
        // Buscar o LivesText quando a cena iniciar
        livesText = GameObject.Find("VidasText").GetComponent<TextMeshProUGUI>();
        pontosText = GameObject.Find("PontosText").GetComponent<TextMeshProUGUI>();

        // Inscrever-se para ser notificado quando a cena for carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // A fun��o OnSceneLoaded ser� chamada sempre que uma cena for carregada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-atualizar o LivesText sempre que uma nova cena for carregada
        livesText = GameObject.Find("VidasText").GetComponent<TextMeshProUGUI>();
        pontosText = GameObject.Find("PontosText").GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate()
    {
        if (livesText != null)
        {
            livesText.text = Game_Manager.instance.lives.ToString();
        }

        if (pontosText != null)
        {
            pontosText.text = Game_Manager.instance.pontos.ToString(); // Mostrar pontos corretamente
        }

        // Se livesText ou pontosText forem nulos, exiba um log
        if (livesText == null)
        {
            Debug.Log("LivesText n�o encontrado.");
        }

        if (pontosText == null)
        {
            Debug.Log("PontosText n�o encontrado.");
        }
    }

    // Desinscrever-se da fun��o quando o objeto for destru�do (para evitar vazamentos de mem�ria)
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
