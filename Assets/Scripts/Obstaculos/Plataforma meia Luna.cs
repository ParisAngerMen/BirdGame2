using UnityEngine;

public class PlataformaMediaLuna : MonoBehaviour
{
    public float radio = 3f;
    public float velocidad = 1f;

    // Ángulo en grados (0 a 180 = media luna)
    public float anguloInicial = 0f;
    public float anguloFinal = 180f;

    private Vector3 centro;
    private float t;

    void Start()
    {
        centro = transform.position;
    }

    void Update()
    {
        t += Time.deltaTime * velocidad;

        // PingPong para ir y volver
        float angulo = Mathf.Lerp(anguloInicial, anguloFinal, Mathf.PingPong(t, 1f));
        float radianes = angulo * Mathf.Deg2Rad;

        float x = Mathf.Cos(radianes) * radio;
        float y = Mathf.Sin(radianes) * radio;

        transform.position = centro + new Vector3(x, y, 0f);
    }
}