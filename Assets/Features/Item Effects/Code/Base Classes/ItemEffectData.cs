using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffectData
{
    public int duration;
    private int countdown;
    public bool isPermanent = false;
    public bool affectsAllCharacters = false;
    public bool isVelocityMod = false;
    [Range(-100f, 100f)]
    public float velocityMod = 0f;

    #region Data Cloning
    public ItemEffectData() { }
    
    public ItemEffectData(ItemEffectData data)
    {
        duration = data.duration;
        countdown = data.duration;
        isPermanent = data.isPermanent;
        affectsAllCharacters = data.affectsAllCharacters;
    }
    #endregion

    public void DecrementCountdown() { countdown--; }
    public bool CheckExpired() { return !isPermanent && countdown <= 0; }
}
