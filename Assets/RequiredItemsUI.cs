using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequiredItemsUI : MonoBehaviour
{
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform requiredItemsUI;
    private Transform sidePanel;
    public int xLimit = 8;
    private RequiredItems requiredItems;

    void Awake(){
        // this.gameObject.SetActive(false);
        sidePanel = transform.Find("SidePanel");
        requiredItemsUI = sidePanel.Find("RequiredItemsUI");
        itemSlotContainer = requiredItemsUI.Find("RequiredItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("RequiredItemSlotTemplate");
    }

    public RequiredItems GetRequiredItems(){
        return requiredItems;
    }

    public RequiredItems.RepairType GetRepairedType(){
        return requiredItems.repairType;
    }

    public int GetUpgradeLevel(){
        return requiredItems.upgradeLevel;
    }

    public bool GetIsUpgrade(){
        return requiredItems.IsUpgrade();
    }
    public void SetRequiredItems(RequiredItems needed){
        requiredItems = needed;
        needed.OnRequiredListChanged += RequiredItem_OnRequiredListChanged;
        RefreshRequiredItems();
    }

    private void RequiredItem_OnRequiredListChanged(object sender, System.EventArgs e){
        RefreshRequiredItems();
    }

    private void RefreshRequiredItems(){
        foreach (Transform child in itemSlotContainer){
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 85f;
        foreach (Items item in requiredItems.GetRequiredItems()){
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
