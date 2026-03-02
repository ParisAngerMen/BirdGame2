using UnityEngine;
using System.Collections;

public class PlataformaSubeBaja : MonoBehaviour
{
    public float altura = 3f;
    public float velocidad = 2f;
    public float tiempoEspera = 1.5f;

    private Vector3 posicionInicial;
    private Vector3 posicionFinal;

    void Start()
    {
        posicionInicial = transform.position;
        posicionFinal = posicionInicial + Vector3.up * altura;

        StartCoroutine(MoverPlataforma());
    }

    IEnumerator MoverPlataforma()
    {
        while (true)
        {
            // Subir
            while (Vector3.Distance(transform.position, posicionFinal) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    posicionFinal,
                    velocidad * Time.deltaTime
                );
                yield return null;
            }

            yield return new WaitForSeconds(tiempoEspera);

            // Bajar
            while (Vector3.Distance(transform.position, posicionInicial) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    posicionInicial,
                    velocidad * Time.deltaTime
                );
                yield return null;
            }

            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}