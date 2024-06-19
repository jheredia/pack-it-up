using UnityEngine;

/* This is an example of a basic PickupBehaviour. It inherits from PickupBehaviour, which is a Unity Monobehaviour also using the IPickupBehaviour interface.
 * It is intended to a be a true Monobehaviour script - that is, it is intended to do one thing, such as play a sound effect or begin a particle system.
 * These should be added to the List<PickupBehaviour> in the Behaviours Object childed to the Pickup Handling Object */

public class PickupBehaviour_DebugTest : PickupBehaviour
{
    public override void OnPickupDoThis()
    {
        // Debug.Log("PICK UP BEHAVIOUR TEST.");
    }
}
