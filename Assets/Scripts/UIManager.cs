using UnityEngine;
using UnityEngine.UI; // Zorg ervoor dat je toegang hebt tot UI-elementen zoals Text

public class UIManager : MonoBehaviour
{
    public Text collectibleText; // Referentie naar de UI Text waar het aantal verzamelde objecten wordt weergegeven

    void Update()
    {
        // Werk de tekst bij met het huidige aantal verzamelde objecten
        collectibleText.text = "Collected: " + CollectibleItem.collectedItems;
    }
}
