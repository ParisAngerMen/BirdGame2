using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float totalPoints = 0;
    [SerializeField] private AudioClip[] sounds;
    private AudioSource audioSource;

    public enum Sounds
    {
        jump,
        hit,
        attack,
        death,
        coin,
        powerup,
        checkpoint
    }
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

        audioSource = GetComponent<AudioSource>();
    }
    public void SetPoints(int value)
    {
        totalPoints += value;
    }

    public float GetPoints()
    {
        return totalPoints;
    }

    public void PlaySound(Sounds sound)
    {
        instance.audioSource.PlayOneShot(instance.sounds[(int)sound]);
    }
}
