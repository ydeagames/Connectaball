using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerColors", menuName = "Game/PlayerColors")]
public class PlayerColors : ScriptableObject
{
    public Color[] colors;

    public Color GetColor(int id)
    {
        if (id < 0)
            return Color.white;
        return colors[id % colors.Length];
    }
}
