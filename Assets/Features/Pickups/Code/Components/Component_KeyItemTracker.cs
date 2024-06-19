using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Component_KeyItemTracker : MonoBehaviour
{
    public int totalKeyItemsInScene = 0;
    public int keyItemsPickedUp = 0;
    public UnityEvent onAllKeyItemsPickedUp;

    public void CheckNewObjectForKeyItemStatus(GameObject newObject)
    {
        Component_PickupDataHandler component = newObject.GetComponentInChildren<Component_PickupDataHandler>();
        if (component == null) return;
        PickupData data = component.workingData;
        if (data == null) return;
        if (data.isKeyItem) totalKeyItemsInScene++;
    }

    public void AcceptPickupPickedUp(CharacterPickupPair pair)
    {
        if (pair == null) return;
        if (pair.pickupData.isKeyItem) keyItemsPickedUp++;
        if (keyItemsPickedUp >= totalKeyItemsInScene) onAllKeyItemsPickedUp?.Invoke();
    }
}
