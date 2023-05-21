using System.Collections.Generic;
using System.Linq;
using CoreData.Master;
using UnityEngine;

namespace Core
{
    public static class Seed
    {
        public static int RotHarvest(this CoreData.Master.Seed seed)
        {
            List<(int, int)> rewards = new()
            {
                (seed.Reward1Id, seed.Reward1Probability),
                (seed.Reward2Id, seed.Reward2Probability),
                (seed.Reward3Id, seed.Reward3Probability)
            };

            var sumWeight = rewards.Sum(r => r.Item2);
            var rand = Random.Range(0, sumWeight);

            foreach (var reward in rewards)
            {
                if (rand < reward.Item2)
                    return reward.Item1;
            }

            return -1;
        }
    }
}