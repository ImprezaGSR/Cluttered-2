using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Regenerate : MonoBehaviour
{
    public int capsuleNum = 20;
    public int capsuleMax = 20;
    public float current = 0;
    public int currentInt = 0;
    public int max = 3600;
    public Text timeLeft;
    public Text capsuleNumber;
    public GameObject lowOxygenText;
    public ParticleSystem explosionEffect;
    public GameObject oxygenUI;
    private bool exploded = false;
    private bool isPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        current = currentInt;
        // if(currentInt >= max){
        //     if (capsuleNum < 100){
        //         capsuleNum += currentInt / max;
        //         SetCurrent(currentInt % max);
        //     }
        // }
        // capsuleNumber.text = capsuleNum+"/100";
        // timeLeft.text = "Regenerate After: "+((max - currentInt)/60).ToString()+"m "+((max - currentInt)%60).ToString()+"s";
        // if (capsuleNum >= 100){
        //     timeLeft.text = "Capacity Full";
        // }
    }

    // Update is called once per frame
    void Update()
    {
        capsuleNumber.text = capsuleNum+"/"+capsuleMax;
        if (GetComponent<ShipManager>().botanistLevel > 0){
            oxygenUI.SetActive(true);
            if(currentInt >= max){
                if (capsuleNum < capsuleMax){
                    capsuleNum += currentInt / max;
                    SetCurrent(currentInt % max);
                }
            }else{
                if (capsuleNum < capsuleMax){
                    current += Time.deltaTime;
                    currentInt = Mathf.FloorToInt(current);
                }
                // Debug.Log("Regenerate After: "+((max - currentInt)/60).ToString()+"m "+((max - currentInt)%60).ToString()+"s");
            }
            capsuleNumber.text = capsuleNum+"/"+capsuleMax;
            timeLeft.text = "Regenerate After: "+((max - currentInt)/60).ToString()+"m "+((max - currentInt)%60).ToString()+"s";
            if (capsuleNum >= capsuleMax){
                capsuleNum = capsuleMax;
                SetCurrent(0);
                timeLeft.text = "Oxygen Capacity Full";
            }    
        }
    }
    public float getFillValue(){
        float percentage = current / max;
        return percentage;
    }
    public int GetMax(){
        return max;
    }
    public void SetMax(int newMax){
        max = newMax;
    }
    public int GetCurrent(){
        return currentInt;
    }
    public void SetCurrent(int newCurrent){
        currentInt = newCurrent;
        current = newCurrent;
    }
    public void AddCurrent(int num){
        currentInt += num;
        current = currentInt;
    }
    public int GetCapsuleNum(){
        return capsuleNum;
    }
    public void SetCapsuleNum(int newCapsuleNum){
        capsuleNum = newCapsuleNum;
    }
    public void TakeCapsule(int num){
        if(capsuleNum >= num){
            capsuleNum -= num;
        }else{
            Debug.Log("ERROR: Insufficient Capsule Amount");
        }
        this.gameObject.GetComponent<ShipManager>().Save();
    }
    public void SetLevel(int level){
        max = Mathf.FloorToInt(3600.0f * (6-level)/5);
        current = current * (6-level)/5;
        currentInt =  Mathf.FloorToInt(current);
    }
    public void SetCapsuleMax(int level){
        if(level-1 >= 0){
            capsuleMax = Mathf.FloorToInt(20.0f + (20.0f*(level-1)));
        }else{
            capsuleMax = 20;
        }
        
    }
}
