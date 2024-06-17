using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupBehaviour : MonoBehaviour, IPickupBehaviour
{
    public virtual void OnPickupDoThis() { }
}
