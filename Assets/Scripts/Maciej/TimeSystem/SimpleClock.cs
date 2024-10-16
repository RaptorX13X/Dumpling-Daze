using UnityEngine;
using TMPro;
using UnityEngine.Events;
using FMODUnity;

public class SimpleClock : MonoBehaviour
{
    private TextMeshProUGUI clockText;
    private int hours = 0;
    private int minutes = 0;
    private int lastHour = -1; // Track the last hour to detect hour changes
    private const float dayDurationInSeconds = 10 * 60 * 60; // 10 hours in seconds
    private int initialSeconds; // Store the initial seconds

    [SerializeField] private float timeScale = 1.0f; // Time scale multiplier
    [SerializeField] private float updateInterval = 1f; // Update interval for the clock display
    [SerializeField] private int maxHours = 24; // Maximum hours before resetting

    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string hourSound;

    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private Light skylight;
    
    [SerializeField] private Gradient fogGradient;
    [SerializeField] private Gradient lightGradient;
    [SerializeField] private TimeSO timeScriptableObject;

    [SerializeField] private bool canStartClock = false;

    [SerializeField] private UnityEvent endDay;

    void Start()
    {
        // Get the TextMeshProUGUI component attached to the GameObject
        clockText = GetComponent<TextMeshProUGUI>();
    }

    public void StartClock()
    {

        canStartClock = true;

        // Start the coroutine to update the clock
        StartCoroutine(UpdateClock());
    }

    private void EndClock()
    {
        canStartClock = false;

        endDay.Invoke();
    }

    float timeStartOffset;
    System.Collections.IEnumerator UpdateClock()
    {
        timeStartOffset = Time.time;
        while (canStartClock)
        {
            // Calculate current time within the 10-hour day
            float currentTime = (Time.time - timeStartOffset ) * timeScale ;
            float normalizedTime = currentTime % dayDurationInSeconds;

            // Convert normalized time to hours and minutes
            hours = Mathf.FloorToInt(normalizedTime / 3600) + 8; // Start from 8:00
            minutes = Mathf.FloorToInt((normalizedTime % 3600) / 60);

            // Format the time as a string (HH:mm:ss)
            string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, initialSeconds);

            // Update the TextMeshProUGUI component to display the current time
            clockText.text = timeString;
            timeScriptableObject.Time = timeString;


            // Check if the hour has changed since the last update
            if (hours != lastHour)
            {
                lastHour = hours;
                // Play the hour change sound
                if (hourSound != null)
                {
                   RuntimeManager.PlayOneShot(hourSound, transform.position);
                }
            }

            // Calculate atmosphere thickness based on time
            float lerpTime = normalizedTime / dayDurationInSeconds; // Normalize time to [0, 1]
            float atmosphereThickness = Mathf.Lerp(0.5f, 2f, lerpTime);

            // Apply atmosphere thickness to the Skybox material
            if (skyboxMaterial != null)
            {
                skyboxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
            }

            // Calculate light color based on time
            Color lightColor = lightGradient.Evaluate(lerpTime); // Use gradient to calculate light color

            RenderSettings.fogColor = fogGradient.Evaluate(lerpTime); // Use gradient to calculate fog color

            // Calculate light intensity based on time
            float intensity = Mathf.Lerp(1f, 2f, lerpTime);

            // Apply light color to the directional light
            if (skylight != null)
            {
                skylight.color = lightColor;
                skylight.intensity = intensity;
            }

            if (hours == 16 && minutes == 0)
            {
                Debug.Log("It's 16:00 o'clock!");
                EndClock();
            }

            // Wait for the specified update interval
            yield return new WaitForSeconds(updateInterval);
        }
    }

    // Function to calculate light color based on normalized time
    private Color CalculateLightColor(float normalizedTime)
    {
        // You can customize the color change over time here
        Color startColor = Color.white; // Starting color
        Color endColor = new Color(1.0f, 0.5137f, 0.0f); // Ending color

        return Color.Lerp(startColor, endColor, normalizedTime);
    }
}