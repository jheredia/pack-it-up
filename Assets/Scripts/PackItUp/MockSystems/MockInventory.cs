using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockInventory : MonoBehaviour
{
    public EventHandler OnKeyItemsCollected;
    public EventHandler<List <GameObject>> OnItemsRequest;

    private void OnEnable()
    {
        GameManager.Instance.GetShop().OnShopExit += UpdateCoinTotal;
    }

    public void UpdateCoinTotal(object sender, int value)
    {
        // Update coin value in either GameManager or inventory
    }
}
