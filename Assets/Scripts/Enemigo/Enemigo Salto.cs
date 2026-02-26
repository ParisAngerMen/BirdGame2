using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemigoSaltadorBasico2D : MonoBehaviour
{
    public float fuerzaSalto = 10f;
    public float fuerzaHorizontal = 5f;
    public float intervaloSalto = 1.5f;
    public float tiempoCambioDireccion = 4f;

    private Rigidbody2D rb;
    private float contadorSalto;
    private float contadorDireccion;
    private int direccion = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        contadorSalto += Time.deltaTime;
        contadorDireccion += Time.deltaTime;

        if (contadorSalto >= intervaloSalto)
        {
            rb.linearVelocity = new Vector2(direccion * fuerzaHorizontal, fuerzaSalto);
            contadorSalto = 0f;
        }

        if (contadorDireccion >= tiempoCambioDireccion)
        {
            direccion *= -1;

            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;

            contadorDireccion = 0f;
        }
    }
}