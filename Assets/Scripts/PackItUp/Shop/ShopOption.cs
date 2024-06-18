using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PackItUp.Shop
{
    public class ShopOption : MonoBehaviour
    {
        // Values to fill for purchasable object in shop UI
        [SerializeField] private Image _objectImage;
        [SerializeField] private TMP_Text _objectDescription;
        [SerializeField] private int _objectValue;
        
        // Identifies object as item, or if not, a souvenir
        private bool _isItem = false;
        
        public bool GetItemType()
        {
            return _isItem;
        }

        public void AddObject(GameObject _newObject)
        {
            //_objectImage = _newObject.image;
            //_objectDescription = _newObject.description;
            //_objectValue = _newObject.value;
        }
    }
}