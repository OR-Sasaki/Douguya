using System.Collections.Generic;
using CoreData;

public class ItemMenuUI : ListMenuBase
{
    public override void Initialize(SaveData saveData)
    {
        SetContexts(new List<ListMenuElement.Context>
        {
            new()
            {
                Text = "アイテム１"
            },
            new()
            {
                Text = "アイテム２"
            },
            new()
            {
                Text = "アイテム３"
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