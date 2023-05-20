﻿using System;
using CoreData;
using UniRx;
using UnityEngine;

public class RightUIPresenter : MonoBehaviour
{
    RightUIBase currentUI;

    [SerializeField] NoneRightUI none;
    [SerializeField] GardenPlotRightUI gardenPlot;
    [SerializeField] GameRightUI game;

    void DisableAll()
    {
        none.gameObject.SetActive(false);
        gardenPlot.gameObject.SetActive(false);
        game.gameObject.SetActive(false);
    }
    
    public void OnChangeHover(int value)
    {
        if (!currentUI) return;
        currentUI.Select(value);
    }
    
    public void Next(State state, SaveData saveData)
    {
        DisableAll();

        currentUI = state switch
        {
            Main _ => game,
            GardenView _ => gardenPlot,
            GardenPlant _ => gardenPlot,
            GardenHarvest _ => gardenPlot,
            GardenPlantSelectSeed _ => gardenPlot,
            _ => none
        };
        currentUI.gameObject.SetActive(true);
        currentUI.Initialize(saveData);
        OnChangeHover(0);
    }
}