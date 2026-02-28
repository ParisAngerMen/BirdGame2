using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float totalPoints = 0;
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        else
        {
            instance = this;
        }
    }
    public void SetPoints(int value)
    {
        totalPoints += value;
    }

    public float GetPoints()
    {
        return totalPoints;
    }
}
