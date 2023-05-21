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

        leftUIPresenter.OnHarvest.Subscribe(value =>
        {
            var action = (HarvestActionContext)value;
            var item = Core.Garden.Harvest(saveData, action.GardenPlotIndex);
        });

        stateManager.OnChangeState.Subscribe(state =>
        {
            rightUIPresenter.Next(state, saveData);
            leftUIPresenter.Next(state, saveData);
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
            ItemId = 2,
            Quality = 2,
        });
        player.PlayerItems.Add(new PlayerItem
        {
            ItemId = 2,
            Quality = 3,
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