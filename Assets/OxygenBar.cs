using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public CountDown countDown;
    public Image oxygenBar;
    // Start is called before the first frame update
    void HealthBarFiller(){
        oxygenBar.fillAmount = countDown.oxygen/countDown.maxOxygen;
    }
    // Update is called once per frame
    void Update()
    {
        HealthBarFiller();
    }
}
