using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player to follow
    public Vector3 offset; // Offset from the player's position

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}