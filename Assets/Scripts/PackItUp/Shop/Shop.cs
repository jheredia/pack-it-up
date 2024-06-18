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
        //private List<GameObject> _itemsForSale;
        
        [SerializeField] private Canvas _shopUI;
        [SerializeField] private ShopUIControl _shopController;

        public event EventHandler<int> OnShopExit;

        private int _coinTotal;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _inventory = _gameManager.GetInventory();
        }

        private void OnEnable()
        {
            // disable player controller
            _gameManager.OnShopOpen += OpenUIShop;
            //_inventory.OnItemsRequest += RestockShop;
            _shopController.OnPurchase += ReduceCoinTotal;
        }

        private void OnDisable()
        {
            _gameManager.OnShopOpen -= OpenUIShop;
            _shopController.OnPurchase -= ReduceCoinTotal;
            // enable player controller
        }

        public void RestockCoins()
        {
            // Get updated coin values and missing items
            _coinTotal = _inventory.GetCoins();
        }

        public void OpenUIShop(object sender, EventArgs e)
        {
            RestockCoins();
            _shopUI.enabled = true;
        }
        

        public List<GameObject> GetMissingItems()
        {
            return _inventory.GetMissingItems();
        }

        public void ReduceCoinTotal(object sender, ShopOption option)
        {
            _coinTotal -= option.GetCost();
        }

        // Return current coin amount for comparing prices of objects
        public int ReturnCoinTotal()
        {
            return _coinTotal;
        }
    }
}