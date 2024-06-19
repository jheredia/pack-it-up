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
            Debug.Log("Opening Shop");
            RestockCoins();
            _shopUI.SetActive(true);
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


        /*public void CloseShopUI(object sender, int _)
        {
            _shopUI.SetActive(false);
        }*/
    }
}