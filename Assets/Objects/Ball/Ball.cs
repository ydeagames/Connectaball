using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollerBallBolt
{
    public class Ball : Bolt.EntityBehaviour<ITransformState>
    {
        Vector3 startPos;

        private void Start()
        {
            startPos = this.transform.position;
        }

        /// <summary>
        /// transformをIPlayerStateのTranformに割り当てます。
        /// </summary>
        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Wall")
            {
                this.transform.position = startPos;
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }
}
