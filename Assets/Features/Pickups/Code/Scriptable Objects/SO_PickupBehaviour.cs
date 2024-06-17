using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data/Pickups/Behaviours", order = 1)]

public class SO_PickupBehaviour : ScriptableObject
{
    public IPickupBehaviour behaviour;
}
