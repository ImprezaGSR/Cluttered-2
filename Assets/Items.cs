using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class Items
{
    // Start is called before the first frame update
    public enum ItemType{
        Cube,
        Sphere,
        Capsule,
        Asteroid,
        SolarPanel,
        Cog,
        CopperCog,
        Pipe,
        CopperPipe,
        Plane,
        CopperPlane,
    }

    public ItemType itemType;

    public int amount;

    public bool isFull;

    public Sprite GetSprite(){
        switch (itemType) {
            default:
            case ItemType.Cube: return ItemAsset.Instance.Cube;
            case ItemType.Sphere: return ItemAsset.Instance.Sphere;
            case ItemType.Capsule: return ItemAsset.Instance.Capsule;
            case ItemType.Asteroid: return ItemAsset.Instance.Asteroid;
            case ItemType.SolarPanel: return ItemAsset.Instance.SolarPanel;
            case ItemType.Cog: return ItemAsset.Instance.Cog;
            case ItemType.CopperCog: return ItemAsset.Instance.CopperCog;
            case ItemType.Pipe: return ItemAsset.Instance.Pipe;
            case ItemType.CopperPipe: return ItemAsset.Instance.CopperPipe;
            case ItemType.Plane: return ItemAsset.Instance.Plane;
            case ItemType.CopperPlane: return ItemAsset.Instance.CopperPlane;
        }
    }

    public bool IsStackable(){
        switch (itemType){
            default:
            case ItemType.Cube:
            case ItemType.Sphere:
            case ItemType.Cog:
            case ItemType.CopperCog:
            case ItemType.Pipe:
            case ItemType.CopperPipe:
            case ItemType.Plane:
            case ItemType.CopperPlane:
                return true;
            case ItemType.Capsule:
            case ItemType.Asteroid:
            case ItemType.SolarPanel:
                return false;
        }  
    }
    public bool IsFull(){
        if (amount < 10){
            return false;
        }else{
            return true;
        }
    }
}
