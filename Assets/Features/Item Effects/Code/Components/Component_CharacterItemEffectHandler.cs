using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_CharacterItemEffectHandler : MonoBehaviour
{
    public List<ItemEffectData> effectList;
    private bool isOn = false;
    public float currentVelocityMod = 0f;

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
            CalculateVelocityMods();
            ClearExpiredEffects();
            yield return new WaitForSeconds(1);
        }
    }

    private void DecrementAllEffects()
    {
        foreach (ItemEffectData effect in effectList) { effect.DecrementCountdown(); }
    }

    private void CalculateVelocityMods()
    {
        float total = 0f;
        foreach (ItemEffectData effect in effectList)
        {
            if (!effect.isVelocityMod) break;
            else total += effect.velocityMod;
        }
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
        if (pair.pickupData.itemEffect == null) return;
        ItemEffectData newEffect = new(pair.pickupData.itemEffect.effect);
        if (newEffect == null) return;
        effectList.Add(newEffect);
        if (newEffect.affectsAllCharacters || pair.character == this.gameObject) effectList.Add(newEffect);
    }

    public float GetModifiedTopSpeed(float unmodifiedTopSpeed)
    {
        float multiplier = 1 + (currentVelocityMod / 100f);
        return (unmodifiedTopSpeed * multiplier);
    }

    /* obsoleted due to changes to use forces instead of setting velocity directly
    public Vector2 GetModifiedVelocity(Vector2 unModifiedVelocity)
    {
        float multiplier = 1 + (currentVelocityMod / 100f);
        return new Vector2(unModifiedVelocity.x * multiplier, unModifiedVelocity.y * multiplier);
    }
    */
}
