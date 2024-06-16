using System.Collections.Generic;
using UnityEngine;

namespace PackItUp.Inventory
{
    public class InventoryController : Singleton<InventoryController>
    {
        [SerializeField] SOMapRequirementConfig soMapRequirementConfig;

        InventoryData data;

        public InventoryUI UI { get; set; } = null;

        protected override void OnInit()
        {
            data = new();

            //load default map config
            LoadMapData(1);
        }

        public void AddItem(int itemId, int level = 1, int quantity = 1)
        {
            data.AddItem(itemId, level, quantity);

            if (UI != null && UI.isActiveAndEnabled)
            {
                UI.UpdateData();
            }
        }

        public void RemoveItem(int itemId, int level = 1)
        {
            data.RemoveItem(itemId, level);

            if (UI != null && UI.isActiveAndEnabled)
            {
                UI.UpdateData();
            }
        }

        public bool UseItem(int itemId, int level = 1, int quantity = 1)
        {
            bool used = data.UseItem(itemId, level, quantity);

            if (UI != null && UI.isActiveAndEnabled)
            {
                UI.UpdateData();
            }
            return used;
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
            data.RequiredItemList = soMapRequirementConfig.GetRequirement(mapId).requiredItemList;
            data.InitMandatoryItems();
        }

        public int GetRequiredItemQuantity(int itemId, int level)
        {
            return data.GetRequiredItemQuantity(itemId, level);
        }

        public List<ItemData> GetItemDataList()
        {
            return data.CurrentList;
        }
    }
}