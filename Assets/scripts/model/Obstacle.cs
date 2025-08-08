using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftBound;

    private void Start()
    {
        // Get the x-position just off-screen to the left
        leftBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 2f;
    }

    private void Update()
    {
        // Move obstacle left based on game speed
        transform.Translate(Vector3.left * gameManager.instance.gameSpeed* Time.deltaTime);

        // Destroy the obstacle when it moves off screen
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
