using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class stageSelect : MonoBehaviour
{
    public BoxCollider2D box;
    public LayerMask targetMask;

    public Text stage1Tex;
    public Text stage2Tex;
    public Text stage3Tex;
    public Text stage4Tex;
    public Text stage5Tex;


    int playerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var filter = new ContactFilter2D() { layerMask = targetMask.value, useLayerMask = true };
        var results = new Collider2D[9];
        var resultCount = box.OverlapCollider(filter, results);

        //何人入っているかのテキストの描画
        if (this.name == "Stage1Box")
        {
            stage1Tex.text = (resultCount / 2).ToString() + "/4";
        }

        else if (this.name == "Stage2Box")
        {
            stage2Tex.text = (resultCount / 2).ToString() + "/4";
        }
        else if (this.name == "Stage3Box")
        {
            stage3Tex.text = (resultCount / 2).ToString() + "/4";
        }

        else if (this.name == "Stage4Box")
        {
            stage4Tex.text = (resultCount / 2).ToString() + "/4";
        }
        else if (this.name == "Stage5Box")
        {
            stage5Tex.text = (resultCount / 2).ToString() + "/4";
        }

        //全員入ったらゲームシーンに遷移 if文の中にシーン遷移の記述をお願いします
        if (resultCount >= 8)
        {
            if(this.name == "Stage1Box")
            {
                Debug.Log("シーン遷移1");
            }

            else if(this.name == "Stage2Box")
            {
                Debug.Log("シーン遷移2");
            }
            else if (this.name == "Stage3Box")
            {
                Debug.Log("シーン遷移3");
            }

            else if (this.name == "Stage4Box")
            {
                Debug.Log("シーン遷移4");
            }
            else if(this.name == "Stage5Box")
            {
                Debug.Log("シーン遷移5");
            }

        }
    }

}
