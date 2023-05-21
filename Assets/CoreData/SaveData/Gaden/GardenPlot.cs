namespace CoreData
{
    public record GardenPlot
    {
        public enum Progress
        {
            None,
            Seed,
            Sprout,
            Growth,
            Collectable,
        }
        
        public int SeedId = -1;
        public Progress CurrentProgress = Progress.None;
        public int ElapsedDaysInCurrentProgress = 0;
    }
}