using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points;

    public void AddCoin() 
    {
        GameManager.instance.SetPoints(points);
    }

}
