using System;
using System.Collections.Generic;
using UnityEngine;
using PackItUp.Managers;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

namespace PackItUp.Shop
{
    public class Shop : MonoBehaviour
    {
        private GameManager _gameManager;
        private MockInventory _inventory;
        
        [SerializeField] private GameObject _shopUI;
        [SerializeField] private ShopUIControl _shopController;

        private int _coinTotal;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _inventory = _gameManager.GetInventory();
        }

        private void OnEnable()
        {
            _shopController.OnPurchase += ReduceCoinTotal;
            RestockCoins();
        }

        private void OnDisable()
        {
            _shopController.OnPurchase -= ReduceCoinTotal;
        }

        public void RestockCoins()
        {
            // Get updated coin values
            _coinTotal = _inventory.GetCoins();
        }

        public List<PickupData> GetMissingItems()
        {
            return _inventory.GetMissingItems();
        }

        public void ReduceCoinTotal(object sender, ShopOption option)
        {
            _coinTotal -= option.ObjectValue;
        }

        // Return current coin amount for comparing prices of objects
        public int ReturnCoinTotal()
        {
            return _coinTotal;
        }
    }
}