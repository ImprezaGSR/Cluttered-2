using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthScript : MonoBehaviour
{
    // public Text healthText;
    public event EventHandler OnHealthChanged;
    // public Image healthBar;
    public float health = 100;
    public float maxHealth = 100;
    public float criticalHealth = 0.2f;
    public Text amount;
    public Text critical;
    public GameObject healthLeft;

    public ParticleSystem explosionEffect;
    // float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
        // OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    // Update is called once per frame
    void Update()
    {
        // if (health > maxHealth){
        //     health = maxHealth;
        // }
        // lerpSpeed = 3f * Time.deltaTime;
        // HealthBarFiller();
        // ColorChanger();
    }

    // void HealthBarFiller(){
    //     healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    // }
    // void ColorChanger(){
    //     Color healthColor = Color.Lerp(Color.red, Color.green, (health/maxHealth));
    //     healthBar.color = healthColor;
    // }

    // private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e){
    //     lerpSpeed = 1000000000f * Time.deltaTime;
    //     HealthBarFiller();
    //     ColorChanger();
    //     Debug.Log("Health: "+health);
    // }

    public void Damage(float damagePoints){
        health -= damagePoints;
        if (health < 0){
            health = 0;
        }
        Debug.Log("Health: " +Mathf.Round(health)+"/"+maxHealth);
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        if (health > 0){
            amount.text = Mathf.Round(health).ToString()+"/"+maxHealth.ToString();
            if (health <= maxHealth * criticalHealth){
                critical.text = "Critical Health!";
            }
            healthLeft.SetActive(false);
            healthLeft.SetActive(true);
        }
        if (health <= 0){
            Instantiate(explosionEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Explosion");
            FindObjectOfType<AudioManager>().Stop("Warning");
            FindObjectOfType<AudioManager>().Stop("Breathing");
            FindObjectOfType<GameManager>().setDeathReason("Player was crushed by an asteroid");
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    public void Heal(float healingPoints){
        if (health > maxHealth * criticalHealth){
            critical.text = "";
        }
        health += healingPoints;
        if (health > maxHealth){
            health = maxHealth;
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
    public void HealthLevel(int level){
        maxHealth = 100 + (20*level);
        health = maxHealth;
    }
}
