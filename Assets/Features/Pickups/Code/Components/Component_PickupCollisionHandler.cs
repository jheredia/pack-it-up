using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Component_PickupCollisionHandler : MonoBehaviour
{
    public Collider2D myCollider;
    public Component_PickupDataHandler myDataHandler;
    private bool hasBeenPickedUp = false;
    public CharacterPickupPairEvent onPickup;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (hasBeenPickedUp) return;
        if (!col.TryGetComponent(out Component_PickupHandlerCharacter pickupHandler)) return;
        hasBeenPickedUp = true;
        CharacterPickupPair pair = new CharacterPickupPair().Clone(col.gameObject, myDataHandler.dataWrapper.data);
        onPickup?.Invoke(pair);
    }
}
