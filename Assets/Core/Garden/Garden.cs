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

        public static void Plant()
        {
            
        }

        public static void Harvest()
        {
            
        }
    }
}