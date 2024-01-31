using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipInventory_UI : MonoBehaviour
{
    private ShipInventory inventory;
    private Transform shipInventoryUI;
    private Transform sidePanel;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public int xLimit = 3;

    private void Awake(){
        // this.gameObject.SetActive(false);
        sidePanel = transform.Find("SidePanel");
        shipInventoryUI = sidePanel.Find("ShipInventoryUI");
        itemSlotContainer = shipInventoryUI.Find("ShipItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ShipItemSlotTemplate");
    }
    public void SetShipInventory(ShipInventory inventory){
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e){
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems(){
        foreach (Transform child in itemSlotContainer){
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 85f;
        foreach (Items item in inventory.GetShipItemList()){
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.localPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1){
                uiText.SetText(item.amount.ToString());
            }else{
                uiText.SetText("");
            }
            x++;
            if (x >= xLimit){
                x = 0;
                y--;
            }
        }
    }
}
