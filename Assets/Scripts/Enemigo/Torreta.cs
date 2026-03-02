using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1f;

    private float temporizador;

    void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoEntreDisparos)
        {
            Disparar();
            temporizador = 0f;
        }
    }

    void Disparar()
    {
        Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        GameManager.instance.PlaySound(GameManager.Sounds.attack);
    }
}