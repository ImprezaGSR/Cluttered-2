using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;
    public int amountLimit = 10;
    public int boxLimit = 16;
    private List<Items> itemList;
    public Inventory(){
        itemList = new List<Items>();
    }
    public void AddItem(Items item){
        if(itemList.Count < boxLimit || item.IsStackable()){
            if (item.IsStackable()){
                bool itemAlreadyInInventory = false;
                foreach (Items inventoryItem in itemList){
                    if (inventoryItem.itemType == item.itemType && !inventoryItem.IsFull()) {
                        inventoryItem.amount += item.amount;
                        itemAlreadyInInventory = true;
                    }
                }
                if (!itemAlreadyInInventory && itemList.Count < boxLimit){
                    itemList.Add(item);
                }else if(!itemAlreadyInInventory && itemList.Count >= boxLimit){
                    print("You can't hold items anymore!");
                }
            } else{
                itemList.Add(item);
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }else{
            print("You can't hold items anymore!");
        }
    }

    public void RemoveItem(Items item){
        if (item.IsStackable()){
            Items itemInInventory = null;
            foreach (Items inventoryItem in itemList){
                if (inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0){
                itemList.Remove(item);
            }
        } else{
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    // public int RemoveAllItems(){
    //     itemList.RemoveAll();
    // }

    public List<Items> GetItemList(){
        return itemList;
    }

    public void ClearItemList(){
        itemList.Clear();
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool isInventoryFull(Items item){
        if(itemList.Count < boxLimit || item.IsStackable()){
            if (item.IsStackable()){
                bool itemAlreadyInInventory = false;
                foreach (Items inventoryItem in itemList){
                    if (inventoryItem.itemType == item.itemType && !inventoryItem.IsFull()) {
                        return false;
                    }
                }
                if (!itemAlreadyInInventory && itemList.Count < boxLimit){
                    return false;
                }else if(!itemAlreadyInInventory && itemList.Count >= boxLimit){
                    return true;
                }
            } else{
                return false;
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }else{
            return true;
        }
        return false;
    }
}
