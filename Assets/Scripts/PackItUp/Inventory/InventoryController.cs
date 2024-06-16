using System.Collections.Generic;
using UnityEngine;

namespace PackItUp.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        InventoryData data;

        public void AddItem(int itemId, int level = 1, int quantity = 1)
        {
            data.AddItem(itemId, level, quantity);
        }

        public void RemoveItem(int itemId, int level = 1)
        {
            data.RemoveItem(itemId, level);
        }

        public bool UseItem(int itemId, int level = 1, int quantity = 1)
        {
            return data.UseItem(itemId, level, quantity);
        }

        public void Reset()
        {
            data.Reset();
        }

        public bool CheckMandatoryItems()
        {
            return data.CheckMandatoryItems();
        }

        public void LoadMapData(int mapId)
        {
            //todo
        }
    }
}