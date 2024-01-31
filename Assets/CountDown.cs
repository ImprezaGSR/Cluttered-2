using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public GameObject player;
    public GameObject healthBar;
    public float oxygen = 1000;
    public float maxOxygen = 1000;
    public float timeStart = 180;
    public float lowOxygen = 200;
    public Text amount;
    public Text maxAmount;
    public GameObject lowOxygenText;
    public ParticleSystem explosionEffect;
    private bool exploded = false;
    private bool isPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        amount.text = oxygen.ToString();
        maxAmount.text = maxOxygen.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(oxygen > 0 && FindObjectOfType<GameManager>().gameIsPaused == false){
            oxygen -= (maxOxygen / timeStart) * Time.deltaTime;
            amount.text =  Mathf.Round(oxygen).ToString();
        }
        if (oxygen <= lowOxygen){
            lowOxygenText.SetActive(true);
            if(isPlayed == false){
                FindObjectOfType<AudioManager>().Play("Warning");
                FindObjectOfType<AudioManager>().Play("Breathing");
                isPlayed = true;
            }
        }
        if (oxygen <= 0){
            lowOxygenText.SetActive(false);
            FindObjectOfType<AudioManager>().Stop("Warning");
            FindObjectOfType<AudioManager>().Stop("Breathing");
            amount.text = (0).ToString();
            if (exploded == false){
                Instantiate(explosionEffect, player.transform.position, player.transform.rotation);
                FindObjectOfType<AudioManager>().Play("Explosion");
                exploded = true;
            }
            Destroy(player);
            Destroy(healthBar);
            FindObjectOfType<GameManager>().setDeathReason("Player has suffocated");
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
