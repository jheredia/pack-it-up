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
        print(pair);
        if (pair == null) return;
        print(pair.pickupData.isKeyItem);
        if (pair.pickupData.isKeyItem) keyItemsPickedUp++;
        print(keyItemsPickedUp);
        if (keyItemsPickedUp >= totalKeyItemsInScene)
        {
            print("All items collected");
            onAllKeyItemsPickedUp?.Invoke();
        }
    }
}
