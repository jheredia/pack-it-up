using PackItUp.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : BasePopup
{
    [SerializeField] GameObject container;

    [SerializeField] GameObject itemPrefab;

    List<InventoryItem> itemList = new();

    public void OnEnable()
    {
        InventoryController.Instance.UI = this;
    }

    public void OnDisable()
    {
        InventoryController.Instance.UI = null;
    }

    public override IEnumerator PushRoutine(PopupInfo info)
    {
        LoadData();
        yield return null;
    }

    public void LoadData()
    {
        foreach (Transform child in container.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<ItemData> itemDataList = InventoryController.Instance.GetItemDataList();
        for (int i = 0; i < itemDataList.Count; i++)
        {
            GameObject obj = Instantiate(itemPrefab, container.transform);
            InventoryItem item = obj.GetComponent<InventoryItem>();

            item.LoadData(itemDataList[i]);
            itemList.Add(item);
        }
    }

    public void UpdateData()
    {
        List<ItemData> itemDataList = InventoryController.Instance.GetItemDataList();
        if (itemDataList.Count > itemList.Count)
        {
            for (int i = 0; i < itemDataList.Count - itemList.Count; i++)
            {
                GameObject obj = Instantiate(itemPrefab, container.transform);
                InventoryItem item = obj.GetComponent<InventoryItem>();
                itemList.Add(item);
            }
        }
        else
        {
            for (int i = 0; i < itemDataList.Count; i++)
            {
                itemList[i].gameObject.SetActive(true);
                InventoryItem item = itemList[i];
                item.LoadData(itemDataList[i]);
            }

            for (int i = itemDataList.Count; i < itemList.Count; i++)
            {
                itemList[i].gameObject.SetActive(false);
            }
        }
    }
}
