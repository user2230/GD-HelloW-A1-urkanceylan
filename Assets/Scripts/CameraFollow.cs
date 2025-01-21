using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // De speler die gevolgd moet worden
    public Vector3 offset;    // Verschil in positie tussen de camera en de speler
    public float smoothSpeed = 0.125f;  // Snelheid van de camerabeweging

    void LateUpdate()
    {
        // De gewenste positie van de camera, offset vanaf de speler
        Vector3 desiredPosition = player.position + offset;

        // Maak de beweging van de camera soepel door Lerp te gebruiken
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Zet de camera naar de nieuwe positie
        transform.position = smoothedPosition;

        // Zorg ervoor dat de camera altijd naar de speler kijkt
        transform.LookAt(player);
    }
}
