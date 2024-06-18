using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_VelocityModCalculator : MonoBehaviour
{
    public List<Effect_VelocityMod> mods;

    public Vector2 CalculateVelocityMods(Vector2 preModVelocity)
    {
        Component_CharacterMover mover = GetComponentInParent<Component_CharacterMover>();
        //Enter velocity code here ig 

        return preModVelocity;
    }

    public void AcceptPickupData(CharacterPickupPair pair)
    {
        /*Debug.Log("test");
        StopAllCoroutines();
        image.sprite = pair.pickupData.bigSprite;
        StartCoroutine(StopDisplayingSprite()); */

        //mods.Add(pair.pickupData.)

    }
}
