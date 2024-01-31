using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.IO;

public class PlayerScriptTouch : MonoBehaviour
{
    public bool isFirst = true;
    public HealthScript healthScript;
    public bool collided = false;
    public bool pressed = false;
    public bool ended = false;
    private Inventory itemInventory;
    private ShipInventory shipInventory;
    private int i = 0;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private ShipInventory_UI shipInventoryUI;
    public GameObject touchButtons;
    public GameObject backToShip;
    public GameObject damageUI;
    public GameObject inventoryFull;
    public GameObject redWall;
    public GameObject miniMap;
    public GameObject introUI;
    public AsteroidMovement asteroidMovement;
    private bool collected = false;

    //ShipHub Data (Needed)
    public int capsuleNumR = 10;
    public int armourLevel = 0;
    public int thrusterLevel = 0;
    public int tetherLevel = 0;
    //ShipHub Data (Not needed)
    private int currentIntR = 0;
    private int maxR = 3600;
    private bool isHubFirst = false;
    private bool isGameEnded = false;
    private int gameClosed;
    public Slider music;
    public Slider volume;
    
    // Start is called before the first frame update
    void Awake(){
        LoadLevel();
        TetherArea(tetherLevel);
        Debug.Log("Called!");
    }
    
    void Start()
    {
        itemInventory = new Inventory();
        Debug.Log(itemInventory);
        shipInventory = new ShipInventory();
        uiInventory.SetInventory(itemInventory);
        shipInventoryUI.SetShipInventory(shipInventory);
        Load();
        if (isFirst == true){
            introUI.SetActive(true);
            isFirst = false;
        }
        Save();
        healthScript.HealthLevel(armourLevel);
        FindObjectOfType<JoystickMovement1>().GetComponent<JoystickMovement1>().SpeedLevel(thrusterLevel);
        Debug.LogFormat("Loaded inventory {0}", JsonUtility.ToJson(shipInventory));
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Touch")){
            pressed = true;
        }
        if (CrossPlatformInputManager.GetButtonUp("Touch")){
            pressed = false;
            collected = false;
        }
        if (touchButtons.transform.GetChild(0).gameObject.activeSelf == true){
            CrossPlatformInputManager.SetButtonUp("Touch");
            collected = false;
        }
        if (CrossPlatformInputManager.GetButtonDown("Exit")){
            ended = true;
        }
        if (CrossPlatformInputManager.GetButtonUp("Exit")){
            ended = false;
        }
        if (touchButtons.transform.GetChild(0).gameObject.activeSelf == true){
            CrossPlatformInputManager.SetButtonUp("Exit");
        }
    }

    public Items GetItems(Items.ItemType collectableType){
        return new Items{itemType = collectableType, amount = 1};
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // if (collision.CompareTag("Collectable"))
        // {
        //     touchButtons.transform.GetChild(0).gameObject.SetActive(false);
        //     touchButtons.transform.GetChild(1).gameObject.SetActive(true);
        //     if (pressed == true){
        //         CollectItem(collision);
        //     }
        // }

        if (collision.CompareTag("Collector"))
        {
            shipInventoryUI.gameObject.SetActive(true);
            if (itemInventory.GetItemList().Count > 0){
                touchButtons.transform.GetChild(0).gameObject.SetActive(false);
                touchButtons.transform.GetChild(2).gameObject.SetActive(true);
            }
            if (itemInventory.GetItemList().Count <= 0){
                touchButtons.transform.GetChild(0).gameObject.SetActive(false);
                touchButtons.transform.GetChild(3).gameObject.SetActive(true);
            }
            
            if (pressed == true){
                StoreItem(collision);
                if (collected == false){
                    FindObjectOfType<AudioManager>().Play("PickUp");
                    collected = true;
                }
                CrossPlatformInputManager.SetButtonUp("Touch");
                touchButtons.transform.GetChild(2).gameObject.SetActive(false);
            }
            if (ended == true){
                backToShip.SetActive(true);
                FindObjectOfType<AudioManager>().Play("Blip");
                FindObjectOfType<AudioManager>().Stop("Warning");
                FindObjectOfType<AudioManager>().Stop("Breathing");
                FindObjectOfType<AudioManager>().Stop("PlayerHit");
                CrossPlatformInputManager.SetButtonUp("Exit");
                Time.timeScale = 0f;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Enemy")){
            float aSpeed = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
            float mass = collision.gameObject.transform.localScale.x;
            healthScript.Damage(Mathf.Round(Mathf.Abs((aSpeed * aSpeed) * mass * 0.05f)));
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            damageUI.SetActive(false);
            damageUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        // if (collision.CompareTag("Collectable")){
        //     touchButtons.transform.GetChild(1).gameObject.SetActive(false);
        //     touchButtons.transform.GetChild(0).gameObject.SetActive(true);
        //     pressed = false;
        // }
        if (collision.CompareTag("Collector")){
            shipInventoryUI.gameObject.SetActive(false);
            touchButtons.transform.GetChild(2).gameObject.SetActive(false);
            touchButtons.transform.GetChild(3).gameObject.SetActive(false);
            touchButtons.transform.GetChild(0).gameObject.SetActive(true);
            backToShip.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void CollectItem(Collider2D collision){
        Items.ItemType collectableType = collision.gameObject.GetComponent<CollectableScript>().CollectableType;
        if (!itemInventory.isInventoryFull(new Items{itemType = collectableType, amount = 1})){
            itemInventory.AddItem(new Items{itemType = collectableType, amount = 1});
            FindObjectOfType<AudioManager>().Play("PickUp");
            Destroy(collision.gameObject);
        }else{
            if (collected == false){
                FindObjectOfType<AudioManager>().Play("Disabled");
                inventoryFull.SetActive(false);
                inventoryFull.SetActive(true);
                collected = true;
            }
        }
    }
    public void StoreItem(Collider2D collision){
        foreach (Items item in itemInventory.GetItemList()){
            shipInventory.AddItemToShip(item);
        }
        itemInventory.ClearItemList();
    }

    
    // private class SaveObject{
    //     public float health;
    //     public float volume;
    //     public float music;
    //     public List<Items.ItemType> itemTypes;
    //     public List<int> amounts;
    //     // public string itemTypes;
    //     // public string amounts;
    //     public string test;
    // }

    public void Save(){
        float health = FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().maxHealth;
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
        bool solar = FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().GetSolarPanel();
        bool thrus = FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().GetThruster();
        bool elec = FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().GetElectricity();
        bool sr = FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().GetSuitRoom();
        int bl = FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().GetBotanistLevel();
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

            FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().maxHealth = saveObject.health;
            FindObjectOfType<HealthScript>().gameObject.GetComponent<HealthScript>().health = saveObject.health;
            Debug.Log(saveObject.volume + saveObject.music);
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustVolume(saveObject.volume);
            FindObjectOfType<AudioManager>().gameObject.GetComponent<AudioManager>().AdjustMusic(saveObject.music);
            Debug.Log(saveObject.volume + saveObject.music);
            volume.value = saveObject.volume;
            music.value = saveObject.music;
            currentIntR = saveObject.currentInt;
            maxR = saveObject.max;
            capsuleNumR = saveObject.capsuleNum;
            isHubFirst = saveObject.hubFirst;
            FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().SetSolarPanel(saveObject.solar);
            FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().SetThruster(saveObject.thrus);
            FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().SetElectricity(saveObject.elec);
            FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().SetSuitRoom(saveObject.sr);
            FindObjectOfType<ShipStatus>().gameObject.GetComponent<ShipStatus>().SetBotanistLevel(saveObject.bl);
            isGameEnded = saveObject.end;
            armourLevel = saveObject.al;
            thrusterLevel = saveObject.thl;
            tetherLevel = saveObject.tel;
            gameClosed = saveObject.closed;
            for(int i = 0; i < saveObject.itemTypes.Count; i++){
                shipInventory.AddItemToShip(new Items{itemType = saveObject.itemTypes[i], amount = saveObject.amounts[i]});
            }
            isFirst = saveObject.sFirst;
        }else{
            Debug.Log("No Save File!");
        }
    }

        private void LoadLevel(){
        if(File.Exists(Application.persistentDataPath + "/save.txt")){
            string saveString = File.ReadAllText(Application.persistentDataPath + "/save.txt");
            Debug.Log("Loaded: " + saveString);
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            armourLevel = saveObject.al;
            thrusterLevel = saveObject.thl;
            tetherLevel = saveObject.tel;
        }else{
            Debug.Log("No Save File!");
        }
    }

    private void TetherArea(int level){
        FindObjectOfType<spawnAsteroids>().transform.localScale = new Vector3(100 * ((5.0f+level)/5.0f), 100 * ((5.0f+level)/5.0f), 1);
        FindObjectOfType<ItemSpawnerScript>().gameObject.GetComponent<ItemSpawnerScript>().numberSpawn = Mathf.RoundToInt(FindObjectOfType<ItemSpawnerScript>().gameObject.GetComponent<ItemSpawnerScript>().numberSpawn * ((5.0f+level)/5.0f));
        asteroidMovement.fadeZone *= ((5.0f+level)/5.0f);
        redWall.transform.localScale = new Vector3(1 * ((5.0f+level)/5.0f), 1 * ((5.0f+level)/5.0f), 1.0f);
        miniMap.transform.position = new Vector3(0, 0, -41.5f * ((5.0f+level)/5.0f));
    }
}
