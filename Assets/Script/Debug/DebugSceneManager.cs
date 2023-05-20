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
            Debug.Log( $"OnSelect {typeof(State)}");
            stateManager.GoNextState(state);
        });

        leftUIPresenter.OnReturn.Subscribe(_ =>
        {
            Debug.Log("OnReturn");
            stateManager.GoPrevState();
        });

        leftUIPresenter.OnHoverChange.Subscribe(value =>
        {
            rightUIPresenter.OnChangeHover(value);
        });

        stateManager.OnChangeState.Subscribe(state =>
        {
            Debug.Log( $"OnChangeState {typeof(State)}");
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
        
        return new SaveData
        {
            Game = new Game(),
            Player = new Player(),
            Garden = garden
        };
    }
}