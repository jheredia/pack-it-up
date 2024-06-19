using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PackItUp.Shop
{
    [Serializable]
    public class ShopOption : MonoBehaviour
    {
        // Values to fill for purchasable object in shop UI
        [SerializeField] private Image _objectImage;
        [SerializeField] private TMP_Text _objectDescription;
        private int _objectValue;
        [SerializeField] private TMP_Text _purchaseButtonText;
        

        public Image ObjectImage
        {
            get
            {
                return _objectImage;
            }
            set
            {
                _objectImage = value;
            }
        }

        public TMP_Text ObjectDescription
        {
            get
            {
                return _objectDescription;
            }
            set
            {
                _objectDescription = value;
            }
        }

        public int ObjectValue
        {
            get
            {
                return _objectValue;
            }
            set
            {
                _objectValue = value;
            }
        }

        public TMP_Text PurchaseButtonText
        {
            get
            {
                return _purchaseButtonText;
            }
            set
            {
                _purchaseButtonText = value;
            }
        }

        // Identifies object as item, or if not, a souvenir
        public bool _isItem = false;
        
        public bool GetItemType()
        {
            return _isItem;
        }

        public void AddObject(PickupData _newObject)
        {
            _objectImage.sprite = _newObject.bigSprite;
            _objectDescription.text = "";
            _objectValue = 4;
            _purchaseButtonText.text = _objectValue.ToString();
        }
    }
}