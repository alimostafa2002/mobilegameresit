
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player hit an obstacle!");

            // 🔧 Tell the camera to stop following before destroying player
            Camera.main.GetComponent<camerafollow>().ClearTarget();

            // 🗑️ Destroy the player
            GetComponent<PlayerMovement>().enabled = false;

            gameManager.instance.GameOver();



        }
    }
}
