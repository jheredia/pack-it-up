using UnityEngine;
using UnityEngine.Events;

public class CharacterPickupPair
{
    public GameObject character;
    public PickupData pickupData;

    #region Data Cloning
    public CharacterPickupPair Clone(GameObject newCharacter, PickupData newPickupData)
    {
        character = newCharacter;
        pickupData = newPickupData;
        return this;
    }
    #endregion
}

