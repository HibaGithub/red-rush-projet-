using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public static health healthSystem;
    public Image healthBar;
    public bool healthDamage = false;

    // if health is not instanced then instance it within it self
    void Awake()
    {
        if (healthSystem == null)
        {
            healthSystem = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // calcule health every time player falls
    public void HealthState(float heart)
    {
        healthBar.fillAmount = heart / 5;
    }
}
