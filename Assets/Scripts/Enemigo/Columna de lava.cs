using System.Collections;
using UnityEngine;

public class ColumnaLava2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float alturaMaxima = 5f;
    public float velocidad = 3f;

    [Header("Tiempos")]
    public float tiempoArriba = 2f;
    public float tiempoAbajo = 2f;

    private Vector3 posicionInicial;
    private Vector3 posicionArriba;

    void Start()
    {
        posicionInicial = transform.position;
        posicionArriba = posicionInicial + Vector3.up * alturaMaxima;

        StartCoroutine(CicloLava());
    }

    IEnumerator CicloLava()
    {
        while (true)
        {
            // Subir
            yield return StartCoroutine(Mover(posicionArriba));

            yield return new WaitForSeconds(tiempoArriba);

            // Bajar
            yield return StartCoroutine(Mover(posicionInicial));

            yield return new WaitForSeconds(tiempoAbajo);
        }
    }

    IEnumerator Mover(Vector3 destino)
    {
        while (Vector3.Distance(transform.position, destino) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino,
                velocidad * Time.deltaTime
            );

            yield return null;
        }

        transform.position = destino;
    }
}