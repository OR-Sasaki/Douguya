using CoreData;

namespace Core
{
    public static class Garden
    {
        internal static void NextDay(SaveData saveData)
        {
            foreach (var gardenPlot in saveData.Garden.GardenPlots)
            {
                GardenPlot.NextDay(gardenPlot);
            }
        }

        public static void Plant(SaveData saveData, int gardenPlotIndex, int playerItemIndex)
        {
            GardenPlot.Plant(saveData, saveData.Garden.GardenPlots[gardenPlotIndex], playerItemIndex);
        }

        public static int Harvest(SaveData saveData, int gardenPlotIndex)
        {
            return GardenPlot.Harvest(saveData, saveData.Garden.GardenPlots[gardenPlotIndex]);
        }
    }
}