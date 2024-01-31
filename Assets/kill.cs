using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q)){
            gameObject.GetComponent<HealthScript>().Damage(9999);
        }
    }
}
