using System.Collections.Generic;
using UnityEngine;

namespace PackItUp.Inventory
{
    public class InventoryData
    {
        List<ItemData> currentItemList = new();
        List<ItemData> requiredItemList = new();

        public void AddItem(int itemId, int level = 1, int quantity = 1)
        {
            bool exist = false;
            foreach (ItemData item in currentItemList)
            {
                if (item.itemId == itemId && item.level == level)
                {
                    item.quantity += quantity;
                    exist = true;
                    break;
                }
            }

            if (!exist)
            {
                currentItemList.Add(new(itemId, level, quantity));
            }
        }

        public void RemoveItem(int itemId, int level = 1)
        {
            foreach (ItemData item in currentItemList)
            {
                if (item.itemId == itemId && item.level == level)
                {
                    currentItemList.Remove(item);
                    return;
                }
            }

            Debug.LogWarning("InventoryController RemoveItem not exist");
        }

        public bool UseItem(int itemId, int level = 1, int quantity = 1)
        {
            foreach (ItemData item in currentItemList)
            {
                if (item.itemId == itemId && item.level == level)
                {
                    if (item.quantity >= quantity)
                    {
                        item.quantity -= quantity;
                        return true;
                    }
                    else
                    {
                        Debug.LogWarning("InventoryController UseItem not enough");
                        return false;
                    }
                }
            }

            Debug.LogWarning("InventoryController UseItem not exist");
            return false;
        }

        public bool CheckItem(int itemId, int level, int quantity)
        {
            foreach (ItemData item in currentItemList)
            {
                if (item.itemId == itemId && item.level == level)
                {
                    if (item.quantity >= quantity)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public void Reset()
        {
            currentItemList.Clear();
        }

        public bool CheckMandatoryItems()
        {
            foreach (ItemData requiredItem in requiredItemList)
            {
                if (!CheckItem(requiredItem.itemId, requiredItem.level, requiredItem.quantity))
                    return false;
            }
            return true;
        }
    }
}