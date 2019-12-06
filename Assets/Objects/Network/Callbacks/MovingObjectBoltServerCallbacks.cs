using UnityEngine;
using RollerBallBolt;

namespace RollerBallBolt
{
    [BoltGlobalBehaviour(BoltNetworkModes.Server, "SampleScene")]
    public class MovingObjectBoltServerCallbacks : Bolt.GlobalEventListener
    {
        /// <summary>
        /// このプログラムがシーンを読み込んだ時の処理
        /// </summary>
        /// <param name="map"></param>
        public override void SceneLoadLocalDone(string map)
        {
            // 落下する足場を生成します
            BoltEntity be = BoltNetwork.Instantiate(BoltPrefabs.FallFloorType01);
            be.transform.position = NetworkSceneManager.Instance.GetMovingObjectPosition(be.PrefabId.Value);

            // 移動するオブジェクトを生成します
            be = BoltNetwork.Instantiate(BoltPrefabs.MovingBoxType01);
            be.transform.position = NetworkSceneManager.Instance.GetMovingObjectPosition(be.PrefabId.Value);
        }
    }
}
