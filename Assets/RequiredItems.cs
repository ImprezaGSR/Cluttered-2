using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredItems : MonoBehaviour
{

    public enum RepairType{
        SolarPanel,
        Thruster,
        Electricity,
        Armoury,
        Botanist,
        SuitThruster,
        SuitArmour,
        SuitTether,
    }
    public RepairType repairType;
    public event EventHandler OnRequiredListChanged;
    public List<Items> requiredItems;
    public int upgradeLevel = 1;
    public List<Items> GetRequiredItems(){
        return requiredItems;
    }

    public bool IsUpgrade(){
        switch (repairType){
            default:
            case RepairType.SolarPanel:
            case RepairType.Thruster:
            case RepairType.Electricity:
            case RepairType.Armoury:
            case RepairType.Botanist:
                return true;
            case RepairType.SuitArmour:
                return true;
            case RepairType.SuitThruster:
                return true;
            case RepairType.SuitTether:
                return true;
        }  
    }
}
