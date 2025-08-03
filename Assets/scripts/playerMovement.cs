using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D characterController;
    public float runSpeed = 10f;
    private bool jump = false;

    [Header("Animation")]
    public Animator animator;

    void Update()
    {
        // Always running (in place)
        animator.SetFloat("runSpeed", runSpeed); // Triggers run animation constantly

        // Handle jump input
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // ❗ Set horizontalMove to 0 to prevent actual movement
        characterController.Move(0f, false, jump);
        jump = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player hit an obstacle!");

            // 🔧 Tell the camera to stop following before destroying player
            Camera.main.GetComponent<camerafollow>().ClearTarget();

            // 🗑️ Destroy the player

            gameManager.instance.GameOver();



        }
    }
}
