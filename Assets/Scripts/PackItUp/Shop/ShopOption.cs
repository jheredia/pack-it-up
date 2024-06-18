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