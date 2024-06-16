using System;

namespace PackItUp.Inventory
{
    public enum ItemType
    {
        FOOD,
        MEDICAL,
        ENERGY
    }
    [Serializable]
    public class ItemData
    {
        public int itemId;
        public int level;
        public int quantity;

        public ItemData(int itemId, int level, int quantity)
        {
            this.itemId = itemId;
            this.level = level;
            this.quantity = quantity;
        }

        public ItemData(int itemId)
        {
            this.itemId = itemId;
            this.level = 1;
            this.quantity = 1;
        }
    }
}