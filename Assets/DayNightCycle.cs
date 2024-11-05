using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // De lichtbron die de zon of maan vertegenwoordigt
    public float dayDuration = 120f; // Totale duur van een dag in seconden
    public Gradient lightColor; // Kleurverandering gedurende de dag (optioneel)
    public AnimationCurve lightIntensity; // Verandering van intensiteit gedurende de dag (optioneel)

    [Header("Storm Settings")]
    public bool enableNightStorms = true; // Schakel stormen 's nachts in of uit
    public ParticleSystem rainEffect; // Particle system voor regen
    public Light stormLightning; // Licht voor bliksem
    public AudioSource thunderSound; // Geluidseffect voor donder
    public float stormChance = 0.3f; // Kans op een storm 's nachts (0 = geen storm, 1 = altijd storm)
    public float lightningFrequency = 5f; // Hoe vaak de bliksem verschijnt in seconden

    private float timeOfDay = 0f; // Houdt de tijd van de dag bij (0-1)
    private bool isNight = false;
    private bool stormActive = false;
    private float nextLightningTime = 0f;

    void Update()
    {
        // Bereken de tijd van de dag als een waarde tussen 0 en 1
        timeOfDay += Time.deltaTime / dayDuration;
        if (timeOfDay >= 1f)
        {
            timeOfDay = 0f; // Begin een nieuwe dag als de huidige voorbij is
        }

        // Rotatie van het licht instellen (draait rond de x-as)
        float sunAngle = timeOfDay * 360f - 90f; // Rotatie tussen -90 (zonsopkomst) en 270 (zonsondergang)
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // Stel de lichtkleur in als er een gradient is
        if (lightColor != null)
        {
            directionalLight.color = lightColor.Evaluate(timeOfDay);
        }

        // Stel de lichtintensiteit in als er een curve is
        if (lightIntensity != null)
        {
            directionalLight.intensity = lightIntensity.Evaluate(timeOfDay);
        }

        // Controleer of het nacht is (bijvoorbeeld na zonsondergang)
        isNight = (timeOfDay > 0.75f || timeOfDay < 0.25f); // Nacht is tussen 75% en 25% van de cyclus

        // Activeer of deactiveer stormen 's nachts
        HandleNightStorms();
    }

    void HandleNightStorms()
    {
        if (isNight && enableNightStorms)
        {
            // Start een storm willekeurig als deze nog niet actief is
            if (!stormActive && Random.Range(0f, 1f) < stormChance)
            {
                stormActive = true;
                ActivateStorm();
            }
        }
        else
        {
            // Stop de storm als het dag wordt
            if (stormActive)
            {
                DeactivateStorm();
                stormActive = false;
            }
        }

        // Als er een storm is, beheer de bliksem
        if (stormActive)
        {
            HandleLightning();
        }
    }

    void ActivateStorm()
    {
        // Activeer regen effect
        if (rainEffect != null)
        {
            rainEffect.Play();
        }

        // Eventueel kan je hier andere visuele of gameplay effecten activeren
        Debug.Log("Storm gestart!");
    }

    void DeactivateStorm()
    {
        // Stop regen effect
        if (rainEffect != null)
        {
            rainEffect.Stop();
        }

        // Eventueel kan je hier andere visuele of gameplay effecten stoppen
        Debug.Log("Storm gestopt!");
    }

    void HandleLightning()
    {
        // Genereer willekeurig bliksem op een interval
        if (Time.time >= nextLightningTime)
        {
            if (stormLightning != null)
            {
                // Flits de bliksem kort aan
                stormLightning.enabled = true;
                Invoke("TurnOffLightning", 0.2f); // Zet bliksem na 0.2 seconden weer uit
            }

            // Speel donderslag af
            if (thunderSound != null)
            {
                thunderSound.Play();
            }

            // Bepaal de tijd voor de volgende bliksemflits
            nextLightningTime = Time.time + Random.Range(2f, lightningFrequency);
        }
    }

    void TurnOffLightning()
    {
        if (stormLightning != null)
        {
            stormLightning.enabled = false;
        }
    }
}
