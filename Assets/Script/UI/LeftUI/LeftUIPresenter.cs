using System;
using CoreData;
using UniRx;
using UnityEngine;

public class LeftUIPresenter : MonoBehaviour
{
    [SerializeField] MainMenuUI mainMenuUI;
    [SerializeField] ItemMenuUI itemMenuUI;
    [SerializeField] GardenMenuUI gardenMenuUI;
    [SerializeField] ShopMenuUI shopMenuUI;
    [SerializeField] GardenHarvestMenuUI gardenHarvestMenuUI;
    [SerializeField] GardenPlantMenuUI gardenPlantMenuUI;
    [SerializeField] GardenViewMenuUI gardenViewMenuUI;
    [SerializeField] GardenPlantSelectSeedMenuUI gardenPlantSelectSeedMenuUI;
    
    ListMenuBase currentListMenu;

    readonly Subject<State> onSelectSubject = new();
    readonly Subject<Unit> onReturnSubject = new();
    readonly Subject<int> onHoverChangeSubject = new();
    readonly Subject<Unit> onNextDaySubject = new();
    public IObservable<State> OnSelect => onSelectSubject;
    public IObservable<Unit> OnReturn => onReturnSubject;
    public IObservable<int> OnHoverChange => onHoverChangeSubject;
    public IObservable<Unit> OnNextDay => onNextDaySubject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentListMenu.Next();
            onHoverChangeSubject.OnNext(currentListMenu.CurrentSelectIndex);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            currentListMenu.Prev();
            onHoverChangeSubject.OnNext(currentListMenu.CurrentSelectIndex);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var nextState = currentListMenu.NextState();
            onSelectSubject.OnNext(nextState);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            onReturnSubject.OnNext(Unit.Default);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            onNextDaySubject.OnNext(Unit.Default);
        }
    }

    public void Next(State state, SaveData saveData)
    {
        if(currentListMenu)
            currentListMenu.gameObject.SetActive(false);
        
        currentListMenu = state switch
        {
            Main _ => mainMenuUI,
            Item _ => itemMenuUI,
            Shop _ => shopMenuUI,
            Garden _ => gardenMenuUI,
            GardenView _ => gardenViewMenuUI,
            GardenPlant _ => gardenPlantMenuUI,
            GardenPlantSelectSeed _ => gardenPlantSelectSeedMenuUI,
            GardenHarvest _ => gardenHarvestMenuUI
        };
        currentListMenu.Initialize(saveData);
        currentListMenu.gameObject.SetActive(true);
    }
}