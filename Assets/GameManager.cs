using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool gameIsPaused = false;
    public float restartDelay = 1f;
    public GameObject comleteLevelUI;
    public GameObject gameOverUI;
    public Text deathReason;
    private ShipInventory shipInventory;
    //LoadingScreen
    public GameObject loadingScreen;
    public Slider progressSlider;
    public Text percent;
    

    void Awake(){
        // SaveObject saveObject = new SaveObject{
        //     health = 100,
        // };
        // string json = JsonUtility.ToJson(saveObject);
        // SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(json);
        // Debug.Log(loadedSaveObject.health);
    }

    // void Start(){
    //     Load();
    //     shipInventory = new ShipInventory();
    //     Debug.LogFormat("Loaded inventory {0}", JsonUtility.ToJson(shipInventory));
    // }
    public void CompleteLevel(){
        comleteLevelUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
    public void EndGame(){
        if (gameIsPaused == false){
            gameIsPaused = true;
            Debug.Log("GAME OVER");
            gameOverUI.SetActive(true);
            // Invoke("Restart", restartDelay);
        }
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void setDeathReason(String newDeathReason){
        deathReason.text = newDeathReason;
    }

    public void Pause(){
        Time.timeScale = 0f;
    }
    public void Resume(){
        Time.timeScale = 1f;
    }
    public void PlaySelect(){
        FindObjectOfType<AudioManager>().Play("Select");
    }

    public void PlayBlip(){
        FindObjectOfType<AudioManager>().Play("Blip");
    }

    // // public void SavePlayer(){
    // //     SaveSystem.SavePlayer(FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>(), FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>(), shipInventory);
    // // }

    // // public void LoadPlayer(){
    // //     PlayerStatus data = SaveSystem.LoadPlayer();
    // //     FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().maxHealth = data.health;
    // //     FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().maxHealth = data.health;
    // //     FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustVolume(data.volume);
    // //     FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustMusic(data.music);
    // //     foreach(Items item in data.inventoryTotal){
    // //         shipInventory.AddItemToShip(item);
    // //     }
    // // }

    // private static void SaveJsonData(GameManager a_GameManager){
    //     PlayerStatus sd = new PlayerStatus();
    //     a_GameManager.PopulateSaveData(sd);
    //     if (FileManager.WriteToFile("SaveData.dat", sd.ToJson())){
    //         Debug.Log("Save successful");
    //     }
    // }
    // public void PopulateSaveData(PlayerStatus a_SaveData){
    //     a_SaveData.health = FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().maxHealth;
    //     a_SaveData.volume = FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().getVolume();
    //     a_SaveData.music = FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().getMusic();
    //     a_SaveData.inventoryTotal = shipInventory.GetShipItemList();
    // }

    // private static void LoadJsonData(GameManager a_GameManager){
    //     if(FileManager.LoadFromFile("SaveData.dat", out var json)){
    //         PlayerStatus sd = new PlayerStatus();
    //         sd.LoadFromJson(json);
    //         a_GameManager.LoadFromSaveData(sd);
    //         Debug.Log("Load Complete");
    //     }
    // }

    // public void LoadFromSaveData(PlayerStatus a_SaveData){
    //     FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().maxHealth = a_SaveData.health;
    //     FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().health = a_SaveData.health;
    //     FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustVolume(a_SaveData.volume);
    //     FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustMusic(a_SaveData.music);
    //     foreach(Items item in a_SaveData.inventoryTotal){
    //         shipInventory.AddItemToShip(item);
    //     }
    // }
    // public void Save(){
    //     float health = FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().maxHealth;
    //     float volume = FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().getVolume();
    //     float music = FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().getMusic();
        
    //     // List<Items.ItemType> itemTypes = shipInventory.GetItemTypes();
    //     // List<int> amounts = shipInventory.GetAmounts();
    //     string itemTypes = shipInventory.GetItemTypeString();
    //     string amounts = shipInventory.GetAmountString();
    //     string test = "You suck at Coding!";
        

    //     SaveObject saveObject = new SaveObject{
    //         health = health,
    //         volume = volume,
    //         music = music,
    //         itemTypes = itemTypes,
    //         amounts = amounts,
    //         test = test
    //     };
    //     string json = JsonUtility.ToJson(saveObject);
    //     File.WriteAllText(Application.dataPath + "/save.txt", json);
    //     Debug.Log("Saved!");

    // }

    // private void Load(){
    //     if(File.Exists(Application.dataPath + "/save.txt")){
    //         string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
    //         Debug.Log("Loaded: " + saveString);
    //         SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

    //         // FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().maxHealth = saveObject.health;
    //         // FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().health = saveObject.health;
    //         // FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustVolume(saveObject.volume);
    //         // FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustMusic(saveObject.music);
    //         // for(int i = 0; i < saveObject.itemTypes.Count; i++){
    //         //     shipInventory.AddItemToShip(new Items{itemType = saveObject.itemTypes[i], amount = saveObject.amounts[i]});
    //         // }
    //     }else{
    //         Debug.Log("No Save File!");
    //     }
    // }

    // private class SaveObject{
    //     public float health;
    //     public float volume;
    //     public float music;
    //     // public List<Items.ItemType> itemTypes;
    //     // public List<int> amounts;
    //     public string itemTypes;
    //     public string amounts;
    //     public string test;
    // }
    public void LoadLevel(int sceneIndex){
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex){
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressSlider.value = progress;
            percent.text = Mathf.Round(progress * 100f) + "%";
            yield return null; 
        }
    }
}
