using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Component_UIDisplayPickupImage : MonoBehaviour
{
    public Image image;

    public void AcceptPickupData(CharacterPickupPair pair)
    {
        Debug.Log("test");
        StopAllCoroutines();
        image.sprite = pair.pickupData.bigSprite;
        StartCoroutine(StopDisplayingSprite());
    }

    public IEnumerator StopDisplayingSprite()
    {
        yield return new WaitForSeconds(1);
        image.sprite = null;
    }
}
