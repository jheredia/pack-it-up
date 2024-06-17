using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour_PlaySoundFX : PickupBehaviour
{
    public AudioSource source;
    public List<AudioClip> fxList;

    public override void OnPickupDoThis()
    {
        base.OnPickupDoThis(); 
        AudioClip fx = fxList[UnityEngine.Random.Range(0, fxList.Count)];
        source.PlayOneShot(fx);
    }
}
