using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour_DisableSprite : PickupBehaviour
{
    public SpriteRenderer spriteRenderer;

    public override void OnPickupDoThis()
    {
        base.OnPickupDoThis();

        spriteRenderer.enabled = false;
    }
}
