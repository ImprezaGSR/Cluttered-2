using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMenu : MonoBehaviour
{
    public void getMenu(){
        FindObjectOfType<OptionUICanvas>().gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
