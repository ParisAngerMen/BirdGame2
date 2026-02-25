using UnityEngine;

public class EnemigoPatrulla2D : MonoBehaviour
{
    public float velocidad = 3f;
    public float tiempoParaGirar = 3f;

    private float contadorTiempo;
    private int direccion = 1; // 1 = derecha, -1 = izquierda

    void Update()
    {
        // Movimiento constante
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);

        // Contador
        contadorTiempo += Time.deltaTime;

        if (contadorTiempo >= tiempoParaGirar)
        {
            Girar();
            contadorTiempo = 0f;
        }
    }

    void Girar()
    {
        direccion *= -1;

        // Voltear sprite visualmente
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}