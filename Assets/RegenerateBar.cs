using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegenerateBar : MonoBehaviour
{
    public Regenerate regenerate;
    public Image regenerateBar;
    // Start is called before the first frame update
    void HealthBarFiller(){
        regenerateBar.fillAmount = regenerate.current/regenerate.max;
    }
    // Update is called once per frame
    void Update()
    {
        HealthBarFiller();
    }
}
