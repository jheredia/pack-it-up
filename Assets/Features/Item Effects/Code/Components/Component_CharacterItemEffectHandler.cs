using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_CharacterItemEffectHandler : MonoBehaviour
{
    public List<ItemEffectData> effectList;
    private bool isOn = false;

    private void Start()
    {
        isOn = true;
        StartCoroutine(EffectsLogic());
    }

    public IEnumerator EffectsLogic()
    {
        while (isOn)
        {
            DecrementAllEffects();
            ClearExpiredEffects();
            yield return new WaitForSeconds(1);
        }
    }

    private void DecrementAllEffects()
    {
        foreach (ItemEffectData effect in effectList) { effect.DecrementCountdown(); }
    }

    private void ClearExpiredEffects()
    {
        for (int i = effectList.Count - 1; i >= 0; i--)
        {
            if (effectList[i] != null && effectList[i].CheckExpired()) { effectList.RemoveAt(i); }
        }
    }

    public void AcceptNewEffect(CharacterPickupPair pair)
    {
        ItemEffectData newEffect = new(pair.pickupData.itemEffect.effect);
        if (newEffect == null) return;
        effectList.Add(newEffect);
        if (newEffect.affectsAllCharacters || pair.character == this.gameObject) effectList.Add(newEffect);
    }
}