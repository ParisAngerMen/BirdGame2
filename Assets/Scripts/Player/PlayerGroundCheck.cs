using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private PlayerMovement player;
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player has touched ground");
        player.groundedPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.groundedPlayer = false;
    }
}
