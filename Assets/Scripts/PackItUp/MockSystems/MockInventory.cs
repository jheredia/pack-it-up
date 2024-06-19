using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockInventory : MonoBehaviour
{
    public EventHandler OnKeyItemsCollected;
    public EventHandler<List<GameObject>> OnItemsRequest;
    private int _coinTotal = 20;

    public void AcceptPickupData(CharacterPickupPair pair)
    {
        Debug.Log("test");
        StopAllCoroutines();
        //image.sprite = pair.pickupData.bigSprite;
        StartCoroutine(StopDisplayingSprite());
    }

    public IEnumerator StopDisplayingSprite()
    {
        yield return new WaitForSeconds(1);
        //image.sprite = null;
    }

    private void OnEnable()
    {
        //GameManager.Instance.GetShop().OnShopExit += UpdateCoinTotal;
    }

    private void OnDisable()
    {
        //GameManager.Instance.GetShop().OnShopExit -= UpdateCoinTotal;
    }

    public void UpdateCoinTotal(object sender, int value)
    {
        // Update coin value in either GameManager or inventory
        _coinTotal = value;
    }

    public List<GameObject> GetMissingItems()
    {
        List<GameObject> _missingItems = new List<GameObject>();


        return _missingItems;
    }

    // Return current amount of coins
    public int GetCoins()
    {
        return _coinTotal;
    }
}
