using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MockInventory : MonoBehaviour
{
    public EventHandler OnKeyItemsCollected;
    public EventHandler<List<GameObject>> OnItemsRequest;
    [SerializeField] private int _coinTotal = 20;

    private List<PickupData> _debuffItems;
    [SerializeField] public List<SO_PickupData> _SODebuffItems;
    private List<string> _debuffItemNamesMissing;

    private void Awake()
    {
        // Populate list with names of objects that buff/debuff in inspector
        _debuffItems = new List<PickupData>();
    }

    private void OnEnable()
    {
        // clear collected debuff items
        _debuffItemNamesMissing = new List<string>();

        foreach (SO_PickupData item in _SODebuffItems)
        {
            _debuffItems.Add(item.data);
        }

        foreach (PickupData item in _debuffItems)
        {
            _debuffItemNamesMissing.Add(item.displayName);
        }

        GameManager.Instance.GetShop().OnShopExit += UpdateCoinTotal;
    }

    /*private List<PickupData> GetPickupData()
    {

    }*/

    public void AcceptPickupData(PickupData pair)
    {
        Debug.Log("inventory item collection test");
        // Check if item is inventory debuff item
        if (_debuffItemNamesMissing.Contains(pair.displayName))
        {
            _debuffItemNamesMissing.Remove(pair.displayName);
        }
        // Add coins to total coin count
        else if (pair.displayName == "Coin")
        {
            _coinTotal += pair.stackNumber;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.GetShop().OnShopExit -= UpdateCoinTotal;
    }

    public void UpdateCoinTotal(object sender, int value)
    {
        // Update coin value in either GameManager or inventory
        _coinTotal = value;
    }

    // Get items missed for this level
    public List<PickupData> GetMissingItems()
    {
        List<PickupData> _missingItems = new List<PickupData>();
        foreach (PickupData pair in _debuffItems)
        {
            if (_debuffItemNamesMissing.Contains(pair.displayName))
            {
                _missingItems.Add(pair);
            }
        }
        return _missingItems;
    }

    // Return current amount of coins
    public int GetCoins()
    {
        return _coinTotal;
    }
}
