using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_PickupBehaviourHandler : MonoBehaviour
{
    public List<PickupBehaviour> behaviours;
    public void WhenPickedUp() { foreach (var behaviour in behaviours) { behaviour.OnPickupDoThis(); } }
}
