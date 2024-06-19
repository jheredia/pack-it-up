using System;
using System.Collections.Generic;
using UnityEngine;
using PackItUp.Managers;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
using TMPro;

namespace PackItUp.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private ShopUIControl _shopController;
        private Shop _shop;
        [SerializeField] private List<ShopOption> _shopOptions;
        [SerializeField] private TMP_Text _coinText;

        private List<GameObject> _souvenirsForSale;
        private List<PickupData> _itemsForSale;

        public event EventHandler<int> OnClearOptions;
        private int _tempCoinTotal;
        private string _startingCoinText;

        private void Awake()
        {
            _startingCoinText = _coinText.text;
            _shop = GameManager.Instance.GetShop();
        }

        private void OnEnable()
        {
            // Restock shop after level completion and get updated coin amount for UI
            _itemsForSale = _shop.GetMissingItems();
            _tempCoinTotal = _shop.ReturnCoinTotal();
            _coinText.text = _startingCoinText + _tempCoinTotal.ToString();
            RestockShop();

            _shopController.OnContinue += ClearOptions;
            _shopController.OnPurchase += UpdateTempCoinTotal;
        }

        private void OnDisable()
        {
            _shopController.OnContinue -= ClearOptions;
            _shopController.OnPurchase -= UpdateTempCoinTotal;
        }

        public void RestockShop()
        {
            // Get buff/debuff items that were missed in the previous stage
            Debug.Log("Restocking Shop");
            foreach (ShopOption _option in _shopOptions)
            {
                // Fill items row with missed items
                Debug.Log(_itemsForSale.Count);
                if (_option.GetItemType() && _itemsForSale.Count > 0)
                {
                    _option.enabled = true;
                    _option.AddObject(_itemsForSale[0]);
                    _itemsForSale.RemoveAt(0);
                }
            }
        }

        public void ClearOptions(object sender, EventArgs e)
        {
            // Disable all shop options
            foreach (ShopOption _option in _shopOptions)
            {
                _option.enabled = false;
            }

            // Send recent coin total after purchases
            OnClearOptions?.Invoke(this, _tempCoinTotal);

            gameObject.SetActive(false);
        }

        public void UpdateTempCoinTotal(object sender, ShopOption option)
        {
            _tempCoinTotal -= option.ObjectValue;
            _coinText.text = _startingCoinText + _tempCoinTotal.ToString();
        }
    }
}
