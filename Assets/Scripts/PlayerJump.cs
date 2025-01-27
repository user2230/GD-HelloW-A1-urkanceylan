using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 10f;  // Force applied for jumping
    public LayerMask groundLayer;  // Ground layer to detect when player is grounded
    private Rigidbody rb;          // Reference to the player's Rigidbody
    private bool isGrounded;       // Check if the player is on the ground
    public Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get the Rigidbody component
    }

    void Update()
    {
        // Check if the player is on the ground before allowing a jump
        isGrounded = IsGrounded();

        // Jump when pressing the spacebar and player is on the ground
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("jump");
            Jump();
        }
    }

    void Jump()
    {
        // Apply force to the Rigidbody to make the player jump
        Debug.Log("Jump");
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        _animator.SetTrigger("jump");
    }

    bool IsGrounded()
        {
            // Cast a ray downwards to check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);
    }
}
