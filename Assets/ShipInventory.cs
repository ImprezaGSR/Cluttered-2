using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class ShipInventory
{
    public event EventHandler OnItemListChanged;
    private List<Items> itemListShip;
    // private string path = Application.persistentDataPath + "/inventory.json";
    private string path { get { return Path.Combine(Application.persistentDataPath, "shipInventory.json"); } }

    public ShipInventory(){
        itemListShip = new List<Items>();
    }
    public void AddItemToShip(Items item){
        bool itemAlreadyInInventory = false;
        foreach (Items inventoryItem in itemListShip){
            if (inventoryItem.itemType == item.itemType) {
                inventoryItem.amount += item.amount;
                itemAlreadyInInventory = true;
            }
        }
        if (!itemAlreadyInInventory){
            itemListShip.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void RemoveItemFromShip(Items removeItem){
        Items item;
        bool itemAlreadyInInventory = true;
        foreach (Items inventoryItem in itemListShip.ToArray()){
            if (inventoryItem.itemType == removeItem.itemType){
                if (inventoryItem.amount > removeItem.amount){
                    inventoryItem.amount -= removeItem.amount;
                }else if (inventoryItem.amount == removeItem.amount){
                    // itemListShip.Remove(inventoryItem);
                    itemAlreadyInInventory = false;
                    item = inventoryItem;
                }
            }
        }
        if(!itemAlreadyInInventory){
            itemListShip.RemoveAll(item => item.itemType == removeItem.itemType);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Items> GetShipItemList(){
        return itemListShip;
    }
    public List<Items.ItemType> GetItemTypes(){
        List<Items.ItemType> itemTypes = new List<Items.ItemType>();
        for (int i = 0 ; i < itemListShip.Count; i++){
            itemTypes.Add(itemListShip[i].itemType);
        }
        return itemTypes;
    }
    public string GetItemTypeString(){
        List<string> itemTypes = new List<string>();
        foreach(Items item in GetShipItemList()){
            itemTypes.Add(item.itemType.ToString());
        }
        string itemTypeString = string.Join(",", itemTypes.ToArray());
        return itemTypeString;
    }

    public List<int> GetAmounts(){
        List<int> amounts = new List<int>();
        foreach(Items item in GetShipItemList()){
            amounts.Add(item.amount);
        }
        return amounts;
    }
    public string GetAmountString(){
        List<string> amounts = new List<string>();
        foreach(Items item in GetShipItemList()){
            amounts.Add(item.amount.ToString());
        }
        string amountString = string.Join(",", amounts.ToArray());
        return amountString;
    }


    // public void Save()
    // {
    //     File.WriteAllText(path, JsonUtility.ToJson(this));
    //     Debug.Log("Save Complete!");
    // }
    // // load the inventory from disk
    // public void Load()
    // {
    //     if (File.Exists(path)){
    //         JsonUtility.FromJsonOverwrite(File.ReadAllText(path), this);
    //         Debug.Log("Load Complete!");
    //     }else{
    //         Debug.Log("Load Not Completed");
    //     }
    // }

}
