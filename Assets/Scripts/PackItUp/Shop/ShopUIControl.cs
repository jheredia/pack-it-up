using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackItUp.Shop
{
    public class ShopUIControl : MonoBehaviour
    {
        public event EventHandler<ShopOption> OnPurchase;
        public event EventHandler OnContinue;

        public void Purchase(ShopOption optionValues)
        {
            var _cost = optionValues.ObjectValue;
            // must check that current coin amount >= cost
            Debug.Log(_cost);
            if (GameManager.Instance.GetShop().ReturnCoinTotal() >= _cost && optionValues.ObjectImage != null)
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
            OnContinue?.Invoke(this, null);
        }

        public void ExitGame()
        {
            Debug.Log("Exit Game");
        }
    }
}