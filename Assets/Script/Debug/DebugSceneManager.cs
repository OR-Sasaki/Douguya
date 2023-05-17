using System.Collections.Generic;
using CoreData;
using UnityEngine;

public class DebugSceneManager : MonoBehaviour
{
    void Start()
    {
        var saveData = CreateSaveData();
        
        // 仮のアイテムを投入
        
    }

    static SaveData CreateSaveData()
    {
        var garden = new Garden();
        garden.GardenPlots.Add(new GardenPlot());
        
        return new SaveData
        {
            Game = new Game(),
            Player = new Player(),
            Garden = garden
        };
    }
}