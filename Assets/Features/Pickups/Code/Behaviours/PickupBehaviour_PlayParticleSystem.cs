using UnityEngine;

public class PickupBehaviour_PlayParticleSystem : PickupBehaviour
{
    public ParticleSystem particles;

    public override void OnPickupDoThis()
    {
        base.OnPickupDoThis();

        particles.Play();
    }
}
