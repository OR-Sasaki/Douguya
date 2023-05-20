using System.Collections.Generic;
using CoreData;

public class ShopMenuUI : ListMenuBase
{
    public override void Initialize(SaveData saveData, State state)
    {
        SetContexts(new List<ListMenuElement.Context>
        {
            new()
            {
                Text = "お店１"
            },
            new()
            {
                Text = "お店２"
            },
            new()
            {
                Text = "お店３"
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