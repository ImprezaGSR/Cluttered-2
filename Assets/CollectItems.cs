using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CollectItems : MonoBehaviour
{
    public PlayerScriptTouch playerScriptTouch;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            playerScriptTouch.touchButtons.transform.GetChild(0).gameObject.SetActive(false);
            playerScriptTouch.touchButtons.transform.GetChild(1).gameObject.SetActive(true);
            if (playerScriptTouch.pressed == true){
                playerScriptTouch.CollectItem(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Collectable")){
            playerScriptTouch.touchButtons.transform.GetChild(1).gameObject.SetActive(false);
            playerScriptTouch.touchButtons.transform.GetChild(0).gameObject.SetActive(true);
            playerScriptTouch.pressed = false;
        }
    }
}
