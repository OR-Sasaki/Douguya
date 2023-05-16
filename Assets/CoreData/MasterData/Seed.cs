using System.Collections.Generic;

namespace CoreData.Master
{
    public record Seed
    {
        public int SeedId;
        public SeedProbability[] Probabilities;
        public int SeedToSprout;
        public int SproutToGrowth;
        public int GrowthToCollectable;
        public Dictionary<GardenPlot.Progress, int> NeedDaysForNextProgress;

        public record SeedProbability
        {
            public int Id;
            public float Probability;
        }
    }
}