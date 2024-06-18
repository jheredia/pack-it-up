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
        private List<GameObject> _itemsForSale;

        public event EventHandler<int> OnClearOptions;
        private int _tempCoinTotal;

        private void Awake()
        {
            _shop = GameManager.Instance.GetShop();
        }

        private void OnEnable()
        {
            //RestockShop(List < GameObject > itemsMissing)
            _itemsForSale = _shop.GetMissingItems();
            _shopController.OnContinue += ClearOptions;
            _shopController.OnPurchase += UpdateTempCoinTotal;
        }

        private void OnDisable()
        {
            _shopController.OnContinue -= ClearOptions;
            _shopController.OnPurchase += UpdateTempCoinTotal;
        }

        public void RestockShop(List<GameObject> itemsMissing)
        {
            // Get buff/debuff items that were missed in the previous stage
            foreach (ShopOption _option in _shopOptions)
            {
                // Fill items row with missed items
                if (_option.GetItemType() && itemsMissing.Count > 0)
                {
                    _option.enabled = true;
                    _option.AddObject(itemsMissing[0]);
                    itemsMissing.RemoveAt(0);
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
            // Disable all shop options
            foreach (ShopOption _option in _shopOptions)
            {
                _option.enabled = false;
            }

            // Send recent coin total after purchases
            OnClearOptions?.Invoke(this, _tempCoinTotal);

            enabled = false;
        }

        public void UpdateTempCoinTotal(object sender, ShopOption option)
        {
            _tempCoinTotal -= option.ObjectValue;
            _coinText.text = _tempCoinTotal.ToString();
        }
    }
}
