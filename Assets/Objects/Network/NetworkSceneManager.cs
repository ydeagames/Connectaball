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

        public List<OBJECT_RECORD> playerPositions {
            get;
            private set;
        }
        public OBJECT_RECORD ballPosition
        {
            get;
            private set;
        }

        public OBJECT_RECORD movingObjectPosition
        {
            get;
            private set;
        }

        private void Awake()
        {
            Instance = this;

            playerPositions = new List<OBJECT_RECORD>();

            GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
            OBJECT_RECORD or;
            foreach(GameObject go in gos)
            {
                or.position = go.transform.position;
                playerPositions.Add(or);
                Destroy(go);
            }

            GameObject ball = GameObject.FindGameObjectWithTag("Ball");
            or.position = ball.transform.position;
            ballPosition = or;
            Destroy(ball);

            //GameObject movingObject = GameObject.FindGameObjectWithTag("MovingObject");
            //or.id = movingObject.GetComponent<BoltEntity>().PrefabId.Value;
            //or.position = movingObject.transform.position;
            //movingObjectPosition = or;
            //Destroy(movingObject);
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
    }
}
