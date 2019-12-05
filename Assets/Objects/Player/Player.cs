using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class Player : Bolt.EntityBehaviour<ITransformState>
{
    Rigidbody2D rigid;
    float angularDrag = 0.99f;

    Vector2 myAcceleration;
    Vector2 sharedAcceleration;
    float myAngularAcceleration;
    float sharedAngularAcceleration;
    float sharedAngularVelocity;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myAcceleration.x = Input.GetAxis($"JoyX");
        myAcceleration.y = Input.GetAxis($"JoyY");
        myAngularAcceleration = Input.GetAxis($"JoyLR");
        //Debug.Log($"x:{mouse.x.ToString("F2")}, y:{mouse.y.ToString("F2")}");
    }

    void FixedUpdate()
    {
        rigid.AddForce(new Vector2(sharedAcceleration.x, sharedAcceleration.y), ForceMode2D.Impulse);
        sharedAngularVelocity += sharedAngularAcceleration;
        sharedAngularVelocity *= angularDrag;
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + sharedAngularVelocity);
    }

    #region Network

    /// <summary>
    /// transformをIPlayerStateのTranformに割り当てます。
    /// </summary>
    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }

    /// <summary>
    /// 操作権を持つプレイヤー(Player1はホスト、
    /// Player2はクライアント)で呼び出されます。
    /// 操作をBoltEntityに渡します。
    /// </summary>
    public override void SimulateController()
    {
        IPlayerCommandInput input = PlayerCommand.Create();


        input.Acceleration = new Vector3(myAcceleration.x, myAcceleration.y, 0);
        input.AngularAcceleration = myAngularAcceleration;

        entity.QueueInput(input);
    }

    /// <summary>
    /// オブジェクトのオーナーで呼び出されます。
    /// これはPlayer1, 2ともにホストで呼び出されます。
    /// 入力を受け取って動かします。
    /// Player2からは、クライアントに結果を送信します。
    /// </summary>
    /// <param name="command">送られてきた操作コマンド</param>
    /// <param name="resetState">操作権を持っていたらtrue</param>
    public override void ExecuteCommand(Command command, bool resetState)
    {
        PlayerCommand cmd = (PlayerCommand)command;

        if (resetState)
        {
            // Player2。送られてきたコマンドのデータを反映させます
            transform.localPosition = cmd.Result.Position;
            transform.localEulerAngles = new Vector3(0, 0, cmd.Result.Rotation);
        }
        else
        {
            // 入力を使ってオブジェクトを動かします
            sharedAcceleration = cmd.Input.Acceleration;
            sharedAngularAcceleration = cmd.Input.AngularAcceleration;

            // ホストとクライアントの双方で呼び出されます
            // 現在の座標を送信します
            cmd.Result.Position = transform.localPosition;
            cmd.Result.Rotation = transform.localRotation.z;
        }
    }

    #endregion Network
}
