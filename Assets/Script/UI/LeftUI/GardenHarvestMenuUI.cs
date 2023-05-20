using System.Collections.Generic;
using System.Linq;
using CoreData;

public class GardenHarvestMenuUI : ListMenuBase
{
    public override void Initialize(SaveData saveData, State state)
    {
        var gardenPlots = saveData.Garden.GardenPlots;
        var contexts = gardenPlots.Select((p, index) => new ListMenuElement.Context { Text = $"はたけ{index}"});
        SetContexts(contexts);
    }

    public override State NextState()
    {
        return new None();
    }
}