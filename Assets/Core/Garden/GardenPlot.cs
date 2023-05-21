using System.Linq;
using CoreData;
using CoreData.Master;

namespace Core
{
    public static class GardenPlot
    {
        public static void NextDay(CoreData.GardenPlot gardenPlot)
        {
            if (gardenPlot.SeedId <= 0)
                return;
            
            gardenPlot.ElapsedDaysInCurrentProgress += 1;
            
            if (gardenPlot.CurrentProgress == CoreData.GardenPlot.Progress.Collectable) return;

            var seed = MasterData.I.Seeds[gardenPlot.SeedId];
            var needDaysForNextProgress = seed.NeedDaysForNextProgress(gardenPlot.CurrentProgress);
            if (gardenPlot.ElapsedDaysInCurrentProgress >= needDaysForNextProgress)
            {
                gardenPlot.ElapsedDaysInCurrentProgress = 0;
                gardenPlot.CurrentProgress++;
            }
        }

        public static void Plant(SaveData saveData, CoreData.GardenPlot gardenPlot, int playerItemIndex)
        {
            var playerItem = saveData.Player.PlayerItems[playerItemIndex];
            saveData.Player.PlayerItems.RemoveAt(playerItemIndex);

            var item = MasterData.I.Items[playerItem.ItemId];
            var seed = MasterData.I.Seeds.Values.First(s => s.SeedItemId == item.Id);
            gardenPlot.SeedId = seed.Id;
        }

        public static int Harvest(SaveData saveData, CoreData.GardenPlot gardenPlot)
        {
            var seed = MasterData.I.Seeds[gardenPlot.SeedId];
            var harvestItem = seed.RotHarvest();
            saveData.Player.PlayerItems.Add(new PlayerItem { ItemId = harvestItem, Quality = 1 });

            Initialize(gardenPlot);
            return harvestItem;
        }

        static void Initialize(CoreData.GardenPlot gardenPlot)
        {
            gardenPlot.SeedId = -1;
            gardenPlot.CurrentProgress = CoreData.GardenPlot.Progress.None;
            gardenPlot.ElapsedDaysInCurrentProgress = 0;
        }
    }
}