using System;
using CoreData;
using UniRx;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField] MainMenuUI mainMenuUI;
    [SerializeField] ItemMenuUI itemMenuUI;
    [SerializeField] GardenMenuUI gardenMenuUI;
    [SerializeField] ShopMenuUI shopMenuUI;

    ListMenuBase currentListMenu;

    readonly Subject<State> onSelectSubject = new();
    readonly Subject<Unit> onReturnSubject = new();
    public IObservable<State> OnSelect => onSelectSubject;
    public IObservable<Unit> OnReturn => onReturnSubject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentListMenu.Next();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            currentListMenu.Prev();
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
            Main main => mainMenuUI,
            Item item => itemMenuUI,
            Shop shop => shopMenuUI,
            Garden garden => gardenMenuUI,
        };
        currentListMenu.Initialize(saveData);
        currentListMenu.gameObject.SetActive(true);
    }
}