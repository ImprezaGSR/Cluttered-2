using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatus : MonoBehaviour
{
    public bool solarPanel = false;
    public bool thruster = false;
    public bool electricity = false;
    public bool suitRoom = false;
    public int botanistLevel = 0;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (solarPanel == true){
            this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        if (thruster == true){
            this.gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        if (electricity == true){
            this.gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
        if (suitRoom == true){
            this.gameObject.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
        }
        if (botanistLevel > 0){
            this.gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            for(int i = 0; i < botanistLevel; i++){
                this.gameObject.transform.GetChild(1).GetChild(1).GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public bool GetSolarPanel(){
        return solarPanel;
    }
    public bool GetThruster(){
        return thruster;
    }
    public bool GetElectricity(){
        return electricity;
    }
    public bool GetSuitRoom(){
        return suitRoom;
    }
    public int GetBotanistLevel(){
        return botanistLevel;
    }
    public void SetSolarPanel(bool newSolarPanel){
        solarPanel = newSolarPanel;
    }
    public void SetThruster(bool newThruster){
        thruster = newThruster;
    }
    public void SetElectricity(bool newElectricity){
        electricity = newElectricity;
    }
    public void SetSuitRoom(bool newSuitRoom){
        suitRoom = newSuitRoom;
    }
    public void SetBotanistLevel(int newBotanistLevel){
        botanistLevel = newBotanistLevel;
    }
}
