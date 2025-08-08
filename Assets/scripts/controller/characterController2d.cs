using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private float jumpForce = 400f;

    [Header("Environment Checks")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ceilingLayer;

    const float checkRadius = 0.2f;

    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    private bool grounded;
    private bool wasGrounded;
    private bool hitCeiling;

    [Header("Events")]
    public UnityEvent OnLandEvent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    void FixedUpdate()
    {
        wasGrounded = grounded;
        grounded = false;
        hitCeiling = false;

        // Ground check
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, groundLayer);
        foreach (var collider in groundColliders)
        {
            if (collider.gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke(); // 👈 fire landing event
                }
            }
        }

        // Ceiling check
        Collider2D[] ceilingColliders = Physics2D.OverlapCircleAll(ceilingCheck.position, checkRadius, ceilingLayer);
        foreach (var collider in ceilingColliders)
        {
            if (collider.gameObject != gameObject)
            {
                hitCeiling = true;
                // You can add additional logic here if needed
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        // Allow movement even when not grounded (for midair control)
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (grounded && jump)
        {
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    // Optional getter if needed
    public bool IsGrounded() => grounded;
    public bool HitCeiling() => hitCeiling;
}
