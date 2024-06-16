using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PackItUp.Inventory
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "ScriptableObjects/ItemConfig", order = 0)]
    public class SOItemConfig : ScriptableObject
    {
        public List<ItemConfig> itemList = new();

        public ItemConfig GetItemConfig(int itemId, int level)
        {
            return itemList.FirstOrDefault(x => x.itemId == itemId && x.level == level);
        }
    }
    [Serializable]
    public class ItemConfig
    {
        public int itemId;
        public string name;
        public ItemType type;
        public int level;
        public Sprite icon;
    }
}


