using System.Collections.Generic;
using CoreData;

public class MainMenuUI : ListMenuBase
{
    public override void InitializeImp(SaveData saveData, State state)
    {
        SetContexts(new List<ListMenuElement.Context>
        {
            new()
            {
                Text = "にわ"
            },
            new()
            {
                Text = "アイテム"
            },
            new()
            {
                Text = "お店"
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