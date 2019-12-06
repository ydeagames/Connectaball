using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSprite : Bolt.EntityBehaviour<ITransformState>
{
    /// <summary>
    /// transformをIPlayerStateのTranformに割り当てます。
    /// </summary>
    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }
}
