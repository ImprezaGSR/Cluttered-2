using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public void DeactivateSelf(){
        gameObject.SetActive(false);
    }
}
