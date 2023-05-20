using System.Collections.Generic;
using CoreData;

public class GardenMenuUI : ListMenuBase
{
    public override void Initialize(SaveData saveData)
    {
        SetContexts(new List<ListMenuElement.Context>
        {
            new()
            {
                Text = "はたけ１"
            },
            new()
            {
                Text = "はたけ２"
            },
            new()
            {
                Text = "はたけ３"
            }
        });
    }

    public override State NextState()
    {
        return CurrentSelectIndex switch
        {
            0 => new Garden(),
            1 => new Item(),
            2 => new Shop(),
            _ => new Garden()
        };
    }
}