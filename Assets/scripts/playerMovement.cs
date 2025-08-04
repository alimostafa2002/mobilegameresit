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
        // Always play run animation
        animator.SetFloat("runSpeed", runSpeed);

        // ✅ Allow spacebar on ALL platforms (editor, desktop, mobile simulator)
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // Prevent horizontal movement, only jump
        characterController.Move(0f, false, jump);
        jump = false;
    }

    // ✅ Public method for UI Button to call
    public void OnJumpButtonPressed()
    {
        jump = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player hit an obstacle!");

            // Stop camera from following
            Camera.main.GetComponent<camerafollow>().ClearTarget();

            // Trigger game over
            gameManager.instance.GameOver();
        }
    }
}
