using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public HealthScript healthScript;
    private void OnTriggerStay2D(Collider2D collision){
        if (collision.CompareTag("Enemy") || collision.CompareTag("Collectable")){
            if (collision.CompareTag("Enemy")){
                explosionEffect.gameObject.transform.localScale *= collision.transform.localScale.x;
                Instantiate(explosionEffect, collision.transform.position, collision.transform.rotation);
                explosionEffect.gameObject.transform.localScale /= collision.transform.localScale.x;
            }
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("Explosion");
        }

        if (collision.CompareTag("Player")){
            if (healthScript.health < healthScript.maxHealth){
                Debug.Log("Healing!");
                healthScript.Heal(0.1f);
            }
        }
    }
    
}
