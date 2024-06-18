using System;
using System.Collections.Generic;
using UnityEngine;
using PackItUp.Managers;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

namespace PackItUp.Shop
{
    public class ShopUI : MonoBehaviour
    {
        private GameManager _gameManager;
        private MockInventory _inventory;
        private List<GameObject> _itemsForSale;
        private List<GameObject> _souvenirsForSale;
        private List<ShopOption> _shopOptions;
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
            _gameManager.OnShopOpen += OpenShop;
            _inventory.OnItemsRequest += RestockShop;
            _shopController.OnPurchase += ReduceCoinTotal;
        }

        private void OnDisable()
        {
            _gameManager.OnShopOpen -= OpenShop;
            _inventory.OnItemsRequest -= RestockShop;
            _shopController.OnPurchase -= ReduceCoinTotal;
        }

        public void OpenShop(object sender, EventArgs e)
        {
            enabled = true;
            // TODO: disable player input
        }

        public void RestockShop(object sender, List<GameObject> itemsMissing)
        {
            // Get buff/debuff items that were missed in the previous stage
            _itemsForSale = itemsMissing;

            foreach (ShopOption _option in _shopOptions)
            {
                // Fill items row with missed items
                if (_option.GetItemType() && _itemsForSale.Count > 0)
                {
                    _option.enabled = true;
                    _option.AddObject(_itemsForSale[0]);
                    _itemsForSale.RemoveAt(0);
                }
                // Fill souvenirs row with sounevirs to buy
                else if (_souvenirsForSale.Count > 0)
                {
                    _option.enabled = true;
                    _option.AddObject(_souvenirsForSale[0]);
                    _souvenirsForSale.RemoveAt(0);
                }
            }
        }

        public void ClearOptions(object sender, EventArgs e)
        {
            // TODO: enable player input
            // Disable all shop options
            foreach (ShopOption _option in _shopOptions)
            {
                _option.enabled = false;
            }
            enabled = false;

            OnShopExit?.Invoke(this, _coinTotal);
        }

        public void ReduceCoinTotal(object sender, int value)
        {
            _coinTotal -= value;
        }
    }
}
