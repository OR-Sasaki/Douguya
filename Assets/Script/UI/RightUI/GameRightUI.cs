using CoreData;
using LitJson;
using TMPro;
using UnityEngine;

public class GameRightUI : RightUIBase
{
    [SerializeField] TMP_Text text;
    
    public override void Initialize(SaveData saveData)
    {
        text.text = JsonMapper.ToJson(saveData.Game);
    }

    public override void Select(int value)
    {
    }

    public override void Clear()
    {
        text.text = "";
    }
}