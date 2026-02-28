using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 8f;
    public float tiempoVida = 3f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Aquí luego puedes hacer daño
        Destroy(gameObject);
    }
}