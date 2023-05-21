using System.Collections.Generic;
using System.Linq;
using CoreData;

public class GardenMenuUI : ListMenuBase
{
    public override void InitializeImp(SaveData saveData, State state)
    {
        SetContexts(new List<ListMenuElement.Context>
        {
            new()
            {
                Text = "見る"
            },
            new()
            {
                Text = "植える"
            },
            new()
            {
                Text = "収穫する"
            }
        });
    }

    public override State NextState()
    {
        return CurrentSelectIndex switch
        {
            0 => new GardenView(),
            1 => new GardenPlant(),
            2 => new GardenHarvest(),
            _ => new Garden()
        };
    }
}