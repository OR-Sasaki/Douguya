using System.Collections.Generic;
using System.Linq;
using CoreData;
using CoreData.Master;

public class ItemMenuUI : ListMenuBase
{
    public override void InitializeImp(SaveData saveData, State state)
    {
        var playerItems = saveData.Player.PlayerItems;
        var contexts = playerItems.Select((item, index)
            => new ListMenuElement.Context { Text = $"{MasterData.I.Items[item.ItemId].Name}"});
        SetContexts(contexts);
    }

    public override State NextState()
    {
        return new None();
    }
}