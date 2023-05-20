using System;
using System.Collections.Generic;

namespace CoreData.Master
{
    public record Seed
    {
        public int Id;
        public int SeedItemId;
        public int Reward1Id;
        public int Reward1Probability;
        public int Reward2Id;
        public int Reward2Probability;
        public int Reward3Id;
        public int Reward3Probability;
        public int SeedToSprout;
        public int SproutToGrowth;
        public int GrowthToCollectable;

        public int NeedDaysForNextProgress(GardenPlot.Progress progress)
            => progress switch
            {
                GardenPlot.Progress.Seed => SeedToSprout,
                GardenPlot.Progress.Sprout => SproutToGrowth,
                GardenPlot.Progress.Growth => GrowthToCollectable,
                _ => 0
            };
    }
}