using PackItUp.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numText;
    [SerializeField] Image icon;

    [SerializeField]
    SOItemConfig itemConfig;

    public void LoadData(ItemData data)
    {
        icon.sprite = itemConfig.GetItemConfig(data.itemId, data.level).icon;
        int requirement = InventoryController.Instance.GetRequiredItemQuantity(data.itemId, data.level);
        string numStr = requirement > 0 ? $"{data.quantity}/{requirement}" : data.quantity.ToString();
        numText.text = numStr;
    }
}
