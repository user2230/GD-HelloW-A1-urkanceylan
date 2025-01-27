using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Snelheid van de speler
    public float jumpForce = 5f;  // Springkracht
    public Animator animator;

    private Rigidbody rb;
    public GameObject animatedModel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Haal de Rigidbody van de speler op
    }

    void Update()
    {
        // Beweging in de horizontale richting
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D of pijltjestoetsen
        float moveVertical = Input.GetAxis("Vertical");  // W/S of pijltjestoetsen

        // Verplaats de speler op de X- en Z-as
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);

        if (movement != Vector3.zero)
        {
            animator.ResetTrigger("idle");
            animator.SetTrigger("walk");
        }
        else
        {
            animator.ResetTrigger("walk");
            animator.SetTrigger("idle");
        }
        animatedModel.transform.localPosition = new Vector3();
    }
}
