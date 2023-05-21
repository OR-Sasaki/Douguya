using CoreData;
using CoreData.Master;
using LitJson;
using TMPro;
using UnityEngine;

public class PlayerItemRightUI : RightUIBase
{
    SaveData saveData;
    [SerializeField] TMP_Text text;
    
    public override void Initialize(SaveData saveData)
    {
        this.saveData = saveData;
    }

    public override void Select(int value)
    {
        var playerItem = saveData.Player.PlayerItems[value];
        text.text = JsonUtility.ToJson(playerItem);

        var item = MasterData.I.Items[playerItem.ItemId];
        text.text += JsonUtility.ToJson(item);
    }
    
    public override void Clear()
    {
        text.text = "";
    }
}