using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class Player : Bolt.EntityBehaviour<ITransformState>
{
    Vector2 mouse;
    public int controller;
    Rigidbody2D rigid;
    Vector2 force;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mouse.x = Input.GetAxis($"Joy{controller}X");
        mouse.y = Input.GetAxis($"Joy{controller}Y");
        //Debug.Log($"x:{x.ToString("F2")}, y:{y.ToString("F2")}");
    }

    void FixedUpdate()
    {
        rigid.AddForce(new Vector2(force.x, force.y), ForceMode2D.Impulse);
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
        IRollerBallBoltCommandInput input = RollerBallBoltCommand.Create();

        Vector3 data = new Vector3(mouse.x, mouse.y, 0);
        input.Mouse = data;

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
        RollerBallBoltCommand cmd = (RollerBallBoltCommand)command;

        if (resetState)
        {
            // Player2。送られてきたコマンドのデータを反映させます
            transform.localPosition = cmd.Result.Position;
        }
        else
        {
            // 入力を使ってオブジェクトを動かします
            force = cmd.Input.Mouse;

            // ホストとクライアントの双方で呼び出されます
            // 現在の座標を送信します
            cmd.Result.Position = transform.localPosition;
        }
    }

    #endregion Network
}
