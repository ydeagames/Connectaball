using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollerBallBolt
{
    public struct OBJECT_RECORD
    {
        public Vector3 position;
    }

    public class NetworkSceneManager : MonoBehaviour
    {
        public static NetworkSceneManager Instance
        {
            get;
            private set;
        }

        public List<OBJECT_RECORD> playerPositions
        {
            get;
            private set;
        }
        public List<OBJECT_RECORD> movingObjectPositions
        {
            get;
            private set;
        }
        public OBJECT_RECORD ballPosition
        {
            get;
            private set;
        }

        private void Awake()
        {
            Instance = this;

            playerPositions = new List<OBJECT_RECORD>();
            movingObjectPositions = new List<OBJECT_RECORD>();

            GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
            OBJECT_RECORD or;
            foreach(GameObject go in gos)
            {
                or.position = go.transform.position;
                playerPositions.Add(or);
                Destroy(go);
            }

            GameObject[] mos = GameObject.FindGameObjectsWithTag("MovingObject");
            foreach (GameObject mo in mos)
            {
                or.position = mo.transform.position;
                movingObjectPositions.Add(or);
                Destroy(mo);
            }

            GameObject ball = GameObject.FindGameObjectWithTag("Ball");
            or.position = ball.transform.position;
            ballPosition = or;
            Destroy(ball);
        }

        /// <summary>
        /// BoltEntity.prefabId.Valueを受け取って、該当する
        /// プレハブIDで記録された座標を返します。
        /// </summary>
        /// <param name="id">プレハブID</param>
        /// <returns>座標を返します</returns>
        public Vector3 GetPlayerPosition(int id)
        {
            if (0 <= id && id < playerPositions.Count)
            {
                return playerPositions[id].position;
            }
            return Vector3.zero;
        }

        public Vector3 GetMovingObjectPosition(int id)
        {
            if (0 <= id && id < movingObjectPositions.Count)
            {
                return movingObjectPositions[id].position;
            }
            return Vector3.zero;
        }
    }
}
