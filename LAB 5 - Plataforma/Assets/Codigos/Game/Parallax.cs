using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material mat;
    public float distancia;

    [Range(0f,0.5f)]
    public float velocidade=0.2f;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        distancia += Time.deltaTime * velocidade;
        mat.SetTextureOffset("_Maintext", Vector2.right * distancia);
    }
}
