using System.Collections.Generic;
using CoreData;
using UnityEngine;
using UniRx;

public class DebugSceneManager : MonoBehaviour
{
    StateManager stateManager;
    [SerializeField] Presenter presenter;
    
    void Start()
    {
        var saveData = CreateSaveData();

        MasterDataImporter.I.StartImport();

        stateManager = new StateManager();
        
        presenter.OnSelect.Subscribe(state =>
        {
            Debug.Log(typeof(State));
            stateManager.GoNextState(state);
        });

        presenter.OnReturn.Subscribe(_ =>
        {
            Debug.Log("Return");
            stateManager.GoPrevState();
        });

        stateManager.OnChangeState.Subscribe(state =>
        {
            Debug.Log(state);
            presenter.Next(state, saveData);
        });
        
        stateManager.Initialize();
    }

    static SaveData CreateSaveData()
    {
        var garden = new CoreData.Garden();
        garden.GardenPlots.Add(new GardenPlot());
        
        return new SaveData
        {
            Game = new Game(),
            Player = new Player(),
            Garden = garden
        };
    }
}