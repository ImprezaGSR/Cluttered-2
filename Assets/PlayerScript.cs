using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public HealthScript healthScript;
    public bool collided = false;
    public bool pressed = false;
    private Inventory itemInventory;
    private ShipInventory shipInventory;
    private int i = 0;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private ShipInventory_UI shipInventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        itemInventory = new Inventory();
        shipInventory = new ShipInventory();
        uiInventory.SetInventory(itemInventory);
        shipInventoryUI.SetShipInventory(shipInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            pressed = true;
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            pressed = false;
        }
        if(Input.GetKey(KeyCode.Q)){
            healthScript.Damage(9999);
        }
       
    }

    public Items GetItems(Items.ItemType collectableType){
        return new Items{itemType = collectableType, amount = 1};
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable") && pressed == true)
        {
            CollectItem(collision);
        }

        if (collision.CompareTag("Collector"))
        {
            shipInventoryUI.gameObject.SetActive(true);
            if (pressed == true && itemInventory.GetItemList().Count > 0){
                StoreItem(collision);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Enemy")){
            float randomX = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            float randomY = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            float mass = collision.gameObject.transform.localScale.x;
            healthScript.Damage(Mathf.Round(Mathf.Abs(randomX * randomY * mass)));
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Collector")){
            shipInventoryUI.gameObject.SetActive(false);
        }
    }

    public void CollectItem(Collider2D collision){
        Items.ItemType collectableType = collision.gameObject.GetComponent<CollectableScript>().CollectableType;
        itemInventory.AddItem(new Items{itemType = collectableType, amount = 1});
        Destroy(collision.gameObject);
    }
    public void StoreItem(Collider2D collision){
        foreach (Items item in itemInventory.GetItemList()){
            shipInventory.AddItemToShip(item);
        }
        itemInventory.ClearItemList();
    }

    // public bool touch(){
    //     return pressed;
    // }

}
