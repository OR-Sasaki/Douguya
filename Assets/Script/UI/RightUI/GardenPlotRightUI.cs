using CoreData;
using LitJson;
using TMPro;
using UnityEngine;

public class GardenPlotRightUI : RightUIBase
{
    SaveData saveData;
    [SerializeField] TMP_Text text;
    
    public override void Initialize(SaveData saveData)
    {
        this.saveData = saveData;
    }

    public override void Select(int value)
    {
        var plot = saveData.Garden.GardenPlots[value];
        text.text = JsonMapper.ToJson(plot);
    }
}