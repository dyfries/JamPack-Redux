using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class that applies the current health value of a given health component to the fill amount of a Unity UI Image.
/// </summary>
[RequireComponent(typeof(Image))]
public class HealthBarUI : MonoBehaviour
{
    [Header("Whose Health are we Displaying?")]
    [Tooltip("The Health component that we'd like to track with the Image")]
    public Health healthComponentToTrack;
    
    // The Image the fill effect will be applied on.
    private Image healthBarImage;
    
    // The percentage that will be applied to the image's fillAmount.
    private float percentFilled;
    
    // CODE TO RUN ON START
    private void Start()
    {
        // Grab a reference to the health bar image
        healthBarImage = GetComponent<Image>();
        
        
        // Check to see if the health component we're displaying is valid.
        if (healthComponentToTrack == null)
        {
            Debug.LogError("HealthBarUI needs a Health Component to read");
        }
        
        
        // Start listening for the OnDamage event from that health component.
        healthComponentToTrack.OnDamage.AddListener(SetPercentFilled);
    }
    
    /// <summary>
    /// A method that sets the fill amount of a Unity UI Image to equal the current health of the given health component.
    /// </summary>
    public void SetPercentFilled()
    {
        // Get current health as a number between 0 and 1, where 1 is max health. ( currentHP / maxHP )
        percentFilled = healthComponentToTrack.GetCurrentHealth() / healthComponentToTrack.maxHP;
        
        
        // Ensure the above value is between 0 and 1
        percentFilled = Mathf.Clamp01(percentFilled);
        
        
        // Apply this value to the health bar image's fill amount.
        healthBarImage.fillAmount = percentFilled;
    }
}
