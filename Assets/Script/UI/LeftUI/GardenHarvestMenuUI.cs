using System.Collections.Generic;
using System.Linq;
using CoreData;

public class GardenHarvestMenuUI : ListMenuBase
{
    IEnumerable<int> plots;

    public override string Action => "Harvest";
    
    public override LeftUIActionContext ActionValue
        => new HarvestActionContext { GardenPlotIndex = plots.ToArray()[CurrentSelectIndex]};
    
    public override void InitializeImp(SaveData saveData, State state)
    {
        plots =
            saveData
                .Garden
                .GardenPlots
                .Select((p, index) => p.CurrentProgress == GardenPlot.Progress.Collectable ? index : -1)
                .Where(p => p >= 0);

        var contexts = plots.Select((p, index)
            => new ListMenuElement.Context { Text = $"はたけ{plots.ToArray()[index]}" });
        SetContexts(contexts);
    }

    public override State NextState()
    {
        return new Garden{ Back = true };
    }

    public override int RightValue => ElementExist ? plots.ToArray()[CurrentSelectIndex] : -1;
}