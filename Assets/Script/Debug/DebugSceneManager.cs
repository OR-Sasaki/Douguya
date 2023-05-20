using System.Collections.Generic;
using CoreData;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;

public class DebugSceneManager : MonoBehaviour
{
    StateManager stateManager;
    [SerializeField] LeftUIPresenter leftUIPresenter;
    [SerializeField] RightUIPresenter rightUIPresenter;
    
    void Start()
    {
        var saveData = CreateSaveData();

        MasterDataImporter.I.StartImport();

        stateManager = new StateManager();
        
        leftUIPresenter.OnSelect.Subscribe(state =>
        {
            stateManager.GoNextState(state);
        });

        leftUIPresenter.OnReturn.Subscribe(_ =>
        {
            stateManager.GoPrevState();
        });

        leftUIPresenter.OnHoverChange.Subscribe(value =>
        {
            rightUIPresenter.OnChangeHover(value);
        });

        leftUIPresenter.OnNextDay.Subscribe(value =>
        {
            Core.Main.NextDay(saveData);
            stateManager.Initialize();
        });

        leftUIPresenter.OnPlant.Subscribe(value =>
        {
            var action = (PlantActionContext)value;
            Core.Garden.Plant(saveData, action.GardenPlotIndex, action.PlayerItemIndex);
        });

        stateManager.OnChangeState.Subscribe(state =>
        {
            leftUIPresenter.Next(state, saveData);
            rightUIPresenter.Next(state, saveData);
        });

        stateManager.Initialize();
    }

    static SaveData CreateSaveData()
    {
        var garden = new CoreData.Garden();
        garden.GardenPlots.Add(new GardenPlot());
        garden.GardenPlots.Add(new GardenPlot());
        garden.GardenPlots.Add(new GardenPlot());
        garden.GardenPlots.Add(new GardenPlot());

        var player = new Player();
        player.PlayerItems.Add(new PlayerItem
        {
            ItemId = 1,
            Quality = 1,
        });
        player.PlayerItems.Add(new PlayerItem
        {
            ItemId = 2,
            Quality = 1,
        });
        player.PlayerItems.Add(new PlayerItem
        {
            ItemId = 3,
            Quality = 1,
        });
        
        return new SaveData
        {
            Game = new Game(),
            Player = player,
            Garden = garden
        };
    }
}