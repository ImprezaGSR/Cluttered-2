using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public bool isFirst = true;
    public static Intro instance;
    void Awake(){
        if (instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        if (isFirst = false){
            gameObject.SetActive(false);
        }
    }

    void Update(){
        if (isFirst = false){
            gameObject.SetActive(false);
        }
    }

    public void ButtonClicked(){
        isFirst = false;
    }
}
