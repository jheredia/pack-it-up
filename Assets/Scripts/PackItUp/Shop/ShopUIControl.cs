using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIControl : MonoBehaviour
{
    public event EventHandler<int> OnPurchase;
    
    public void Purchase(string cost)
    {
        var _cost = int.Parse(cost);
        OnPurchase?.Invoke(this, _cost);
    }
}
