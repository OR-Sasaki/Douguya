using CoreData;
using CoreData.Master;

namespace Core
{
    public static class GardenPlot
    {
        public static void NextDay(CoreData.GardenPlot gardenPlot)
        {
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
    }
}