using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.Instance.SetCanvas(gameObject);
    }

    public void ShowInventory()
    {
        //todo pause timer
        UIManager.Instance.ShowPopup("Prefabs/InventoryUI");
    }
}
