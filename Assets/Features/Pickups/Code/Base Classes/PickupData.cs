using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class PickupData
{
    //the name displayed to the player on UI elements or wherever
    public string displayName;
    public Sprite bigSprite;
    public bool isKeyItem = false;
    public bool isInventoryItem = false;
    public bool isStackItem = false;
    public int stackNumber = 0;

    #region Data Cloning
    public PickupData CloneData(PickupData data)
    {
        displayName = data.displayName;
        bigSprite = data.bigSprite;
        isKeyItem = data.isKeyItem;
        isInventoryItem = data.isInventoryItem;
        isStackItem = data.isStackItem;
        stackNumber = data.stackNumber;
        return this;
    }
    #endregion
}
