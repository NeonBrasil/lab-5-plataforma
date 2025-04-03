using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private TextMeshProUGUI livesText;
    private TextMeshProUGUI pontosText;

    private int pontosNecessarios = 7; // Quantidade necessária para passar de fase

    void Start()
    {
        // Buscar os textos na UI
        livesText = GameObject.Find("VidasText").GetComponent<TextMeshProUGUI>();
        pontosText = GameObject.Find("PontosText").GetComponent<TextMeshProUGUI>();

        // Inscrever-se para atualizar os textos quando a cena for carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reatualizar os textos ao carregar uma nova cena
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
            pontosText.text = $"{Game_Manager.instance.pontos}/{pontosNecessarios}"; // Exibe "Pontos/PontosNecessários"
        }

        // Logs para debug caso os textos não sejam encontrados
        if (livesText == null)
        {
            Debug.Log("LivesText não encontrado.");
        }

        if (pontosText == null)
        {
            Debug.Log("PontosText não encontrado.");
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
