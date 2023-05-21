using System.Collections.Generic;
using System.Linq;
using CoreData;

public class GardenPlantMenuUI : ListMenuBase
{
    IEnumerable<int> plots;

    public override void InitializeImp(SaveData saveData, State state)
    {
        plots =
            saveData
                .Garden
                .GardenPlots
                .Select((p, index) => p.SeedId < 0 ? index : -1)
                .Where(p => p >= 0);

        var contexts = plots.Select((p, index)
            => new ListMenuElement.Context { Text = $"はたけ{plots.ToArray()[index]}" });
        SetContexts(contexts);
    }

    public override State NextState()
    {
        return new GardenPlantSelectSeed { GardenPlotIndex = plots.ToArray()[CurrentSelectIndex] };
    }

    public override int RightValue =>  ElementExist ?  plots.ToArray()[CurrentSelectIndex] : -1;
}