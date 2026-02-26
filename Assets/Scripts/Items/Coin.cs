using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points = 10;

    public void AddCoin() 
    {
        GameManager.instance.SetPoints(points);
        Destroy(gameObject);               
    }

}
