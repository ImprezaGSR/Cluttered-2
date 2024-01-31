using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ShipManager : MonoBehaviour
{
    public bool isHubFirst = false;
    public bool solarPanel = false;
    public bool thruster = false;
    public bool electricity = false;
    public bool suitRoom = false;
    public int botanistLevel = 0;
    public bool isGameEnded = false;
    public ShipInventory shipInventory;
    public int armourLevel = 0;
    public int thrusterLevel = 0;
    public int tetherLevel = 0;
    public int gameClosed;
    [SerializeField] private ShipInventory_UI shipInventoryUI;

    //LoadingScreen
    public GameObject loadingScreen;
    public Slider progressSlider;
    public Text percent;
    public Material newColorMaterial;
    public Material prevColorMaterial;

    public GameObject solarPanelObject;
    public GameObject thrusterObject;
    public GameObject electricityObject;
    public GameObject armouryObject;
    public GameObject botanistObject;
    public GameObject insufficient;
    private GameObject backUI;
    private Transform backCamera;
    public GameObject engineerUI;
    public GameObject mechanicUI;
    public GameObject botanistUI;
    public GameObject requiredItemsUI;
    public GameObject completeUI;
    public GameObject inventoryUI;
    public GameObject introUI;
    public Slider music;
    public Slider volume;
    private bool isFirst = true;
    public GameObject insufficientCapsule;
    
    // Start is called before the first frame update
    void Start()
    {
        
        shipInventory = new ShipInventory();
        shipInventoryUI.SetShipInventory(shipInventory);
        
        Load();
        this.GetComponent<Regenerate>().SetCapsuleMax(botanistLevel);
        if (isHubFirst == true){
            introUI.SetActive(true);
            newColorMaterial.color = Color.white;
            prevColorMaterial.color = Color.white;
            FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("Blip");
            isHubFirst = false;
        }
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int gameStarted = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        if (gameClosed < gameStarted){
            this.gameObject.GetComponent<Regenerate>().AddCurrent(gameStarted - gameClosed);
        }
        Save();
        if (solarPanel == true){
            solarPanelObject.SetActive(true);
            engineerUI.transform.GetChild(3).gameObject.SetActive(false);
        }
        if (thruster == true){
            thrusterObject.SetActive(true);
            engineerUI.transform.GetChild(5).gameObject.SetActive(false);
        }
        if (electricity == true){
            electricityObject.SetActive(true);
            engineerUI.transform.GetChild(4).gameObject.SetActive(false);
        }
        if (suitRoom == true){
            armouryObject.SetActive(true);
            engineerUI.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (botanistLevel > 0){
            botanistObject.SetActive(true);
            for(int i = 0; i < botanistLevel; i++){
                botanistObject.transform.GetChild(i).gameObject.SetActive(true);
                engineerUI.transform.GetChild(1).gameObject.SetActive(false);
            }
            botanistUI.transform.GetChild(botanistLevel-1).gameObject.SetActive(true);
        }
        mechanicUI.transform.GetChild(1).gameObject.transform.GetChild(thrusterLevel).gameObject.SetActive(true);
        mechanicUI.transform.GetChild(2).gameObject.transform.GetChild(armourLevel).gameObject.SetActive(true);
        mechanicUI.transform.GetChild(3).gameObject.transform.GetChild(tetherLevel).gameObject.SetActive(true);
    } 
    void Update(){
        if (solarPanelObject.transform.localScale.x <= 1.1){
            Vector3 STemp = solarPanelObject.transform.localScale;
            STemp.x += 0.01f;
            solarPanelObject.transform.localScale = STemp;
        }
    }
    void OnApplicationQuit(){
        Debug.Log("PlayStopped");
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        gameClosed = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        Save();
    }
    void OnApplicationPause(bool pause)
    {
        if(pause){
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            gameClosed = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            Save();
        }
    }

    public void Save(){
        float health = 100;
        float volume = FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().getVolume();
        float music = FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().getMusic();
        
        List<Items.ItemType> itemTypes = shipInventory.GetItemTypes();
        List<int> amounts = shipInventory.GetAmounts();
        // string itemTypes = shipInventory.GetItemTypeString();
        // string amounts = shipInventory.GetAmountString();
        string test = "You suck at Coding!";
        int currentInt = this.gameObject.GetComponent<Regenerate>().GetCurrent();
        int max = this.gameObject.GetComponent<Regenerate>().GetMax();
        int capsuleNum = this.gameObject.GetComponent<Regenerate>().GetCapsuleNum();
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
            string saveString = File.ReadAllText(Application.persistentDataPath + "/save.txt");
            Debug.Log("Loaded: " + saveString);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustVolume(saveObject.volume);
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustMusic(saveObject.music);
            music.value = saveObject.music;
            volume.value = saveObject.volume;
            for(int i = 0; i < saveObject.itemTypes.Count; i++){
                shipInventory.AddItemToShip(new Items{itemType = saveObject.itemTypes[i], amount = saveObject.amounts[i]});
            }
            this.gameObject.GetComponent<Regenerate>().SetCurrent(saveObject.currentInt);
            this.gameObject.GetComponent<Regenerate>().max = saveObject.max;
            this.gameObject.GetComponent<Regenerate>().capsuleNum = saveObject.capsuleNum;
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
            isFirst = saveObject.sFirst;
        }else{
            Debug.Log("No Save File!");
        }
    }

    public void SetInventoryPos(RectTransform shipInventory){
        shipInventory.anchoredPosition = new Vector2(-266, -215);
    }

    public void BackInventoryPos(RectTransform shipInventory){
        shipInventory.anchoredPosition = new Vector2(-266, 506);
    }

    public void SetBackCoordinate(GameObject UI){
        backUI = UI;
    }

    public void GetBackCoordinate(GameObject UI){
        if(backUI == UI){
            UI.SetActive(true);
        }
    }

    public void craft(RequiredItemsUI needed){
        int enough = 0;
        foreach(Items neededItem in needed.GetRequiredItems().requiredItems){
            foreach(Items inventoryItems in shipInventory.GetShipItemList()){
                if(inventoryItems.itemType == neededItem.itemType){
                    if(inventoryItems.amount >= neededItem.amount){
                        enough += 1;
                    }
                }
            }
        }
        if(enough >= needed.GetRequiredItems().requiredItems.Count){
            Debug.Log("Crafted!");
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().Play("Select");
            repairedSystem(needed.GetRepairedType(), needed.GetUpgradeLevel());
            requiredItemsUI.SetActive(false);
            inventoryUI.SetActive(false);
            completeUI.SetActive(true);
            foreach(Items neededItem in needed.GetRequiredItems().requiredItems){
                foreach(Items inventoryItems in shipInventory.GetShipItemList().ToArray()){
                    if(inventoryItems.itemType == neededItem.itemType){
                        if(inventoryItems.amount >= neededItem.amount){
                            print("Hello");
                            shipInventory.RemoveItemFromShip(neededItem);
                        }
                    }
                }
            }
        }else{
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().Play("Disabled");
            insufficient.SetActive(true);
            Debug.Log("ERROR: Insufficient Amount");
        }
    }

    public void unlockSolarPanel(){
        completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "REPAIRED!";
        solarPanelObject.SetActive(true);
        Vector3 STemp = solarPanelObject.transform.localScale;
        STemp.x = 0;
        solarPanelObject.transform.localScale = STemp;
        engineerUI.transform.GetChild(3).gameObject.SetActive(false);
        solarPanel = true;
    }
    public void unlockThruster(){
        completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "REPAIRED!";
        thrusterObject.SetActive(true);
        engineerUI.transform.GetChild(5).gameObject.SetActive(false);
        thruster = true;
    }
    public void unlockElectricity(){
        completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "REPAIRED!";
        electricityObject.SetActive(true);
        engineerUI.transform.GetChild(4).gameObject.SetActive(false);
        electricity = true;
    }
    public void unlockArmoury(){
        completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "REPAIRED!";
        armouryObject.SetActive(true);
        engineerUI.transform.GetChild(2).gameObject.SetActive(false);
        suitRoom = true;
    }
    public void upgradeBotanist(int level){
        completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "UPGRADED!";
        botanistObject.SetActive(true);
        botanistObject.transform.GetChild(level-1).gameObject.SetActive(true);
        botanistLevel = level;
        engineerUI.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.GetComponent<Regenerate>().SetLevel(level);
        if(level - 2 >= 0){
            botanistUI.transform.GetChild(level-2).gameObject.SetActive(false);
        }else{
            completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "REPAIRED!";
        }
        botanistUI.transform.GetChild(level-1).gameObject.SetActive(true);
    }
    public void upgradeSuitThruster(int level){
        completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "UPGRADED!";
        thrusterLevel = level;
        if(level - 1 >= 0){
            mechanicUI.transform.GetChild(1).gameObject.transform.GetChild(level-1).gameObject.SetActive(false);
        }
        mechanicUI.transform.GetChild(1).gameObject.transform.GetChild(level).gameObject.SetActive(true);
    }
    public void upgradeSuitArmour(int level){
        completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "UPGRADED!";
        armourLevel = level;
        if(level - 1 >= 0){
            mechanicUI.transform.GetChild(2).gameObject.transform.GetChild(level-1).gameObject.SetActive(false);
        }
        mechanicUI.transform.GetChild(2).gameObject.transform.GetChild(level).gameObject.SetActive(true);
    }
    public void upgradeSuitTether(int level){
        completeUI.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = "UPGRADED!";
        tetherLevel = level;
        if(level - 1 >= 0){
            mechanicUI.transform.GetChild(3).gameObject.transform.GetChild(level-1).gameObject.SetActive(false);
        }
        mechanicUI.transform.GetChild(3).gameObject.transform.GetChild(level).gameObject.SetActive(true);
    }

    public void repairedSystem(RequiredItems.RepairType repairType, int upgradeLevel){
        Debug.Log("Repaired: "+repairType);
        switch(repairType){
            case RequiredItems.RepairType.SolarPanel:   unlockSolarPanel();                  break;
            case RequiredItems.RepairType.Thruster:     unlockThruster();                    break;
            case RequiredItems.RepairType.Electricity:  unlockElectricity();                 break;
            case RequiredItems.RepairType.Armoury:      unlockArmoury();                     break;
            case RequiredItems.RepairType.Botanist:     upgradeBotanist(upgradeLevel);       break;
            case RequiredItems.RepairType.SuitArmour:   upgradeSuitArmour(upgradeLevel);     break;
            case RequiredItems.RepairType.SuitThruster: upgradeSuitThruster(upgradeLevel);   break;
            case RequiredItems.RepairType.SuitTether:   upgradeSuitTether(upgradeLevel);     break;
        }  
        
    }
    public void AdjustVolume(float value){
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustVolume(value);
    }
    public void AdjustMusic(float value){
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustMusic(value);
    }
    public void Play(string name){
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().Play(name);
    }
    public void Stop(string name){
        FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().Stop(name);
    }
    public void LoadLevel(int sceneIndex){
        if (GetComponent<Regenerate>().capsuleNum > 0){
            StartCoroutine(LoadAsynchronously(sceneIndex));
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().Play("Select");
        }else{
            insufficientCapsule.SetActive(true);
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().Play("Disabled");
        }
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
