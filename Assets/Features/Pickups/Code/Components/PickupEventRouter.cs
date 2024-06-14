using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEventRouter : MonoBehaviour
{
    public PickupUnityEvent onPickupPickedUp;

    public void AcceptNewObject(GameObject obj)
    {
        Component_PickupCollisionHandler component = obj.GetComponentInChildren<Component_PickupCollisionHandler>();
        if (component != null)
        {
            SubscribeToOnPickup(component);
        }
    }

    private void SubscribeToOnPickup(Component_PickupCollisionHandler component)
    {
        component.onPickup.AddListener((whatever)  => onPickupPickedUp?.Invoke(whatever));
    }
}
