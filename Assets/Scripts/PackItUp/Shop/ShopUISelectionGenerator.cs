using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackItUp.Shop
{
    public class ShopUISelectionGenerator : MonoBehaviour
    {
        private List<GameObject> _selectableOptions;
        [SerializeField] private List<GameObject> _optionButtons;

        private void Awake()
        {
            _selectableOptions = new List<GameObject>();
        }

        public void RestockOptions(object sender, EventArgs e)
        {
            // fill _selectableOptions with options for items/souvenirs
            var _currentButton = _optionButtons[0];
            while (_selectableOptions.Count > 0)
            {
                if (_currentButton)
                {

                }
                else
                {
                    
                }
            }
        }
    }
}