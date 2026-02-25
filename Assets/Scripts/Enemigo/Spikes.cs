using System.Collections;
using UnityEngine;

public class Pinchos2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float alturaFuera = 1f;          // Cuánto suben
    public float velocidad = 2f;           // Velocidad subida/bajada

    [Header("Tiempo")]
    public float tiempoOcultos = 3f;       // Tiempo escondidos
    public float tiempoActivos = 2f;       // Tiempo fuera

    private Vector3 posicionInicial;
    private Vector3 posicionVisible;

    private void Start()
    {
        posicionInicial = transform.position;
        posicionVisible = posicionInicial + Vector3.up * alturaFuera;

        StartCoroutine(CicloPinchos());
    }

    IEnumerator CicloPinchos()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoOcultos);

            yield return StartCoroutine(Mover(posicionVisible));

            yield return new WaitForSeconds(tiempoActivos);

            yield return StartCoroutine(Mover(posicionInicial));
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