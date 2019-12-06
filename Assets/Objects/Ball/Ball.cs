using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollerBallBolt
{
    public class Ball : Bolt.EntityBehaviour<ITransformState>
    {
        Vector3 startPos;
        public AudioClip BallTouch;
        public AudioClip BallDeath;

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
                AudioSource.PlayClipAtPoint(BallDeath, transform.position);
                this.transform.position = startPos;
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            else
            {
                AudioSource.PlayClipAtPoint(BallTouch, transform.position);
            }
        }
    }
}
