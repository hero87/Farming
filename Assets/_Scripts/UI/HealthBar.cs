using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Gradient gradient;
    

    public void SetValue(float percentage)
    {
        healthImage.fillAmount = percentage;
        healthImage.color = gradient.Evaluate(percentage);
    }

}
