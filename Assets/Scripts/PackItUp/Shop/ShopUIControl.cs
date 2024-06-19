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

        public void Purchase(ref ShopOption optionValues)
        {
            var _cost = optionValues.ObjectValue;
            // must check that current coin amount >= cost
            if (GameManager.Instance.GetShop().ReturnCoinTotal() >= _cost && optionValues.ObjectImage == null)
            {
                OnPurchase?.Invoke(this, optionValues);
                // Set options to default since this item cannot be purchased twice
                optionValues.ObjectValue = 0;
                optionValues.ObjectDescription.text = "";
                optionValues.ObjectImage = null;
            }
        }

        
        public void Continue()
        {
            OnContinue?.Invoke(this, null);
        }
    }
}