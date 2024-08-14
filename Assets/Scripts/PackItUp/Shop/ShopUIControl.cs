using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackItUp.Shop
{
    public class ShopUIControl : MonoBehaviour
    {
        public event EventHandler<ShopOption> OnPurchase;
        [SerializeField] private Shop _shop;

        public void Purchase(ShopOption optionValues)
        {
            var _cost = optionValues.ObjectValue;
            // must check that current coin amount >= cost
            if (_shop.ReturnCoinTotal() >= _cost && optionValues.ObjectImage != null)
            {
                OnPurchase?.Invoke(this, optionValues);
                // Set options to default since this item cannot be purchased twice
                optionValues.ObjectValue = 0;
                optionValues.ObjectDescription.text = "";
                optionValues.ObjectImage = null;
                optionValues.PurchaseButtonText.text = "Thanks";

                enabled = false;
                
            }
        }

        
        public void Continue()
        {
            Debug.Log("Continue Game");
            GameManager.Instance.AdvanceLevelAndStart();
        }

        public void ExitGame()
        {
            Debug.Log("Exit Game");
        }
    }
}