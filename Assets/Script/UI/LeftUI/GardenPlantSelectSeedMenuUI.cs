using System.Collections.Generic;
using System.Linq;
using CoreData;
using CoreData.Master;

public class GardenPlantSelectSeedMenuUI : ListMenuBase
{
    IEnumerable<int> items;
    GardenPlantSelectSeed state;

    public override string Action => "Plant";

    public override LeftUIActionContext ActionValue
        => new PlantActionContext
        {
            PlayerItemIndex = items.ToArray()[CurrentSelectIndex],
            GardenPlotIndex = state.GardenPlotIndex
        };

    public override void InitializeImp(SaveData saveData, State state)
    {
        this.state = (GardenPlantSelectSeed)state;
        
        items =
            saveData
                .Player
                .PlayerItems
                .Select(p =>  MasterData.I.Items[p.ItemId])
                .Select((i, index) => i.Type == "Seed" ? index : -1)
                .Where(i => i >= 0);
        
        var contexts = items.Select((item, index)
            => new ListMenuElement.Context { Text = $"{MasterData.I.Items[saveData.Player.PlayerItems[item].ItemId].Name}"});
        SetContexts(contexts);
    }

    public override State NextState()
    {
        return new Garden{ Back = true };
    }

    public override int RightValue => ElementExist ? items.ToArray()[CurrentSelectIndex] : -1;
}