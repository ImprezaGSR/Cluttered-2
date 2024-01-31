using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    public HealthScript healthScript;
    public Image healthBar;
    float lerpSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        healthScript.OnHealthChanged += HealthSystem_OnHealthChanged;
        ColorChanger();
    }

    void HealthBarFiller(){
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, healthScript.health/healthScript.maxHealth, lerpSpeed);
    }
    void ColorChanger(){
        Color healthColor = Color.Lerp(Color.red, Color.green, (healthScript.health/healthScript.maxHealth));
        healthBar.color = healthColor;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e){
        lerpSpeed = 1000000000f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
