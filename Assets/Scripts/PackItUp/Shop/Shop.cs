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
        
        private GameObject _shopUI;
        private ShopUIControl _shopController;

        public event EventHandler<int> OnShopExit;

        private int _coinTotal;

        private void Awake()
        {
            // Get Shop UI from current scene
            GetShopUI();

            _gameManager = GameManager.Instance;
            _inventory = _gameManager.GetInventory();
        }

        /*void Start()
        {
            StartCoroutine(StartRoutine());
        }

        private IEnumerator<out> StartRoutine()
        {
            yield return new WaitForSeconds(.2f);

            GetShopUI();
        }
*/
        public void GetShopUI()
        {
            // Get Shop UI from current scene
            _shopUI = GameObject.FindWithTag("ShopUI");
            Debug.Log(_shopUI);
            if (_shopUI != null)
            {
                Debug.Log("got new data");
                Debug.Log("shoprevamped");
                _shopController = _shopUI.GetComponent<ShopUIControl>();
            }
            _shopUI.SetActive(false);
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
            _shopUI.GetComponent<ShopUI>()?.RestockShop();
        }
        

        public List<PickupData> GetMissingItems()
        {
            Debug.Log(_inventory);
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