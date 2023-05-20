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
    public IObservable<State> OnSelect => onSelectSubject;
    public IObservable<Unit> OnReturn => onReturnSubject;
    public Subject<int> OnHoverChange => onHoverChangeSubject;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentListMenu.Next();
            OnHoverChange.OnNext(currentListMenu.CurrentSelectIndex);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            currentListMenu.Prev();
            OnHoverChange.OnNext(currentListMenu.CurrentSelectIndex);
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