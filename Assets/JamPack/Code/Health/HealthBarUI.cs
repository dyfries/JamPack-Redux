using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Image to Apply Effect On")]
    public Image healthBar;

    public Health healthToShow;
    
    // The percentage that will be applied to the image's fillAmount.
    private float percentFilled;


    private void Start()
    {
        if (healthBar == null)
        {
            Debug.LogError("HealthBarUI needs a healthBar Image Component");
        }
        
        if (healthToShow == null)
        {
            Debug.LogError("HealthBarUI needs a Health Component to read");
        }

        healthToShow.OnDamage.AddListener(SetPercentFilled);
    }
    
    public void SetPercentFilled()
    {
        float healthPercent = healthToShow.GetCurrentHealth() / healthToShow.maxHP;
        percentFilled = Mathf.Clamp01(healthPercent);
        healthBar.fillAmount = percentFilled;
    }
}
