using System.Collections.Generic;

namespace CoreData
{
    public record Garden
    {
        public readonly List<GardenPlot> GardenPlots = new();
    }
}