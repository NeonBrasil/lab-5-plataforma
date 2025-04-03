using UnityEngine;

public class Parallax_Controler : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distancia;

    GameObject[] backgronds;
    Material[] mat;
    float[] backspeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float Parallax_Velocidade;
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backspeed = new float[backCount];
        backgronds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgronds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgronds[i].GetComponent<Renderer>().material;
        }
        backspeedCalculate(backCount);
    }

    void backspeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgronds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgronds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backspeed[i] = 1 - (backgronds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        distancia = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < backgronds.Length; i++)
        {
            float velocidade = backspeed[i] * Parallax_Velocidade;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distancia, 0) * velocidade);
        }
    }
}