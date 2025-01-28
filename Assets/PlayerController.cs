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
       Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();  // Haal de Rigidbody van de speler op
    }

    void Update()
    {
        // Beweging in de horizontale richting
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D of pijltjestoetsen
        float moveVertical = Input.GetAxis("Vertical");  // W/S of pijltjestoetsen

        float rot=Input.GetAxis("Mouse X");
        Debug.Log(rot);
        gameObject.transform.Rotate(0, rot*2, 0);
        // Verplaats de speler op de X- en Z-as
        Vector3 movementF = transform.forward*moveVertical;
        Vector3 movementS = transform.right * moveHorizontal;
        movementF += movementS;
        rb.velocity = new Vector3(movementF.x * speed, rb.velocity.y, movementF.z * speed);

        if (movementF != Vector3.zero)
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
