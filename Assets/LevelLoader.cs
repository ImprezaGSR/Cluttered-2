using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider volumeSlider;
    public Slider musicSlider;
    public Slider progressSlider;
    public Text percent;
    public GameObject mainMenu;
    public ShipInventory shipInventory;
    //ShipHub Data (Needed)
    private int capsuleNumR = 20;
    private int armourLevel = 0;
    private int thrusterLevel = 0;
    private int tetherLevel = 0;
    //ShipHub Data (Not needed)
    private int currentIntR = 0;
    private int maxR = 3600;
    private bool isHubFirst = false;
    public bool solarPanel = false;
    private bool thruster = false;
    private bool electricity = false;
    private bool suitRoom = false;
    private int botanistLevel = 0;
    private bool isGameEnded = false;
    private int gameClosed;
    private bool isFirst;

    public void LoadLevel(int sceneIndex){
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    void Start(){
        Load();
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

    public void ClearData(){
        if(File.Exists(Application.persistentDataPath + "/save.txt")){
            File.Delete(Application.persistentDataPath + "/save.txt");
        }
    }

    // private void Load(){
    //     if(File.Exists(Application.dataPath + "/save.txt")){
    //         string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
    //         Debug.Log("Loaded: " + saveString);
    //         SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
    //         FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustVolume(saveObject.volume);
    //         FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustMusic(saveObject.music);
    //         volumeSlider.value = saveObject.volume;
    //         musicSlider.value = saveObject.music;
    //     }else{
    //         Debug.Log("No Save File!");
    //     }
    // }

    public void Save(){
        float health = 100;
        float volume = FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().getVolume();
        float music = FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().getMusic();
        
        List<Items.ItemType> itemTypes = shipInventory.GetItemTypes();
        List<int> amounts = shipInventory.GetAmounts();
        // string itemTypes = shipInventory.GetItemTypeString();
        // string amounts = shipInventory.GetAmountString();
        string test = "You suck at Coding!";
        int currentInt = currentIntR;
        int max = maxR;
        int capsuleNum = capsuleNumR;
        bool hubFirst = isHubFirst;
        bool solar = solarPanel;
        bool thrus = thruster;
        bool elec = electricity;
        bool sr = suitRoom;
        int bl = botanistLevel;
        bool end = isGameEnded;
        int al = armourLevel;
        int thl = thrusterLevel;
        int tel = tetherLevel;
        int closed = gameClosed;
        bool sFirst = isFirst;

        SaveObject saveObject = new SaveObject{
            health = health,
            volume = volume,
            music = music,
            itemTypes = itemTypes,
            amounts = amounts,
            test = test,
            currentInt = currentInt,
            max = max,
            capsuleNum = capsuleNum,
            hubFirst = hubFirst,
            solar = solar,
            thrus = thrus,
            elec = elec,
            sr = sr,
            bl = bl,
            end = end,
            al = al,
            thl = thl,
            tel = tel,
            closed = closed,
            sFirst = sFirst
        };
        string json = JsonUtility.ToJson(saveObject);
        UnityEngine.iOS.Device.SetNoBackupFlag(json);
        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
        Debug.Log("Saved!");
    }

    private void Load(){
        if(File.Exists(Application.persistentDataPath + "/save.txt")){
            mainMenu.transform.GetChild(0).gameObject.SetActive(true);
            mainMenu.transform.GetChild(1).gameObject.SetActive(false);
            string saveString = File.ReadAllText(Application.persistentDataPath + "/save.txt");
            Debug.Log("Loaded: " + saveString);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustVolume(saveObject.volume);
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustMusic(saveObject.music);
            volumeSlider.value = saveObject.volume;
            musicSlider.value = saveObject.music;
            currentIntR = saveObject.currentInt;
            maxR = saveObject.max;
            capsuleNumR = saveObject.capsuleNum;
            isHubFirst = saveObject.hubFirst;
            solarPanel = saveObject.solar;
            thruster = saveObject.thrus;
            electricity = saveObject.elec;
            suitRoom = saveObject.sr;
            botanistLevel = saveObject.bl;
            isGameEnded = saveObject.end;
            armourLevel = saveObject.al;
            thrusterLevel = saveObject.thl;
            tetherLevel = saveObject.tel;
            gameClosed = saveObject.closed;
            for(int i = 0; i < saveObject.itemTypes.Count; i++){
                shipInventory.AddItemToShip(new Items{itemType = saveObject.itemTypes[i], amount = saveObject.amounts[i]});
            }
        }else{
            Debug.Log("No Save File!");
            mainMenu.transform.GetChild(1).gameObject.SetActive(true);
            mainMenu.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
}
