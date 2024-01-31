using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    public float health = 100;
    public float totalOxygenCapsule = 10000f;
    public int suitLevel = 1;
    public int jetPackLevel = 1;
    public int inventoryLevel = 1;
    public int tetherLevel = 1;
    public int shieldLevel = 1;

    public List<Items> inventoryTotal;
    
    public int botanistRoomLevel = 1;
    public bool isElectricBoxFixed = false;
    public bool isSuitContainerFixed = false;
    public bool isSolarPanelFixed = false;
    public bool isThrusterFixed = false;
    public bool hasCleared = false;

    public int deathCount = 0;
    public int collectCount = 0;
    public int damageCount = 0;
    public int scavengingCount = 0;
    public int toShipCount = 0;

    public float volume = 1;
    public float music = 1;

    // public PlayerStatus(HealthScript healthScript, AudioManager audioManager, ShipInventory shipInventory){
    //     health = healthScript.maxHealth;
    //     volume = audioManager.getVolume();
    //     volume = audioManager.getMusic();
    //     inventoryTotal = shipInventory.GetShipItemList();
    // }

    public string ToJson(){
        return JsonUtility.ToJson(this);
    }
    public void LoadFromJson(string a_Json){
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }

}
public interface ISaveable{
    void PopulateSaveData(PlayerStatus a_SaveData);
    void LoadFromSaveData(PlayerStatus a_SaveData);
}
