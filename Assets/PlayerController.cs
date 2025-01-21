using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Snelheid van de speler
    public float jumpForce = 5f;  // Springkracht

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
        rb.AddForce(movement * speed);

        // Springen (optioneel, controleer of de speler op de grond is)
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        animatedModel.transform.localPosition = new Vector3();
    }
}
